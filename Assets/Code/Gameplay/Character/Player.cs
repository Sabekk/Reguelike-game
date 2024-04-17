using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class Player : CharacterBase
    {
        // Start is called before the first frame update
        void Start()
        {
            Initialize(CharacterDatabase.Instance.PlayerData);
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(Values.HP.CurrentValue);
        }
    }

}
