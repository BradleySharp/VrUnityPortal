using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnCollision : MonoBehaviour {

    public Camera MainCamera = null;
    public Transform OtherPortal = null;
    public Camera PortalCamera = null;

    // Use this for initialization
    void Start ()
    {
        var col = gameObject.GetComponent<MeshCollider>();
    }

    void OnCollisionEnter(Collision col)
    {
        MainCamera.transform.position = OtherPortal.transform.position;
        MainCamera.transform.rotation = PortalCamera.transform.rotation;
        //Above will add a push going through the portal, below wont.
        /*var rot = MainCamera.transform.rotation;
        MainCamera.transform.Rotate(rot.x, rot.y + 180, rot.z);*/
    }

    // Update is called once per frame
    void Update () {
		
	}
}
