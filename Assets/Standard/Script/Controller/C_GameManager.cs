using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class C_GameManager : MonoBehaviour
{
    public int RespawnProgress = 0;
    public Vector3 RespawnPoint;
    public GameObject Player;
    public Transform Goal;
    public int MultiTouchLimit = 4;
    private Animator MyAnimator;
    [SerializeField]
    private C_Button[] Buttons;

    //C_GameManager
    public void Load ()
    {
    }

    public void Save ()
    {
    }

    void Touch_Mobile ()
    {
        int _touchCount = Mathf.Min(Input.touchCount, MultiTouchLimit);
        if (_touchCount > 0)
        {
            for(int i=0; i<_touchCount; i++)
            {
                ButtonCast(Input.GetTouch(i).position);
            }
        }
    } 

    void Touch_PC ()
    {
        if (Input.GetMouseButton(0))
        {
            ButtonCast(Input.mousePosition);
        }
    }

    void ButtonCast(Vector3 _mp)
    {
        Ray _CameraRay = Camera.main.ScreenPointToRay(_mp);
        RaycastHit _HitObj;
        if (Physics.Raycast(_CameraRay, out _HitObj, 100f))
        {
            foreach (C_Button item in Buttons)
            {
                if (item.gameObject.Equals(_HitObj.transform.gameObject))
                {
                    item.Excute();
                    return;
                }
            }
        }

    }

    //MonoBehaviour
    void Awake ()
    {
        MyAnimator = GetComponent<Animator>();
        Buttons = GetComponentsInChildren<C_Button>();
        if (Buttons.Length == 0)
        {
            Debug.Log("WARNING : This map have no Button. This Level will cannot Play.");
        }
        if (Player == null)
        {
            Debug.Log("WARNING : This map have no Player. This Level will cannot Play.");
        }

    }

    void Start ()
    {
        if (Player != null)
            RespawnPoint =  Player.transform.position;
    }

    void FixedUpdate ()
    {
        Touch_PC();
        //Touch_Mobile();
    }

}
