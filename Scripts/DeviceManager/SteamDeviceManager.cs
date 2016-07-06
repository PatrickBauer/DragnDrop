using UnityEngine;
using System.Collections;

public class SteamDeviceManager : DeviceManager
{

    override public void AddPinch() {
        left.AddComponent<SteamVRPinchActivator>();
        right.AddComponent<SteamVRPinchActivator>();
    }
}
