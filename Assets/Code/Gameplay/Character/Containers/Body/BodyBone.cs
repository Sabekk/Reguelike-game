using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BodyBone : MonoBehaviour, IIdEqualable
{
    #region VARIABLES

    [SerializeField, ReadOnly] private int id = Guid.NewGuid().GetHashCode();

    #endregion

    #region PROPERTIES

    public int Id => id;

    #endregion

    #region METHODS

    public bool IdEquals(int id)
    {
        return Id == id;
    }

    #endregion
}
