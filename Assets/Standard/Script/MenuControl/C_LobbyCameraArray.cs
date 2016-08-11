using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_LobbyCameraArray : MonoBehaviour
{
    [SerializeField]
    private Camera CameraUsed;
    public Transform[] CameraPositionGoal;
    public int CameraPositionIndex;

    private Quaternion LastRotation;
    private Vector3 LastPosition;
    private float Clamp;

    public void SetCameraArray(int _array)
    {
        CameraPositionIndex = _array;
        LastRotation = CameraUsed.transform.rotation;
        LastPosition = CameraUsed.transform.position;
        Clamp = 0;
    }
    public void SetCameraArray(bool _IsAdd)
    {
        if (_IsAdd)
        {
            CameraPositionIndex++;
        }
        else CameraPositionIndex--;

        if (CameraPositionIndex < 0)
        {
            CameraPositionIndex = CameraPositionGoal.Length - 1;
        }
        else if (CameraPositionIndex >= CameraPositionGoal.Length)
        {
            CameraPositionIndex = 0;
        }
        LastRotation = CameraUsed.transform.rotation;
        LastPosition = CameraUsed.transform.position;
        Clamp = 0;
    }
    void Awake ()
    {
        LastPosition = CameraUsed.transform.position;
        LastRotation = CameraUsed.transform.rotation;
        Clamp = 1;
    }
    void FixedUpdate ()
    {
        if (Clamp < 1) Clamp += 0.01f;
        CameraUsed.transform.position = Vector3.Slerp(LastPosition, CameraPositionGoal[CameraPositionIndex].position, Clamp);
        CameraUsed.transform.rotation = Quaternion.Slerp(LastRotation, CameraPositionGoal[CameraPositionIndex].rotation, Clamp);
    }
}
