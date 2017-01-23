using UnityEngine;
using System.Collections;

public class letter : MonoBehaviour {

	public float maxdeltay;
	public float ospeed;
	public float ypos;
	//public float rotscale;
	
	private float theta;
	private float fromdegrees;

	public void quit() {
		Application.Quit ();
	}
	
	void Start() {
		fromdegrees = Mathf.PI / 180F;
		theta = 0F;
	}
	
	void Update () {
		theta += ospeed * Time.deltaTime;
		if (theta > 360F) {
			theta -= 360F;
		}
		transform.position = new Vector3(transform.position.x, ypos + maxdeltay * Mathf.Sin(theta * fromdegrees), transform.position.z);
		//transform.Rotate(new Vector3(0F, 0F, (maxdeltay * Mathf.Sin(theta * fromdegrees))/rotscale));
	}
}