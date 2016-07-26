using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DeviceManager : MonoBehaviour {
    public BaseDeviceManager kinect;
    public BaseDeviceManager leap;
    public BaseDeviceManager steamvr;

    public KinectInit kinectInit;
    
    public AccuracyTester accuracyTeser;


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

    public DeviceTypes device = DeviceTypes.SteamVR;
    public ActivatorTypes activator = ActivatorTypes.Pinch;

    public bool showModels = true;

    private List<Vector3> positionsStart = new List<Vector3>();
    private List<Vector3> positionsEnd = new List<Vector3>();

    public DragObject dragObject;
    public GameObject dropTarget;
    public GameObject dragStart;
    public GameObject control;
    public RenderLine line;
    public GameObject DragDropObjects;

    public TestManager testManager;
    public bool training = true;

    private int positionCounter = 0;

    // Use this for initialization
    void Start()
    {
        //1 = links -> rechts
        positionsStart.Add(new Vector3(-0.3f, 0.0f, 0.0f));
        positionsEnd.Add(new Vector3(0.3f, 0.0f, 0.0f));

        //2 = rechts -> links
        positionsStart.Add(new Vector3(0.3f, 0.0f, 0.0f));
        positionsEnd.Add(new Vector3(-0.3f, 0.0f, 0.0f));

        //----------------------


        //3 = obenlinks -> untenrechts
        positionsStart.Add(new Vector3(-0.26f, 0.15f, 0.0f));
        positionsEnd.Add(new Vector3(0.26f, -0.15f, 0.0f));

        //4 = untenlinks -> obenrechts
        positionsStart.Add(new Vector3(-0.26f, -0.15f, 0.0f));
        positionsEnd.Add(new Vector3(0.26f, 0.15f, 0.0f));

        //5 = untenrechts -> obenlinks
        positionsStart.Add(new Vector3(0.26f, -0.15f, 0.0f));
        positionsEnd.Add(new Vector3(-0.26f, 0.15f, 0.0f));

        //6 = obenrechts -> untenlinks
        positionsStart.Add(new Vector3(0.26f, 0.15f, 0.0f));
        positionsEnd.Add(new Vector3(-0.26f, -0.15f, 0.0f));

        //----------------------

        //7 = vornelinks - hintenrechts
        positionsStart.Add(new Vector3(-0.26f, 0.0f, 0.0f));
        positionsEnd.Add(new Vector3(0.26f, 0.0f, 0.3f));

        //8 = hintenlinks -> vornerechts
        positionsStart.Add(new Vector3(-0.26f, 0.0f, 0.3f));
        positionsEnd.Add(new Vector3(0.26f, 0.0f, 0.0f));
        
        //9 = hintenrechts -> vornelinks
        positionsStart.Add(new Vector3(0.26f, 0.0f, 0.3f));
        positionsEnd.Add(new Vector3(-0.26f, 0.0f, 0.0f));

        //10 = vornerechts -> hintenlinks
        positionsStart.Add(new Vector3(0.26f, 0.0f, 0.0f));
        positionsEnd.Add(new Vector3(-0.26f, 0.0f, 0.3f));

    }

    public void Hide()
    {
        DragDropObjects.SetActive(false);
    }

    public void SetNewPositions()
    {
        dragObject.transform.localPosition = positionsStart[positionCounter];
        dragStart.transform.localPosition = positionsStart[positionCounter];
        control.transform.localPosition = positionsStart[positionCounter];

        dropTarget.transform.localPosition = positionsEnd[positionCounter];
        line.Reset();

        //Debug.Log("Distance: " + Vector3.Distance(dragObject.transform.localPosition, dropTarget.transform.localPosition));

        positionCounter = positionCounter + 1;

        if (positionCounter == positionsStart.Count)
            positionCounter = 0;
    }

    public void Reset(DeviceTypes deviceType, ActivatorTypes activatorType, bool showModels)
    {
        device = deviceType;
        activator = activatorType;
        this.showModels = showModels;

        Reset();
    }

    public void Reset()
    {
        DeactivateAllDevices();
        DragDropObjects.SetActive(true);

        positionCounter = 0;
        SetNewPositions();

        accuracyTeser.accuracy = 0.0f;

        BaseDeviceManager activeDevice = null;

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
        activeDevice.ShowModels(showModels);
    }

    public void DeactivateAllDevices()
    {
        kinectInit.gameObject.SetActive(false);

        kinect.Disable();
        steamvr.Disable();
        leap.Disable();
    }

    public void SetUserHeight()
    {
        if (!GameObject.Find("Camera (eye)"))
        {
            Debug.Log("No camera found");
            return;
        }

        Vector3 current = DragDropObjects.transform.position;
        current.y = GameObject.Find("Camera (eye)").transform.position.y - 0.3f;
        DragDropObjects.transform.position = current;
    }

    public void StartKinectInit()
    {
        DeactivateAllDevices();
        kinectInit.gameObject.SetActive(true);
        steamvr.Enable();
        steamvr.ShowModels(true);
        kinect.Enable();
        kinect.ShowModels(true);
    }

    void OnGUI()
    {
        GUILayout.Label("Accuracy: " + accuracyTeser.accuracy.ToString("F1") + "%");

        GUILayout.Label("Models:");
        DoModelGUI();

        GUILayout.Label("Devices:");
        DoDeviceGUI();

        GUILayout.Label("Activators:");
        DoActivatorGUI();

        if (GUILayout.Button("Reset"))
        {
            Reset();
        }

        GUILayout.Space(20.0f);

        if (GUILayout.Button("Kinect Initialization"))
        {
            StartKinectInit();
        }

        if (GUILayout.Button("Set User Height"))
        {
            SetUserHeight();
        }

        GUILayout.Space(20.0f);
        GUILayout.Label(testManager.currentMessage);

        if (GUILayout.Button("Next"))
        {
            testManager.NextAction();
        }

        if (GUILayout.Button("Write Test File"))
        {
            string path = Application.persistentDataPath;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(path + "/testfile.dd");
            bf.Serialize(file, new UserData());
            file.Close();
        }
    }

    private void DoModelGUI()
    {
        GUILayout.BeginHorizontal();

        GUI.color = showModels ? Color.green : Color.white;
        if (GUILayout.Button("Aktiv"))
        {
            showModels = true;
        }

        GUI.color = !showModels ? Color.green : Color.white;
        if (GUILayout.Button("Inaktiv"))
        {
            showModels = false;
        }

        GUI.color = Color.white;

        GUILayout.EndHorizontal();
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
