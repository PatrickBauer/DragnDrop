using UnityEngine;
using System.Collections;

public class LeapDeviceManager : BaseDeviceManager {

    public DragDropActivator PinchDetector_L;
    public DragDropActivator PinchDetector_R;

    public Leap.Unity.CapsuleHand L;
    public Leap.Unity.CapsuleHand R;

    override public void AddPinch()
    {
        left.GetComponent<DragInitiator>().externalActivator = PinchDetector_L;
        right.GetComponent<DragInitiator>().externalActivator = PinchDetector_R;
    }

    override public void ShowModels(bool showModels)
    {
        //Debug.Log("Showmodels: " + showModels + " für " + this.gameObject);
        
        if(!L || !R)
        {
            return;
        }

        if (showModels)
        {
           L._showArm = true;
           R._showArm = true;
        }
        else
        {
            L._showArm = false;
            R._showArm = false;
        }

        L.updateArmVisibility();
        R.updateArmVisibility();
    }
}
