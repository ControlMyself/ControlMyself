using UnityEngine;
using UnityEditor;
using ExSaveSys.Components;

[CustomEditor(typeof (GUIDGen))]
public class GuidEdtr : Editor{
    
	public override void OnInspectorGUI()
	{
		if(GUILayout.Button ("Generate IDs"))
		{
			GameObject[] obs = GameObject.FindObjectsOfType<GameObject>();
			foreach(GameObject go in obs)
			{
				if(!go.GetComponent<ID>())
				{
					go.AddComponent<ID>();
					go.GetComponent<ID>().id = System.Convert.ToString(System.Guid.NewGuid());
				}else if(go.GetComponent<ID>().id == "")
				{
					go.GetComponent<ID>().id = System.Convert.ToString(System.Guid.NewGuid());
				}
                if (go.GetComponent<Camera>() && !go.GetComponent<SerializableCamera>())
                {
                    SerializableCamera SC = go.AddComponent<SerializableCamera>();
                    SC.clearFlags = go.GetComponent<Camera>().clearFlags;
                    SC.depth = go.GetComponent<Camera>().depth;
                    SC.farClipPlane = go.GetComponent<Camera>().farClipPlane;
                    SC.fieldOfView = go.GetComponent<Camera>().fieldOfView;
                    SC.hdr = go.GetComponent<Camera>().hdr;
                    SC.nearClipPlane = go.GetComponent<Camera>().nearClipPlane;
                    SC.useOcclusionCulling = go.GetComponent<Camera>().useOcclusionCulling;
                    
                }
                if (go.GetComponent<Rigidbody>() && !go.GetComponent<SerializableRigidBoby>())
                {
                    SerializableRigidBoby SRB = go.AddComponent<SerializableRigidBoby>();
                        SRB.drag = go.GetComponent<Rigidbody>().drag;
                        SRB.angularDrag = go.GetComponent<Rigidbody>().angularDrag;
                        SRB.collisionDetectionMode = go.GetComponent<Rigidbody>().collisionDetectionMode;
                        SRB.interpolation = go.GetComponent<Rigidbody>().interpolation;
                        SRB.isKinematic = go.GetComponent<Rigidbody>().isKinematic;
                        SRB.mass = go.GetComponent<Rigidbody>().mass;
                        SRB.useGravity = go.GetComponent<Rigidbody>().useGravity;
                }
                if (go.GetComponent<Light>() && !go.GetComponent<SerializableLight>())
                {
                    SerializableLight SL = go.AddComponent<SerializableLight>();
                    SL.bounceIntensity = go.GetComponent<Light>().bounceIntensity;
                    SL.intensity = go.GetComponent<Light>().intensity;
                    SL.renderMode = go.GetComponent<Light>().renderMode;
                    SL.shadowStrength = go.GetComponent<Light>().shadowStrength;
                    SL.shadows = go.GetComponent<Light>().shadows;
                    SL.type = go.GetComponent<Light>().type;
                }

			}
		}
				if(GUILayout.Button ("Reset GUID"))
				{
						GameObject[] obs = GameObject.FindObjectsOfType<GameObject>();
						foreach(GameObject go in obs)
						{
								if (go.GetComponent<ID> ()) {
                    DestroyImmediate(go.GetComponent<ID>(), false);
								}
						}
				}
	}
}
