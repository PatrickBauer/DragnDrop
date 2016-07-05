using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Basic implementation of how to use a Vive controller as an input device.
 * Can only interact with items with InteractableBase component
 */
public class SteamVRDragInitiator : DragInitiator
{
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

   

    private SteamVR_TrackedObject trackedObj;


    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

       
    }

}