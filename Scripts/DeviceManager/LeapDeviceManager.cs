using UnityEngine;
using System.Collections;

public class LeapDeviceManager : DeviceManager {

    public DragDropActivator PinchDetector_L;
    public DragDropActivator PinchDetector_R;

    override public void AddPinch()
    {
        left.GetComponent<DragInitiator>().externalActivator = PinchDetector_L;
        right.GetComponent<DragInitiator>().externalActivator = PinchDetector_R;
    }
}
