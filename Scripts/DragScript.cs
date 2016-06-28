using UnityEngine;
using System.Collections;
using Leap.Unity.PinchUtility;

public class DragScript : MonoBehaviour {
    public LeapPinchDetector pinchLeft;
    public LeapPinchDetector pinchRight;

    private Transform _anchor;

    void Start () {
        GameObject pinchControl = new GameObject("RTS Anchor");
        _anchor = pinchControl.transform;
        _anchor.parent = transform.parent;
        transform.parent = _anchor;
    }
	
	void Update () {
        bool didUpdate = false;
        didUpdate |= pinchLeft.DidChangeFromLastFrame;
        didUpdate |= pinchRight.DidChangeFromLastFrame;

        if (didUpdate){
            transform.SetParent(null, true);
        }

        if (pinchLeft.IsPinching) {
            dragObject(pinchLeft);
        } else if (pinchRight.IsPinching) {
            dragObject(pinchRight);
        }

        if (didUpdate) {
            transform.SetParent(_anchor, true);
        }
    }

    void dragObject(LeapPinchDetector pinch)
    {
        _anchor.position = pinch.Position;
    }
}
