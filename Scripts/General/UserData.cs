using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class UserData {
    public List<TrackInstance> trackings;
    public string userName;
    int trackingCount = 1;
    
    public bool Save(string username)
    {
        string path = Application.persistentDataPath;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path + "/user_" + username + ".dd");
        bf.Serialize(file, this);
        file.Close();

        return true;
    }

    public void addTracking(TrackInstance instance)
    {
        this.trackings.Add(instance);

        Debug.Log("Tracking hinzugefuegt - " + trackingCount++);
    }

    // Use this for initialization
    public UserData()
    {
        trackings = new List<TrackInstance>();
    }
}
