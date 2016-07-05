using UnityEngine;
using System.Collections;

public class TouchActivator : DragDropActivator {

    int frameCounter = 0;
    bool triggerSinceLastFrame;

	// Update is called once per frame
	void Update () {
        if(triggerSinceLastFrame)
        {
            if(frameCounter == 1)
            {
                triggerSinceLastFrame = false;
                changedSinceLastFrame = false;
                frameCounter = 0; 
                return;
            }

            frameCounter = frameCounter + 1;
            changedSinceLastFrame = true;
        } else
        {
            changedSinceLastFrame = false;
        }
    }

    // Adds all colliding items to a HashSet for processing which is closest
    private void OnTriggerEnter(Collider collider)
    {
        isActive = true;
        triggerSinceLastFrame = true;
    }

    // Remove all items no longer colliding with to avoid further processing
    private void OnTriggerExit(Collider collider)
    {
        isActive = false;
        triggerSinceLastFrame = true;
    }
}
