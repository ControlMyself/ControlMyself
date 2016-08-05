using UnityEngine;
using System.Collections.Generic;

public class C_Button : MonoBehaviour
{
    public List<C_Command> MyCommand = new List<C_Command>();
    [HideInInspector]
    public Collider Mycollider;
    [HideInInspector]
    public Rigidbody MyRigidbody;

    void Awake ()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        if (MyRigidbody == null)
        {
            //터치에 필요한 콜라이더가 존재하는 체크
            Debug.Log("Rigidbody Componant not Found. " + gameObject.name + ".C_Button Componant Destroyed.");
            Destroy(this);
        }
        Mycollider = GetComponent<Collider>();
        if (Mycollider == null)
        {
            //터치에 필요한 콜라이더가 존재하는 체크
            Debug.Log("Collider Componant not Found. " + gameObject.name + ".C_Button Componant Destroyed.");
            Destroy(this);
        }
    }

    public void Excute()
    {
        foreach (C_Command item in MyCommand)
        {
            if (item.enabled) item.Event.Invoke();
        }
    }
}
