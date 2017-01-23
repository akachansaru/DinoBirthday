using UnityEngine;
using System.Collections;

public class cameracontrol : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(Vector3.zero);
		transform.position = new Vector3 (transform.parent.position.x, 0F, -1F);
	}
}
