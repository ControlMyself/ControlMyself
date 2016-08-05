using UnityEngine;
using System.Collections;

public class C_PlayerCtrl : IngameComp
{

    private C_GameManager Sys;
    public float Speed = 1f;
    public float JumpPower = 3f;
    [SerializeField]
    private float SlopeLimit = 45f;
    [SerializeField]
    private bool LookRight = true;
    [SerializeField]
    private bool OnGround = true;
    [SerializeField]
    private bool IsDeath = false;

    //C_PlayerMove
    public void Jump()
    {
        if (OnGround && !IsDeath)
        {
            MyRigidBody.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
            OnGround = false;
        }
    }
    public void MoveLeft()
    {
        if (!IsDeath)
        {
            if (LookRight) LookRight = false;
            MyRigidBody.MovePosition(transform.position + Vector3.left * Speed * Time.deltaTime);
        }

    }
    public void MoveRight()
    {
        if (!IsDeath)
        {
            if (!LookRight) LookRight = true;
            MyRigidBody.MovePosition(transform.position + Vector3.right * Speed * Time.deltaTime);
        }
    }
    public void death ()
    {
        IsDeath = true;
        Sys.GetComponent<Animator>().SetBool("Reset",true);
        Invoke("Revive", 3f);
    }
    public void Revive ()
    {
        IsDeath = false;
    }

    //MonoBehaviour
    void Awake()
    {
        Sys = GetComponentInParent<C_GameManager>();
        if (Sys == null)
        {
            Destroy(this);
        }
        MyCollider = GetComponent<Collider>();
        bool _trigger = false;
        if (MyCollider == null)
        {
            Debug.Log("Collider Componant not Found. " + gameObject.name + ".C_PlayerMove Componant Destroyed.");
            _trigger = true;
        }
        MyRigidBody = GetComponent<Rigidbody>();
        if (MyRigidBody == null)
        {
            Debug.Log("RigidBody Componant not Found. " + gameObject.name + ".C_PlayerMove Componant Destroyed.");
            _trigger = true;
        }
        if (_trigger) Destroy(this);
    }

    void OnCollisionStay(Collision _coll)
    {
        if (_coll.gameObject.CompareTag("Block"))
        {
            int _count = _coll.contacts.Length;
            if (!IsDeath)
            {
                if (Physics.CheckSphere(transform.position, 0.05f, 1 << LayerMask.NameToLayer("Ground"), QueryTriggerInteraction.Ignore))
                {
                    death();
                }
            }
            for (int i=0; i< _count; i++)
            {
                Vector3 _CollisionNormal = _coll.contacts[i].normal;
                if (Vector3.Angle(_CollisionNormal, Vector3.up) < SlopeLimit)
                {
                    OnGround = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider _coll)
    {
        if (_coll.CompareTag("Respawn"))
        {
            Sys.Save();
        }
    }

    void OnTriggerExit(Collider _coll)
    {
        if (_coll.CompareTag("GameController"))
        {
            death();
        }
    }
}

