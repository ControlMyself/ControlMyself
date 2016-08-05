using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class C_Timer : IngameComp
{
    public enum Timer_Mode { REPEAT, ONCE };
    public Timer_Mode CurrentTimer_Mode = Timer_Mode.ONCE;
    [SerializeField]
    private int Time = 0;
    [SerializeField]
    private float TimeTick = 1f;
    [SerializeField]
    private List<Node> TimeSheet = new List<Node>();
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
        for (a = 0; a<TimeSheet.Count; a++)
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
        if (a == TimeSheet.Count - 1 && CurrentTimer_Mode == Timer_Mode.REPEAT)
            Time = 0;
    }

    void StartTimer ()
    {
        if (GetOn())
            MyCoroutine = StartCoroutine(TimerCoroutine());
    }
    void StopTimer ()
    {
        SetOn(false);
        StopCoroutine(MyCoroutine);
    }
    //MonoBehaviour
    void Start ()
    {
        StartTimer();
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
