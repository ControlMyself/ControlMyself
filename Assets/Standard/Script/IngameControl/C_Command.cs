using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class C_Command : IngameComp
{
    [SerializeField]
    public UnityEvent Event;

    public void Excute()
    {
        if (GetOn())
            Event.Invoke();
    }
}

