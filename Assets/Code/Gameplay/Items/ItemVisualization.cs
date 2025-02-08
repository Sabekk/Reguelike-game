using Gameplay.Character;
using ObjectPooling;
using Sirenix.OdinInspector;
using StudioJAW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public class ItemVisualization : MonoBehaviour, IPoolable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int[] bonesIds;
        [SerializeField] private Transform[] itemCoreBones;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] private ItemVisualizationSocketType socketType;

        private List<Transform> settingBones = new();

        #endregion

        #region PROPERTIES

        public PoolObject Poolable { get; set; }

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
            Player player = FindAnyObjectByType<Player>();

            for (int i = 0; i < bonesIds.Length; i++)
            {
                BodyBone bone = player.BodyContainer.GetBodyBoneById(bonesIds[i]);
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
            bonesIds = null;

            if (skinnedMeshRenderer == null)
                skinnedMeshRenderer.GetComponent<SkinnedMeshRenderer>();
            Player player = FindAnyObjectByType<Player>();

            BoneRemapper remapper = GetComponent<BoneRemapper>();
            if (remapper == null)
                remapper = gameObject.AddComponent<BoneRemapper>();

            remapper.BoneEditor.ForceSpawnBones();

            itemCoreBones = remapper.bones;

            Transform rootBone = itemCoreBones[0];
            skinnedMeshRenderer.rootBone = rootBone;
            skinnedMeshRenderer.bones = itemCoreBones;

            List<int> ids = new();
            for (int i = 0; i < itemCoreBones.Length; i++)
            {
                BodyBone bone = player.BodyContainer.GetBodyBone(itemCoreBones[i].name);
                if (bone != null)
                    ids.Add(bone.Id);
            }
            bonesIds = ids.ToArray();
            DestroyImmediate(remapper);
        }

        #endregion
    }
}
