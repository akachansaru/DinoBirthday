using UnityEngine;
using System.Collections;

public class Perspective : MonoBehaviour {

    void Update() {
        transform.localPosition = new Vector3(((-20f / 110f) * transform.parent.position.x) + 0f, 
            -transform.parent.position.y, transform.localPosition.z);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
