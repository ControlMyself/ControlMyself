using UnityEngine;
using System.Collections.Generic;

public class C_Keyhole : MonoBehaviour
{
    public enum KEYMODE { HOLDING, SWITCHING, ONCE };
    public KEYMODE CurrentKEYMODE;
    public List<C_Command> Commands_Stay = new List<C_Command>();
    public List<C_Command> Commands_Start = new List<C_Command>();
    public List<C_Command> Commands_End = new List<C_Command>();
    public List<GameObject> Key = new List<GameObject>();
    [HideInInspector]
    public bool On;
    private Collider MyCollider;

    //C_Keyhole
    void KeyStart ()
    {
        foreach(C_Command item in Commands_Start)
        {
            item.Event.Invoke();
        }
    }

    void KeyStay()
    {
        foreach (C_Command item in Commands_Stay)
        {
            item.Event.Invoke();
        }
    }

    void KeyEnd()
    {
        foreach (C_Command item in Commands_End)
        {
            item.Event.Invoke();
        }
    }

    bool CheckKeyMatching (GameObject a)
    {
        bool _match = false;
        foreach (GameObject item in Key)
        {
            if (a.Equals(item))
            {
                _match = true;
                break;
            }
        }
        return _match;
    }

    //MonoBehaviour
    void FixedUpdate ()
    {
        if (On)
        {
            KeyStay();
        }
    }

    void OnTriggerEnter (Collider _coll)
    {
        if (CheckKeyMatching(_coll.gameObject))
        {
            switch (CurrentKEYMODE)
            {
                case KEYMODE.ONCE:
                    if (!On)
                    {
                        On = true;
                        KeyStart();
                    }
                    break;
                case KEYMODE.HOLDING:
                    On = true;
                    KeyStart();
                    break;
                case KEYMODE.SWITCHING:
                    On = !On;
                    if (On) KeyStart();
                    else KeyEnd();
                    break;
            }
        }
    }

    void OnTriggerExit (Collider _coll)
    {
        if (CheckKeyMatching(_coll.gameObject))
        {
            if (CurrentKEYMODE == KEYMODE.HOLDING)
            {
                On = false;
                KeyEnd();
            }
        }
    }
}
