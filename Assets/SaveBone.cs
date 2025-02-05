using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBone : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinned;
    [SerializeField] private Transform[] bones;
    [SerializeField] private Transform root;

    [Button]
    private void SaveBones()
    {
        root = skinned.rootBone;
        bones = skinned.bones;
    }
}
