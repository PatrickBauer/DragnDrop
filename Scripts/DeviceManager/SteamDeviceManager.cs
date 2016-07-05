using UnityEngine;
using System.Collections;

public class SteamDeviceManager : DeviceManager
{

    override public void Enable() { }

    override public void Disable() {
        left.GetComponent<DragInitiator>().externalActivator = null;
        right.GetComponent<DragInitiator>().externalActivator = null;

        if (left.GetComponent<DragDropActivator>())
            Destroy(left.GetComponent<DragDropActivator>());

        if (right.GetComponent<DragDropActivator>())
            Destroy(right.GetComponent<DragDropActivator>());
    }

    override public void AddPinch() {
        left.AddComponent<SteamVRPinchActivator>();
        right.AddComponent<SteamVRPinchActivator>();
    }
}
