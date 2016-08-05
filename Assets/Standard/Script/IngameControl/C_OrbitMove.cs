using UnityEngine;
using System.Collections.Generic;


public class C_OrbitMove : MonoBehaviour
{
    public bool On = false;
    private Rigidbody MyRigidBody;
    public enum ORBIT_Mode { CIRCLE, REPEAT, PENDULUM, ONEWAY};
    public ORBIT_Mode Orbit_ModeCurrent;
    public enum MOVE_Mode { AUTO, REMOTE, STEP };
    public MOVE_Mode Move_ModeCurrent;
    [SerializeField]
    public List<OrbitTransform> OrbitPoint = new List<OrbitTransform>();
    public bool Reverse = false;
    public float Speed = 1f;
    private float Torque = 0f;
    [SerializeField]
    private int OrbitProgressive = 0;

    [System.Serializable]
    public class OrbitTransform
    {
        public Vector3 position = Vector3.zero;
        public Vector3 rotation = Vector3.zero;
        public C_Command pointEvent;
    }

    //C_OrbitMove
    public void SetOn (bool _on)
    {
        On = _on;
    }
    public void SetReverse (bool _rev)
    {
        Reverse = _rev;
    }
    public void SetProgress (int a)
    {
        OrbitProgressive = a;
        transform.position = OrbitPoint[OrbitProgressive].position;
        transform.rotation = Quaternion.Euler(OrbitPoint[OrbitProgressive].rotation);
    }
    public void SetGoal (int a)
    {
        OrbitProgressive = a;
        Torque = Quaternion.Angle(transform.rotation, Quaternion.Euler(OrbitPoint[OrbitProgressive].rotation))
        / (Vector3.Distance(transform.position, OrbitPoint[OrbitProgressive].position) / Speed);
    }
    void DrawOrbit()
    {
        for(int i = 0; i<OrbitPoint.Count-1; i++)
        {
            Debug.DrawLine(OrbitPoint[i].position, OrbitPoint[i+1].position, Color.red, 0f, false);
        }
        if (Orbit_ModeCurrent == ORBIT_Mode.CIRCLE)
            Debug.DrawLine(OrbitPoint[OrbitPoint.Count-1].position, OrbitPoint[0].position, Color.red, 0f, false);
    }
    public void MoveOrbit(ORBIT_Mode _om)
    {
        MyRigidBody.MovePosition(Vector3.MoveTowards(transform.position, OrbitPoint[OrbitProgressive].position, Speed * Time.deltaTime));
        MyRigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(OrbitPoint[OrbitProgressive].rotation), Torque * Time.deltaTime));
        if (transform.position == OrbitPoint[OrbitProgressive].position)
        {
            if (OrbitPoint[OrbitProgressive].pointEvent != null)
            {
                OrbitPoint[OrbitProgressive].pointEvent.Event.Invoke();
            }
            if (Move_ModeCurrent == MOVE_Mode.STEP)
            {
                On = false;
            }
            if (OrbitProgressive == 0 && Reverse)
            {
                switch (_om)
                {
                    case ORBIT_Mode.CIRCLE:
                        OrbitProgressive = OrbitPoint.Count - 1;
                        break;
                    case ORBIT_Mode.REPEAT:
                        SetProgress(OrbitPoint.Count - 1);
                        break;
                    case ORBIT_Mode.PENDULUM:
                        Reverse = !Reverse;
                        break;
                    case ORBIT_Mode.ONEWAY:
                        On = false;
                        break;
                }
            }
            else if (OrbitProgressive == OrbitPoint.Count-1 && !Reverse)
            {
                switch (_om)
                {
                    case ORBIT_Mode.CIRCLE:
                        OrbitProgressive = 0;
                        break;
                    case ORBIT_Mode.REPEAT:
                        SetProgress(0);
                        break;
                    case ORBIT_Mode.PENDULUM:
                        Reverse = !Reverse;
                        break;
                    case ORBIT_Mode.ONEWAY:
                        On = false;
                        break;
                }
            }
            else
            {
                if (Reverse)
                OrbitProgressive--;
                else
                OrbitProgressive++;
            }
            Torque = Quaternion.Angle(transform.rotation, Quaternion.Euler(OrbitPoint[OrbitProgressive].rotation))
                    /(Vector3.Distance(transform.position, OrbitPoint[OrbitProgressive].position) / Speed);
        }
    }

    //MonoBehaviour
    void Awake()
    {
        if (OrbitPoint.Count <= 1)
        {
            Debug.Log("OrbitPoint Is Empty. " + gameObject.name + ".C_OrbitMove Componant Destroyed.");
            Destroy(this);
            return;
        }
        MyRigidBody = GetComponent<Rigidbody>();
        if (MyRigidBody == null)
        {
            Debug.Log("Rigidbody Componant not Found. " + gameObject.name + ".C_OrbitMove Componant Destroyed.");
            Destroy(this);
            return;
        }
        if (OrbitProgressive < 0) OrbitProgressive += OrbitPoint.Count;


        }
        void Update()
        {
            //DrawOrbit();
        }

        void FixedUpdate()
        {
            if ((Move_ModeCurrent == MOVE_Mode.AUTO || Move_ModeCurrent == MOVE_Mode.STEP) && On)
                MoveOrbit(Orbit_ModeCurrent);
        }

}
