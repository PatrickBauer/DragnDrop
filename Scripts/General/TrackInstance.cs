using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TrackInstance {
    public List<Vector3Serializer> positions;
    public float time;

    //local positions
    public Vector3Serializer startpoint;
    public Vector3Serializer endpoint;

    public DeviceManager.DeviceTypes device;
    public DeviceManager.ActivatorTypes activator;

    public bool modelsActive;

    //speed 0 = genau wie moeglich 1 = schnell wie moeglich
    public int speed; 

    //control value (local positions)
    public int startEndPositionIndex;

    //0 = left, 1 = right
    public int hand = 0;

    public string initiator;

    // Use this for initialization
    public TrackInstance()
    {
        positions = new List<Vector3Serializer>();
        time = 0.0f;

        startpoint = new Vector3Serializer();
        endpoint = new Vector3Serializer();
    }
}
