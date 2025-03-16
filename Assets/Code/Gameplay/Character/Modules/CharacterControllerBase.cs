using UnityEngine;

namespace Gameplay.Character.Controller
{
    public abstract class CharacterControllerBase
    {
        [SerializeField, HideInInspector] protected CharacterBase character;

        public virtual void Initialize(CharacterBase character)
        {
            this.character = character;
        }

        public abstract void OnUpdate();
        public abstract void CleanUp();
    }
}