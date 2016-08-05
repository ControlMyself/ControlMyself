using UnityEngine;
using System.Collections;

public class C_Push : MonoBehaviour
{
    private Rigidbody MyRigidBody;
    
    //C_Push
    public void SetGravity (bool _on)
    {
        MyRigidBody.useGravity = _on;
    }

    //MonoBehaviour
    void Awake ()
    {
        MyRigidBody = GetComponent<Rigidbody>();
        if (MyRigidBody == null)
        {
            Debug.Log("RigidBody Componant not Found. " + gameObject.name + ".C_Push Componant Destroyed.");
            Destroy(this);
        }
        C_OrbitMove _Conflict1 = GetComponent<C_OrbitMove>();
        if (_Conflict1 != null)
        {
            Debug.Log(gameObject.name + ".C_OrbitMove Componant Found. " + gameObject.name + ".C_Push Componant Destroyed.");
            Destroy(this);
        }
        C_OrbitMove _Conflict2 = GetComponent<C_OrbitMove>();
        if (_Conflict2 != null)
        {
            Debug.Log(gameObject.name + ".C_PalyerMove Componant Found. " + gameObject.name + ".C_Push Componant Destroyed.");
            Destroy(this);
        }
    }

    void OnEnable ()
    {
        MyRigidBody.isKinematic = false;
    }
    void OnDisable ()
    {
        MyRigidBody.isKinematic = true;
    }
    void OnTriggerExit(Collider _coll)
    {
        if (_coll.CompareTag("GameController"))
        {
            MyRigidBody.useGravity = false;
            gameObject.SetActive(false);
        }
    }
}
