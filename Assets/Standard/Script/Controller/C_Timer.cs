using UnityEngine;
using System.Collections;


public class C_Timer : MonoBehaviour
{
    public enum Timer_Mode { REPEAT, ONCE };
    public Timer_Mode CurrentTimer_Mode = Timer_Mode.ONCE;
    [SerializeField]
    private bool On= false;
    [SerializeField]
    private int Time = 0;
    [SerializeField]
    private float TimeTick = 1f;
    [SerializeField]
    private Node[] TimeSheet;
    [SerializeField]
    private Coroutine MyCoroutine;

    [System.Serializable]
    private class Node
    {
        public C_Command Command;
        public int Time;
    }

    //C_Timer
    public void SetTimePosition(int _a)
    {
        Time = _a;
    }
    public void SetTimeTick(float _a)
    {
        TimeTick = _a;
    }
    void NodeCheck(int _t)
    {
        int a;
        for (a = 0; a<TimeSheet.Length; a++)
        {
            int _ct = TimeSheet[a].Time;
            if (_ct == _t)
            {
                Debug.Log(gameObject.name + ".C_Timer is Play Event[" + a+"]");
                if (TimeSheet[a].Command.enabled == true)
                    TimeSheet[a].Command.Event.Invoke();
                break;
            }
        }
        if (a == TimeSheet.Length - 1 && CurrentTimer_Mode == Timer_Mode.REPEAT)
            Time = 0;
    }
    void StartTimer ()
    {
        On = true;
        MyCoroutine = StartCoroutine(TimerCoroutine());
    }
    void StopTimer ()
    {
        On = false;
        StopCoroutine(MyCoroutine);
    }
    //MonoBehaviour
    void Start ()
    {
        if (On) MyCoroutine = StartCoroutine(TimerCoroutine());
    }
    IEnumerator TimerCoroutine()
    {
        while(true)
        {
            NodeCheck(Time++);
            yield return new WaitForSeconds(TimeTick);
        }
    }
}
