using UnityEngine;
using System.Collections;

public class HandMenu : MonoBehaviour {
    public DeviceManager deviceManager;

    public UnityEngine.UI.Dropdown deviceDropdown;
    public UnityEngine.UI.Dropdown activatorDropdown;
    public UnityEngine.UI.Toggle showModelToggle;

    public SteamVR_TrackedObject controller;
    public Wacki.ViveUILaserPointer laser;

    public bool isOn = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if((int) controller.index >= 0)
        {
            if(SteamVR_Controller.Input((int)controller.index).GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
            {
                isOn = !isOn;

                laser.enabled = isOn;

                if (laser.hitPoint && laser.pointer)
                {
                    laser.hitPoint.SetActive(isOn);
                    laser.pointer.SetActive(isOn);
                }


                GetComponent<Canvas>().enabled = isOn;
            }
        }
	}

    public void Reset()
    {
        deviceManager.Reset((DeviceManager.DeviceTypes)deviceDropdown.value, (DeviceManager.ActivatorTypes)activatorDropdown.value, showModelToggle.isOn);
    }
}
