using UnityEngine;
using System.Collections;

public class C_LobbyCameraArray : MonoBehaviour
    {
    [SerializeField]
    private Camera CameraUsed;
    public Transform[] CameraPositionAdress;
    private int CameraPositionAdressIndex;

    public int CameraPosition()
    {
        return CameraPositionAdressIndex;
    }
    public void CameraPosition(int _index)
    {
        CameraPositionAdressIndex = _index;
        CameraUsed.transform.position = Vector3.MoveTowards(CameraUsed.transform.position , CameraPositionAdress[_index].position , 1);
        CameraUsed.transform.rotation = Quaternion.RotateTowards(CameraUsed.transform.rotation, CameraPositionAdress[_index].rotation, 1);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
