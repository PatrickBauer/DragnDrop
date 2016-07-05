using UnityEngine;
using System.Collections;

public class DeviceManager : MonoBehaviour {
    public GameObject toEnableDisable;

    public GameObject left;
    public GameObject right;


    public virtual void Enable() {
        toEnableDisable.SetActive(true);
    }

    public virtual void Disable() {
        toEnableDisable.SetActive(false);

        left.GetComponent<DragInitiator>().externalActivator = null;
        right.GetComponent<DragInitiator>().externalActivator = null;

        if(left.GetComponent<DragDropActivator>())
            Destroy(left.GetComponent<DragDropActivator>());

        if (right.GetComponent<DragDropActivator>())
            Destroy(right.GetComponent<DragDropActivator>());
    }

    public virtual void AddSpeech()
    {
        left.GetComponent<DragInitiator>().externalActivator = GameObject.Find("Scripts/Speech").GetComponent<DragDropActivator>();
        right.GetComponent<DragInitiator>().externalActivator = GameObject.Find("Scripts/Speech").GetComponent<DragDropActivator>();
    }

    public virtual void AddTouch() {
        left.AddComponent<TouchActivator>();
        right.AddComponent<TouchActivator>();
    }

    public virtual void AddPinch() { }

    public virtual void ResetActivator() {
        left.GetComponent<DragInitiator>().ResetActivator();
        right.GetComponent<DragInitiator>().ResetActivator();
    }
}

