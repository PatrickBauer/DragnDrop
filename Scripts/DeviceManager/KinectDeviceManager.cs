using UnityEngine;
using System.Collections;

public class KinectDeviceManager : DeviceManager {

    public DragDropActivator KinectPinch_L;
    public DragDropActivator KinectPinch_R;

    override public void AddPinch()
    {
        left.GetComponent<DragInitiator>().externalActivator = KinectPinch_L;
        right.GetComponent<DragInitiator>().externalActivator = KinectPinch_R;
    }
}
