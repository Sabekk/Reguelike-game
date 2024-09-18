using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayManager
{
    public void Initialzie();
    public void LateInitialzie();
    public void CleanUp();
}
