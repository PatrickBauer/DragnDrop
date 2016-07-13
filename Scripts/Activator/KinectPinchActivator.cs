using UnityEngine;
using System.Collections;

public class KinectPinchActivator : DragDropActivator
{
    public GameObject A;
    public GameObject B;

    public float startDistance = 0.065f;
    public float stopDistance = 0.10f;

    UnityEngine.UI.Text kinectText;
    Transform lookTarget;


    // Use this for initialization
    void Start () {
        kinectText = GameObject.Find("KinectText").GetComponent<UnityEngine.UI.Text>();
        lookTarget = GameObject.Find("Camera (eye)").transform;
    }

    // Update is called once per frame
    override public void FixedUpdate () {
        base.FixedUpdate();

        float distance = Vector3.Distance(A.transform.position, B.transform.position);

        //draw text
        kinectText.text = distance.ToString();
        kinectText.transform.LookAt(lookTarget);
        kinectText.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));


        if (!isActive && distance <= startDistance)
        {
            isActive = true;
            changedSinceLastFrame = true;
            return;
        }

        if (isActive && distance >= stopDistance)
        {
            isActive = false;
            changedSinceLastFrame = true;
           return;
        }

        changedSinceLastFrame = false;
    }
}
