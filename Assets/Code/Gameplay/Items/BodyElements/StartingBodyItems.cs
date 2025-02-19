using Gameplay.Items;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character/StartValues/StartingBodyItems", fileName = "StartingBodyItems")]
public class StartingBodyItems : ScriptableObject
{
    #region VARIABLES

    [SerializeField] private SerializableDictionary<BodyElementType, StartingBodyItem> bodyElements;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS
    private void Test()
    {

    }

    #endregion
}
