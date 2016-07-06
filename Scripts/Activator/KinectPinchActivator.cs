using UnityEngine;
using System.Collections;

public class KinectPinchActivator : DragDropActivator
{
    private int lastState = 0;

    public GameObject A;
    public GameObject B;

    public float startDistance = 0.03f;
    public float stopDistance = 0.04f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(A.transform.position, B.transform.position);

        if (lastState == 0 && distance <= startDistance)
        {
            isActive = true;
            changedSinceLastFrame = true;
            lastState = 1;
            return;
        }

        if (lastState == 1 && distance >= stopDistance)
        {
            isActive = false;
            changedSinceLastFrame = true;
            lastState = 0;
            return;
        }

        changedSinceLastFrame = false;
    }
}
