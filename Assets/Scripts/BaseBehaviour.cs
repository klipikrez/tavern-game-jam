using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBehaviour : ScriptableObject
{
    public abstract void StartBehaviour(GameManager manager);
    public abstract void UpdateBehaviour(GameManager manager);
    public abstract void EndBehaviour(GameManager manager);

}

