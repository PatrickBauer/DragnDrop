using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
    public GameObject dragObject;
    public GameObject start;
    public GameObject end;
    
	void Update () {
        transform.position = ClosestPointOnLine(start.transform.position, end.transform.position, dragObject.transform.position);
    }

    Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
    {
        var vVector1 = vPoint - vA;
        var vVector2 = (vB - vA).normalized;

        //var d = Vector3.Distance(vA, vB);
        var t = Vector3.Dot(vVector2, vVector1);

        //if (t <= 0)
        //    return vA;

        //if (t >= d)
        //    return vB;

        var vVector3 = vVector2 * t;
        var vClosestPoint = vA + vVector3;

        return vClosestPoint;
    }
}

