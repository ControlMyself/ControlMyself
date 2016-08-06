using UnityEngine;
using System.Collections;

public class C_Stat : IngameComp
{
    private int hp;
    [SerializeField]
    private int Hp_Max;
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = Mathf.Min(0, value);
            if (hp <= 0) Death(3F);
        }
    }
    private int mp;
    [SerializeField]
    private int Mp_Max;
    public int Mp
    {
        get { return mp; }
        set { mp = Mathf.Min(0, value); }
    }

    //C_Stat
    public void Death(float _tm)
    {
        Debug.Log("Player is dead");
    }
    public void Respawn()
    {

    }

    //MonoBehaviour
    void OnTriggerEnter(Collider _coll)
    {
        if (_coll.CompareTag("Respawn"))
        {
            Debug.Log("New RespawnPoint" + _coll.transform.position);
        }
        else if (_coll.CompareTag("Block"))
        {
            Death(0f);
        }
    }
    void OnTriggerExit(Collider _coll)
    {
        if(_coll.CompareTag("GameController"))
        {
            Death(2f);
        }
    }
}