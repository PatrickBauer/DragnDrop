﻿using UnityEngine;
using System.Collections;

public class SteamDeviceManager : DeviceManager
{
    public GameObject leftController;
    public GameObject rightController;
    
    override public void AddPinch() {
        leftController.AddComponent<SteamVRPinchActivator>();
        rightController.AddComponent<SteamVRPinchActivator>();

        left.GetComponent<DragInitiator>().externalActivator = leftController.GetComponent<DragDropActivator>();
        right.GetComponent<DragInitiator>().externalActivator = rightController.GetComponent<DragDropActivator>();
    }

    override public void ShowModels(bool showModels)
    {
        if(showModels)
        {
            leftController.transform.Find("Model").gameObject.SetActive(true);
            rightController.transform.Find("Model").gameObject.SetActive(true);
        } else
        {
            leftController.transform.Find("Model").gameObject.SetActive(false);
            rightController.transform.Find("Model").gameObject.SetActive(false);
        }
    }
}
