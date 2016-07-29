using UnityEngine;
using System.Collections;

public class KinectPinchActivator : DragDropActivator
{
    public GameObject A;
    public GameObject B;

    public float startDistance = 0.09f;
    public float stopDistance = 0.10f;

    public float currentDistance = 0.0f;

    UnityEngine.UI.Text kinectText;
    Transform lookTarget;


    // Use this for initialization
    override public void Start () {
        base.Start();

        if (GameObject.Find("KinectText"))
            kinectText = GameObject.Find("KinectText").GetComponent<UnityEngine.UI.Text>();

        if(GameObject.Find("Camera (eye)"))
            lookTarget = GameObject.Find("Camera (eye)").transform;
    }

    // Update is called once per frame
    override public void Update () {
        base.Update();

        float distance = Vector3.Distance(A.transform.position, B.transform.position);
        currentDistance = distance;

        //draw text
        if (kinectText)
        {
            kinectText.text = distance.ToString();
            kinectText.transform.LookAt(lookTarget);
            kinectText.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        }


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
