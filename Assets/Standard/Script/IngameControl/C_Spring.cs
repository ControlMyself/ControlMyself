using UnityEngine;
using System.Collections;

public class C_Spring : IngameComp
{
    public Vector3 ForceVector = Vector3.zero;

    //MonoBehaviour
    void Awake()
    {
        MyCollider = GetComponent<Collider>();
        if (MyCollider == null)
        {
            Debug.Log("Rigidbody Componant not Found. " + gameObject.name + ".C_Spring Componant Destroyed.");
            Destroy(this);
        }
    }
    void OnTriggerEnter(Collider _coll)
    {
        if (_coll.CompareTag("Player"))
        {
            Debug.Log("SpringJoint");
            Rigidbody _ColRigidBody = _coll.gameObject.GetComponent<Rigidbody>();
            if (_ColRigidBody != null)
            {
                Debug.Log("SpringJoint");
                _ColRigidBody.velocity = ForceVector;
            }
        }
    }
}
