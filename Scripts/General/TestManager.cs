using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


//9 Durchgänge 

//HTC Pinch(ohne)
//HTC Touch(mit / ohne)

//Leap Pinch(mit)
//Leap Touch(mit / ohne)

//Kinect Pinch(mit)
//Kinect Touch(mit / ohne)

//Übungsperiode - 10x schnell und 10x genau

public class TestManager : MonoBehaviour {

    public DeviceManager deviceManager;
    public AccuracyTester control;
    public DragObject dragObject;
    
    bool dragStarted = false;

    bool isTracking = false;
    
    TrackInstance tracking;

    int trackingsPerVariation = 10;
    int currentTrackingsPerVariation = 0;

    UserData userData;
    
    List<System.Action> actions;
    List<System.Action> shuffleActions;


    int currentAction = 0;
    public string currentMessage = "Welcome";
    string username;

    public UnityEngine.UI.Text menuText;

    // Use this for initialization
    void Start () {
        if (menuText)
            menuText.text = currentMessage;

        //create single UserData object
        userData = new UserData();

        //shuffled Actions
        shuffleActions = new List<System.Action>();

        //shuffleActions.Add(Kinect);
        //shuffleActions.Add(Leap);
        shuffleActions.Add(Steam);

        var shuffledActions = shuffleActions.OrderBy(a => System.Guid.NewGuid());

        //actions
        actions = new List<System.Action>();

        //add starter actions
        actions.Add(Entry);
        actions.Add(KinectInit);
        actions.Add(KinectStop);

        //add shuffle actions
        foreach(var action in shuffledActions)
        {
            actions.Add(action);

            actions.Add(pinch);
            actions.Add(uebung);
            actions.Add(schnell);
            actions.Add(genau);

            actions.Add(touchmit);
            actions.Add(uebung);
            actions.Add(schnell);
            actions.Add(genau);

            actions.Add(touchohne);
            actions.Add(uebung);
            actions.Add(schnell);
            actions.Add(genau);
        }

        //add end actions
        actions.Add(Save);
    }

    public void NextAction()
    {
        if(actions.Count > currentAction)
        {
            System.Action action = actions[currentAction];
            action();

            if(menuText)
                menuText.text = currentMessage;

            currentAction++;
        } else
        {
            Debug.LogError("No more actions");
        }
    }

    void Entry()
    {
        //generate username
        username = Path.GetRandomFileName();
        username = username.Replace(".", "");
        username = username.Substring(0, 3);

        userData.userName = username;
        
        currentMessage = "UserName aufschreiben: " + username;
    }

    void KinectInit()
    {
        currentMessage = "Kinect Init, Next Sets User Height";
        deviceManager.StartKinectInit();
    }


    void KinectStop()
    {
        currentMessage = "User Height Set, Kinect Init done, Tests will start now:";
        deviceManager.DeactivateAllDevices();
        deviceManager.SetUserHeight();
    }

    void Kinect()
    {
        currentMessage = "Kinect - Next zum aktivieren";
        deviceManager.device = DeviceManager.DeviceTypes.Kinect;

    }
    
    void Leap()
    {
        currentMessage = "Leap - Next zum aktivieren";
        deviceManager.device = DeviceManager.DeviceTypes.Leap;

    }

    void Steam()
    {
        currentMessage = "Steam - Next zum aktivieren";
        deviceManager.device = DeviceManager.DeviceTypes.SteamVR;
    }

    void pinch()
    {
        currentMessage = "Pinch aktiviert - Next zum starten";
        deviceManager.activator = DeviceManager.ActivatorTypes.Pinch;
        deviceManager.showModels = true;

        currentTrackingsPerVariation = 0;
        dragStarted = false;
    }

    void touchmit()
    {
        currentMessage = "Touch mit Models aktiviert - Next zum starten";
        deviceManager.activator = DeviceManager.ActivatorTypes.Touch;
        deviceManager.showModels = true;

        currentTrackingsPerVariation = 0;
        dragStarted = false;
    }

    void touchohne()
    {
        currentMessage = "Touch ohne Models aktiviert - Next zum starten";
        deviceManager.activator = DeviceManager.ActivatorTypes.Touch;
        deviceManager.showModels = false;

        currentTrackingsPerVariation = 0;
        dragStarted = false;
    }


    void uebung()
    {
        currentMessage = "Uebungsphase - Next sobald User bereit";
        deviceManager.training = true;
        isTracking = false;

        currentTrackingsPerVariation = 0;
        dragStarted = false;

        deviceManager.Reset();
    }

    void schnell()
    {
        currentMessage = "Schnelle Phase (x10) - Erst Next wenn verschwunden";
        deviceManager.training = false;
        isTracking = true;

        currentTrackingsPerVariation = 0;
        dragStarted = false;

        deviceManager.Reset();
    }

    void genau()
    {
        currentMessage = "Genaue Phase (x10) - Erst Next wenn verschwunden";
        deviceManager.training = false;
        isTracking = true;

        currentTrackingsPerVariation = 0;
        dragStarted = false;

        deviceManager.Reset();
    }

    void Save()
    {
        deviceManager.DeactivateAllDevices();
        currentMessage = "All done - data saved";
        userData.Save(username);
    }

    //public void StartTracking()
    //{
    //    tracking = new TrackInstance();
    //    isTracking = true;
    //    currentTrackingsPerVariation = 0;
    //}
    
    // Update is called once per frame
    void FixedUpdate () {
        if(dragStarted == false && dragObject.isDragged)
        {
            if(isTracking)
            {
                dragStarted = true;
                tracking = new TrackInstance();
            }
        }

        if (dragStarted && isTracking)
        {
            if(tracking == null)
            {
                Debug.Log("Tracking nicht gesetzt");
            }

            if(!dragObject)
            {
                Debug.Log("DragObject nicht gesetzt");
            }

            if(tracking != null && dragObject)
            {
                Vector3Serializer serializableVector3 = new Vector3Serializer();
                serializableVector3.V3 = dragObject.transform.position;

                tracking.positions.Add(serializableVector3);
                tracking.time += Time.fixedDeltaTime;
            }
        }

	    if(dragObject.isDragged == false && control.isInsideTarget)
        {
            if(isTracking)
            {
                userData.addTracking(this.tracking);
                dragStarted = false;
            }

            currentTrackingsPerVariation += 1;
            if(currentTrackingsPerVariation == trackingsPerVariation)
            {
                if(deviceManager.training)
                {
                    deviceManager.SetNewPositions();
                } else
                {
                    isTracking = false;
                    dragStarted = false;

                    deviceManager.Hide();
                }
            } else
            {
                deviceManager.SetNewPositions();
            }
        }
	}
}
