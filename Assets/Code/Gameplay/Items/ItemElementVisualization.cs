using Gameplay.Character;
using Gameplay.Character.Body;
using ObjectPooling;
using Sirenix.OdinInspector;
using StudioJAW;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public class ItemElementVisualization : MonoBehaviour, IPoolable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int[] bonesIds;
        [SerializeField] private Transform[] itemCoreBones;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] private ItemVisualizationSocketType socket;
        [SerializeField, Tooltip("Items in this sockets will be hide")] private List<ItemVisualizationSocketType> incompatibleSockets;

        private List<Transform> settingBones = new();

        #endregion

        #region PROPERTIES

        public PoolObject Poolable { get; set; }
        public ItemVisualizationSocketType Socket => socket;
        public List<ItemVisualizationSocketType> IncompatibleSockets => incompatibleSockets;

        #endregion

        #region METHODS


        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        public void CopyBones(SkinnedMeshRenderer toCopy)
        {
            skinnedMeshRenderer.bones = toCopy.bones;
        }

        [Button]
        private void ApplyPlayerBones()
        {
            settingBones.Clear();
            CharacterInGame characterInGame = FindAnyObjectByType<CharacterInGame>();

            for (int i = 0; i < bonesIds.Length; i++)
            {
                BodyBone bone = characterInGame.GetBodyBoneById(bonesIds[i]);
                if (bone != null)
                    settingBones.Add(bone.transform);
            }

            if (settingBones.Count > 0)
            {
                skinnedMeshRenderer.rootBone = settingBones[0];
                skinnedMeshRenderer.bones = settingBones.ToArray();

            }
        }

        [Button]
        private void FixBones()
        {
            EditorUtility.SetDirty(this);

            transform.DestroyChildrenImmediate();

            if (skinnedMeshRenderer == null)
                skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

            BoneRemapper remapper = GetComponent<BoneRemapper>();
            if (remapper == null)
                remapper = gameObject.AddComponent<BoneRemapper>();

            remapper.BoneEditor.ForceSpawnBones(transform);

            itemCoreBones = remapper.bones;

            Transform rootBone = itemCoreBones[0];
            skinnedMeshRenderer.rootBone = rootBone;
            skinnedMeshRenderer.bones = itemCoreBones;

            DestroyImmediate(remapper);

            if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
                PrefabUtility.ApplyPrefabInstance(gameObject, InteractionMode.UserAction);
            GetIdsOfCharacterBones();
        }

        [Button]
        private void GetIdsOfCharacterBones()
        {
            EditorUtility.SetDirty(this);
            bonesIds = null;

            CharacterInGame characterInGame = FindAnyObjectByType<CharacterInGame>();
            List<int> ids = new();
            for (int i = 0; i < itemCoreBones.Length; i++)
            {
                BodyBone bone = characterInGame.GetBodyBone(itemCoreBones[i].name);
                if (bone != null)
                    ids.Add(bone.Id);
            }
            bonesIds = ids.ToArray();

            if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
                PrefabUtility.ApplyPrefabInstance(gameObject, InteractionMode.UserAction);
        }

        #endregion
    }
}
