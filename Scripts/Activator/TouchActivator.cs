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

    //Called from initiator
    public void Enter()
    {
        isActive = true;
        triggerSinceLastFrame = true;
    }
    
    //Called from initiator
    public void Exit()
    {
        isActive = false;
        triggerSinceLastFrame = true;
    }
}
