using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayManager<T> : MonoSingleton<T>, IGameplayManager where T : MonoBehaviour
{
    public virtual void Initialzie() { }
    public virtual void LateInitialzie() { }
    public virtual void CleanUp() { }
}
