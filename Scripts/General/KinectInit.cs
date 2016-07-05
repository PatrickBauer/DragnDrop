using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KinectInit : MonoBehaviour
{
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get {
            if ((int)firstControllerTracked.index <= 0 || (int)firstControllerTracked.index >= 10)
                return null;

            return SteamVR_Controller.Input((int)firstControllerTracked.index);
        } }

    public SteamVR_TrackedObject firstControllerTracked;
    public SteamVR_TrackedObject secondControllerTracked;

    public CubemanController cubeman;
    public GameObject indicator;

    public bool active = false;
    public GameObject toDeactivated;

    void OnEnable()
    {
        if (toDeactivated)
            toDeactivated.SetActive(true);

        active = true;
    }
    
    void OnDisable()
    {
        if(toDeactivated)
            toDeactivated.SetActive(false);

        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K)) active = !active;

        //if (!active)
        //    toDeactivated.SetActive(false);
        //else
        //    toDeactivated.SetActive(true);
        
        if (!active || controller == null) return;

        //get position between
        Vector3 middle = Vector3.Lerp(firstControllerTracked.transform.position, secondControllerTracked.transform.position, 0.5f);
        indicator.transform.position = middle;

        if (controller.GetPressDown(triggerButton))
        {
            cubeman.ResetAt(middle);
        }
    }


}