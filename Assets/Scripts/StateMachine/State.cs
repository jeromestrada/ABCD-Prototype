using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public void OnEnter() { }
    virtual public void OnUpdate() { }
    virtual public void OnExit() { }
}
