using UnityEngine;
using System.Collections;

public class BaseDeviceManager : MonoBehaviour {
    public GameObject toEnableDisable;

    public GameObject left;
    public GameObject right;


    public virtual void Enable() {
        if(toEnableDisable)
            toEnableDisable.SetActive(true);

        if (!left.GetComponent<DragInitiator>())
            left.AddComponent<DragInitiator>();

        if(!right.GetComponent<DragInitiator>())
            right.AddComponent<DragInitiator>();
    }

    public virtual void Disable() {
        if (toEnableDisable)
            toEnableDisable.SetActive(false);

        if (left.GetComponent<DragInitiator>())
            DestroyImmediate(left.GetComponent<DragInitiator>());

        if (right.GetComponent<DragInitiator>())
            DestroyImmediate(right.GetComponent<DragInitiator>());

        if (left.GetComponent<DragDropActivator>())
            DestroyImmediate(left.GetComponent<DragDropActivator>());

        if (right.GetComponent<DragDropActivator>())
            DestroyImmediate(right.GetComponent<DragDropActivator>());

        ShowModels(false);
    }

    public virtual void AddSpeech()
    {
        //DragDropActivator speech = GameObject.Find("Scripts/Speech").GetComponent<DragDropActivator>();

        //if (left.GetComponent<DragInitiator>() && speech)
        //    left.GetComponent<DragInitiator>().externalActivator = speech;

        //if(right.GetComponent<DragInitiator>() && speech)
        //    right.GetComponent<DragInitiator>().externalActivator = speech;

        left.AddComponent<SpeechActivator>();
        right.AddComponent<SpeechActivator>();
    }

    public virtual void AddTouch() {
        left.AddComponent<TouchActivator>();
        right.AddComponent<TouchActivator>();
    }

    public virtual void AddPinch() { }

    public virtual void ShowModels(bool showModels)
    {
        //Debug.Log("Showmodels: " + showModels + " für " + this.gameObject);
        setMeshState(toEnableDisable.transform, showModels);
    }

    public void setMeshState(Transform parent, bool state)
    {
        foreach (Transform child in parent)
        {
            setMeshState(child, state);
            
            if(child.GetComponent<DragInitiator>() || (child.name.Length >= 13 && child.name.Substring(0, 13) == "DragInitiator"))
            {
                continue;
            }

            if (child.GetComponent<Renderer>())
            {
                child.GetComponent<Renderer>().enabled = state;
            }
        }
    }

    public virtual void ResetActivator() {
        left.GetComponent<DragInitiator>().ResetActivator();
        right.GetComponent<DragInitiator>().ResetActivator();
    }
}

