using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : StateMachine
{
    public virtual void Enter()
    {
        
    }

    public virtual void Leave()
    {
        
    }

    public abstract void Act();
    public abstract void Reason();
}
