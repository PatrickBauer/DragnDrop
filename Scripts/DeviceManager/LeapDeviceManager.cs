using UnityEngine;
using System.Collections;

public class LeapDeviceManager : DeviceManager {

    public DragDropActivator PinchDetector_L;
    public DragDropActivator PinchDetector_R;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    override public void AddPinch()
    {
        left.GetComponent<DragInitiator>().externalActivator = PinchDetector_L;
        right.GetComponent<DragInitiator>().externalActivator = PinchDetector_R;
    }
}
