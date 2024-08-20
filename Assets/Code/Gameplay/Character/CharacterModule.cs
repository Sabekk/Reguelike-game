using UnityEngine;

namespace Gameplay.Character.Module
{
    public abstract class CharacterModule
    {
        [SerializeField] protected CharacterBase character;

        public virtual void Initialize(CharacterBase character)
        {
            this.character = character;
        }

        public abstract void OnUpdate();
        public abstract void CleanUp();
    }
}