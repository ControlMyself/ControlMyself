using UnityEngine;
using System.Collections.Generic;

public class C_Keyhole : IngameComp
{
    public enum KEYMODE { HOLDING, SWITCHING, ONCE };
    public KEYMODE CurrentKEYMODE;
    public List<C_Command> Commands_Stay = new List<C_Command>();
    public List<C_Command> Commands_Start = new List<C_Command>();
    public List<C_Command> Commands_End = new List<C_Command>();
    public List<GameObject> Key = new List<GameObject>();

    //C_Keyhole
    void KeyStart ()
    {
        foreach(C_Command item in Commands_Start)
        {
            item.Excute();
        }
    }

    void KeyStay()
    {
        foreach (C_Command item in Commands_Stay)
        {
            item.Excute();
        }
    }

    void KeyEnd()
    {
        foreach (C_Command item in Commands_End)
        {
            item.Excute();
        }
    }

    bool CheckKeyMatching (GameObject a)
    {
        if (!GetOn()) return false;
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
        if (GetOn())
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
                    if (!GetOn())
                    {
                        SetOn(true);
                        KeyStart();
                    }
                    break;
                case KEYMODE.HOLDING:
                    SetOn(true);
                    KeyStart();
                    break;
                case KEYMODE.SWITCHING:
                    SwapOn();
                    if (GetOn()) KeyStart();
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
                SetOn(false);
                KeyEnd();
            }
        }
    }
}
