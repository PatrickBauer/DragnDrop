using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SteamVRPinchActivator : DragDropActivator
{
    //private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get {
            if ((int)trackedObj.index < 0 || (int)trackedObj.index > 10)
                return null; 

            return SteamVR_Controller.Input((int)trackedObj.index);
        }}

    private SteamVR_TrackedObject trackedObj;

    void OnEnable()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate()
    {
        if (controller == null) return;        

        if (controller.GetPressDown(triggerButton))
        {
            isActive = true;
            changedSinceLastFrame = true;
            return;
        }

        if (controller.GetPressUp(triggerButton))
        {
            isActive = false;
            changedSinceLastFrame = true;
            return;
        }

        changedSinceLastFrame = false;
    }
}