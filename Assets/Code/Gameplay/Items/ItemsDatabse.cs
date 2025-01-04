using Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/ItemsDatabse", fileName = "ItemsDatabse")]
public class ItemsDatabse : ScriptableObject
{
    #region VARIABLES

    [SerializeField] private List<ItemData> itemDatas;

    #endregion

    #region PROPERTIES

    public List<ItemData> ItemDatas => itemDatas;

    #endregion

    #region METHODS

    #endregion
}
