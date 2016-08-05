using UnityEngine;
using System.Collections;

public class C_CameraFocus : MonoBehaviour
{
    public Transform Focus;
    public Vector2 LimitMax;
    public Vector2 LimitMin;
    public float Smooth;
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Focus != null)
        {
            transform.position = Vector3.Lerp(transform.position, Focus.position, Time.fixedDeltaTime * Smooth);
            if (transform.position.x > LimitMax.x) transform.position = new Vector3(LimitMax.x, transform.position.y, transform.position.z); 
            if (transform.position.y > LimitMax.y) transform.position = new Vector3(transform.position.x, LimitMax.y, transform.position.z);
            if (transform.position.x < LimitMin.x) transform.position = new Vector3(LimitMin.x, transform.position.y, transform.position.z);
            if (transform.position.y < LimitMin.y) transform.position = new Vector3(transform.position.x, LimitMin.y, transform.position.z);
        }
    }

    //
    public void AreaChange(Vector2 _min,Vector2 _max)
    {
        LimitMax = _max;
        LimitMin = _min;
    }
    public void FocusChange(GameObject _magnet)
    {
        Focus = _magnet.transform;
    }
}
