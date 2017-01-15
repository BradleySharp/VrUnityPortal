using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportScript : MonoBehaviour {

    public Camera MainCamera = null;
    public Transform Portal1 = null;
    public Transform Portal2 = null;

    public MeshCollider ColliderRight;

    // Use this for initialization
    void Start () {
		
	}
    // Update is called once per frame

    private void Update()
    {
        var x = Mathf.Round(MainCamera.transform.position.x * 1);
        var x2 = Mathf.Round(Portal1.transform.position.x * 1);
        var x3 = Mathf.Round(Portal2.transform.position.x * 1);
        var z = Mathf.Round(MainCamera.transform.position.z * 1);
        var z2 = Mathf.Round(Portal1.transform.position.z * 1);
        var z3 = Mathf.Round(Portal2.transform.position.z * 1);

        if (z == z2 && x == x2)
        {
            MainCamera.transform.position = Portal2.transform.position;
            //MainCamera.transform.rotation = Portal2.transform.rotation;
            var rot = MainCamera.transform.rotation;
            MainCamera.transform.Rotate(rot.x, rot.y + 180, rot.z);
        }
        else if (z == z3 && x == x3)
        {
            MainCamera.transform.position = Portal1.transform.position;
            //MainCamera.transform.rotation = Portal1.transform.rotation;
            var rot = MainCamera.transform.rotation;
            MainCamera.transform.Rotate(rot.x, rot.y + 180, rot.z);
        }
    }
}
