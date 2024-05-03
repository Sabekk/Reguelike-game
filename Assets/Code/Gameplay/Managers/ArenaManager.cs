using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Gameplay.Arena
{
    public class ArenaManager : MonoSingleton<ArenaManager>
    {
        #region EVENTS

        public event Action<int> OnStagePreChanged;
        public event Action<int> OnStageChanged;
        public event Action<int> OnStagePreStarted;
        public event Action<int> OnStageStarted;
        public event Action<BiomType> OnBiomPreChanged;
        public event Action<BiomType> OnBiomChanged;

        #endregion

        #region VARIABLES

        [SerializeField] private BiomType currentBiom = BiomType.DEFAULT;
        [SerializeField] private int currentStage;

        [BoxGroup("Cheats")]
        [SerializeField] private BiomType cheatBiom;

        #endregion

        #region PROPERTIES

        public BiomType CurrentBiom => currentBiom;
        public int CurrentStage => currentStage;

        #endregion

        #region METHODS

        public void ChangeBiom(BiomType newType)
        {
            if (currentBiom == newType)
                return;

            OnBiomPreChanged?.Invoke(currentBiom);
            currentBiom = newType;
            OnBiomPreChanged?.Invoke(currentBiom);
        }

        public void ChangeStage(int newStage)
        {
            OnStagePreChanged?.Invoke(currentStage);
            currentStage = newStage;
            OnStagePreChanged?.Invoke(currentStage);
        }

        #endregion

        #region EDITOR_METHODS

        [Button]
        private void ChangeBiomCheat()
        {
            ChangeBiom(cheatBiom);
        }

        #endregion
    }
}
