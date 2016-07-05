using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour {
    public DeviceManager kinect;
    public DeviceManager leap;
    public DeviceManager steamvr;

    public KinectInit kinectInit;
    
    public AccuracyTester accuracyTeser;
    public DragObject dragObject;

    public enum DeviceTypes
    {
        SteamVR,
        Kinect,
        Leap
    }

    public enum ActivatorTypes
    {
        Pinch,
        Touch,
        Speech
    }

    protected DeviceTypes device = DeviceTypes.SteamVR;
    protected ActivatorTypes activator = ActivatorTypes.Pinch;

    // Use this for initialization
    void Start () {
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Reset()
    {
        DeactivateAllDevices();
        dragObject.Reset();
        accuracyTeser.accuracy = 0.0f;

        DeviceManager activeDevice = null;

        switch (device)
        {
            case DeviceTypes.SteamVR:
                activeDevice = steamvr;
                break;
            case DeviceTypes.Kinect:
                activeDevice = kinect;
                break;
            case DeviceTypes.Leap:
                activeDevice = leap;
                break;
            default:
                return;
        }

        activeDevice.Enable();

        switch (activator)
        {
            case ActivatorTypes.Pinch:
                activeDevice.AddPinch();
                break;
            case ActivatorTypes.Touch:
                activeDevice.AddTouch();
                break;
            case ActivatorTypes.Speech:
                activeDevice.AddSpeech();
                break;
            default:
                return;
        }

        activeDevice.ResetActivator();
    }

    void DeactivateAllDevices()
    {
        kinectInit.gameObject.SetActive(false);

        kinect.Disable();
        steamvr.Disable();
        leap.Disable();
    }

    void StartKinectInit()
    {
        DeactivateAllDevices();
        kinectInit.gameObject.SetActive(true);
        steamvr.Enable();
        kinect.Enable();
    }

    void OnGUI()
    {
        GUILayout.Label("Accuracy: " + accuracyTeser.accuracy.ToString("F1") + "%");

        GUILayout.Label("Device");
        DoDeviceGUI();

        GUILayout.Label("Activator");
        DoActivatorGUI();

        if (GUILayout.Button("Reset"))
        {
            Reset();
        }

        if (GUILayout.Button("Kinect Initialization"))
        {
            StartKinectInit();
        }
    }

    private void DoDeviceGUI()
    {
        GUILayout.BeginHorizontal();

        GUI.color = device == DeviceTypes.SteamVR ? Color.green : Color.white;
        if (GUILayout.Button("SteamVR"))
        {
            device = DeviceTypes.SteamVR;
        }

        GUI.color = device == DeviceTypes.Kinect ? Color.green : Color.white;
        if (GUILayout.Button("Kinect"))
        {
            device = DeviceTypes.Kinect;
        }

        GUI.color = device == DeviceTypes.Leap ? Color.green : Color.white;
        if (GUILayout.Button("Leap Motion"))
        {
            device = DeviceTypes.Leap;
        }

        GUI.color = Color.white;

        GUILayout.EndHorizontal();
    }

    private void DoActivatorGUI()
    {
        GUILayout.BeginHorizontal();

        GUI.color = activator == ActivatorTypes.Pinch ? Color.green : Color.white;
        if (GUILayout.Button("Pinch"))
        {
            activator = ActivatorTypes.Pinch;
        }

        GUI.color = activator == ActivatorTypes.Speech ? Color.green : Color.white;
        if (GUILayout.Button("Speech"))
        {
            activator = ActivatorTypes.Speech;
        }

        GUI.color = activator == ActivatorTypes.Touch ? Color.green : Color.white;
        if (GUILayout.Button("Touch"))
        {
            activator = ActivatorTypes.Touch;
        }

        GUI.color = Color.white;

        GUILayout.EndHorizontal();
    }


}
