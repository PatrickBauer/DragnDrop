using UnityEngine;
using System.Collections;

public class TouchActivator : DragDropActivator {
    bool triggerSinceLastFrame;

	// Update is called once per frame
	void Update () {
        if(triggerSinceLastFrame)
        {
            triggerSinceLastFrame = false;
            changedSinceLastFrame = true;
        } else {
            changedSinceLastFrame = false;
        }
    }

    public void Enter()
    {
        isActive = true;
        triggerSinceLastFrame = true;
    }

    public void Exit()
    {
        isActive = false;
        triggerSinceLastFrame = true;
    }

    //private void OnTriggerEnter(Collider collider)
    //{
    //    isActive = true;
    //    triggerSinceLastFrame = true;
    //}

    //private void OnTriggerExit(Collider collider)
    //{
    //    isActive = false;
    //    triggerSinceLastFrame = true;
    //}
}
