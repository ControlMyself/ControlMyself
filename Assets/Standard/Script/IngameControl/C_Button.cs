using UnityEngine;
using System.Collections.Generic;

public class C_Button : IngameComp
{
    public List<C_Command> MyCommand = new List<C_Command>();

    void Awake ()
    {
        MyRigidBody = GetComponent<Rigidbody>();
        if (MyRigidBody == null)
        {
            //터치에 필요한 콜라이더가 존재하는 체크
            Debug.Log("Rigidbody Componant not Found. " + gameObject.name + ".C_Button Componant Destroyed.");
            Destroy(this);
        }
        MyCollider = GetComponent<Collider>();
        if (MyCollider == null)
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
            item.Excute();
        }
    }
}
