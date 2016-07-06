using UnityEngine;
using System.Collections;

public class RenderLine : MonoBehaviour {

    public GameObject start;
    public GameObject target;

	// Use this for initialization
	void Start () {
        transform.position = Vector3.Lerp(start.transform.position, target.transform.position, 0.5f);
        transform.LookAt(target.transform);
        transform.Rotate(90, 0, 0);

        Vector3 scale = transform.localScale;
        scale.y = (Mathf.Abs(start.transform.position.x - target.transform.position.x) / 2.0f);
        transform.localScale = scale;
    }
}
