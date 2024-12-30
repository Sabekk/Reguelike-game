using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
   public static void SetActiveOptimize(this GameObject gameObject, bool state)
    {
        if (gameObject.activeInHierarchy == state)
            return;

        gameObject.SetActive(state);
    }
}
