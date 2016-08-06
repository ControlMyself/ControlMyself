using UnityEngine;
using System.Collections;

public class IngameComp : MonoBehaviour
{
    [SerializeField]
    protected bool On;
    [HideInInspector]
    public Collider MyCollider;
    [HideInInspector]
    public Rigidbody MyRigidBody;

    public bool GetOn()
    {
        return On;
    }
    public void SetOn(bool state)
    {
        On = state;
    }
    public void SwapOn()
    {
        On = !On;
    }

}
