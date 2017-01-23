using UnityEngine;
using System.Collections;

public class perspective : MonoBehaviour {
	
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3 (((-20F/110F)*transform.parent.position.x) + 0F, - transform.parent.position.y, transform.localPosition.z);
		transform.rotation = Quaternion.Euler(Vector3.zero);
	}
}
