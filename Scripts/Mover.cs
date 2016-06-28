using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public GameObject a;
    public GameObject b;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.1f * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.1f * Time.deltaTime, 0, 0);
        }


        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0.1f * Time.deltaTime, 0);
        }


        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -0.1f * Time.deltaTime, 0);
        }


        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(0, 0, 0.1f * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(0, 0, -0.1f * Time.deltaTime);
        }

        transform.position = ClosestPointOnLine(a.transform.position, b.transform.position, transform.position);
    }

    Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint) {
        var vVector1 = vPoint - vA;
        var vVector2 = (vB - vA).normalized;

        var d = Vector3.Distance(vA, vB);
        var t = Vector3.Dot(vVector2, vVector1);

        if (t <= 0)
            return vA;

        if (t >= d)
            return vB;

        var vVector3 = vVector2 * t;
        var vClosestPoint = vA + vVector3;

        return vClosestPoint;
    }
}
