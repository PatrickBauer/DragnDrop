using UnityEngine;
using System.Collections;

public class LeapMotionPinchActivator : DragDropActivator {

    public Leap.Unity.PinchUtility.LeapPinchDetector pinchDetector;
    
	// Update is called once per frame
	public override void Update () {
        base.Update();

        if (pinchDetector.DidStartPinch)
        {
            isActive = true;
            changedSinceLastFrame = true;
            return;
        }

        if (pinchDetector.DidEndPinch)
        {
            isActive = false;
            changedSinceLastFrame = true;
            return;
        }
    }
}
