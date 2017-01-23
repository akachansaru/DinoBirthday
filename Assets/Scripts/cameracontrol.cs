using UnityEngine;

public class CameraControl : MonoBehaviour {

    void Update() {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.position = new Vector3(transform.parent.position.x, 0f, -1f);
    }
}
