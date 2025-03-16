using Gameplay.Items;
using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Gameplay.Character.Body
{
    public class CharacterInGame : MonoBehaviour, IPoolable
    {
        #region ACTION

        public event Action OnKill;

        #endregion

        #region VARIABLES

        [SerializeField, FoldoutGroup("Components")] private Animator animator;
        [SerializeField, FoldoutGroup("Components")] private Rigidbody rb;
        [SerializeField, FoldoutGroup("Components")] private CapsuleCollider capsuleCollider;
        [SerializeField] private SerializableDictionary<ItemVisualizationSocketType, BodySocket> bodySockets;
        [SerializeField] private Transform rootBone;
        [SerializeField] private SerializableDictionary<int, BodyBone> bodyBones;
        [SerializeField] private Avatar animatorAvatar;

        #endregion

        #region PROPERTIES

        public CapsuleCollider CapsuleCollider => capsuleCollider;
        public Rigidbody Rb => rb;
        public Animator Aniamtor => animator;
        public Transform RootBone => rootBone;
        public SerializableDictionary<int, BodyBone> BodyBones => bodyBones;
        public SerializableDictionary<ItemVisualizationSocketType, BodySocket> BodySockets => bodySockets;
        public CharacterBase Character { get; set; }
        public PoolObject Poolable { get; set; }

        #endregion

        #region METHODS

        public void Initialize(CharacterBase character)
        {
            Character = character;
        }

        public BodyBone GetBodyBoneById(int id)
        {
            if (BodyBones.TryGetValue(id, out BodyBone bone))
                return bone;
            else
            {
                Debug.LogError($"Errored bone ID: {id}, check called item");
                return null;
            }

        }

        public BodyBone GetBodyBone(string name)
        {
            foreach (var bodyBone in BodyBones.Values)
            {
                if (bodyBone.transform.name == name)
                    return bodyBone;
            }

            return null;
        }

        public BodySocket GetBodySocket(ItemVisualizationSocketType socketType)
        {
            return null;

        }

        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        [Button]
        private void SetBodyBones()
        {
            bodyBones = null;

            Transform[] bonesTmp = RootBone.GetComponentsInChildren<Transform>();
            SerializableDictionary<int, BodyBone> bodyBonesTmp = new();

            foreach (var bone in bonesTmp)
            {
                BodyBone bodyBone = bone.GetComponent<BodyBone>();

                if (bodyBone == null)
                    bodyBone = bone.gameObject.AddComponent<BodyBone>();

                bodyBonesTmp.Add(bodyBone.Id, bodyBone);
            }

            bodyBones = bodyBonesTmp;
        }

        [Button]
        private void FindAllBodyElements()
        {
            bodySockets.Clear();

            BodySocket[] sockets = transform.GetComponentsInChildren<BodySocket>();
            foreach (var socket in sockets)
            {
                if (bodySockets.ContainsKey(socket.SocketType))
                {
                    Debug.LogWarning($"Doubled socket type {socket.SocketType}", socket);
                    return;
                }

                bodySockets.Add(socket.SocketType, socket);
            }
        }

        #endregion
    }
}