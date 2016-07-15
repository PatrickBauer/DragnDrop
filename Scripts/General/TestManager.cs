using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour {

    public DeviceManager deviceManager;
    public AccuracyTester control;
    public DragObject dragObject;

    public float lastTime = 0.0f;
    public float currentTime = 0.0f;

    bool dragStarted = true;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if(dragStarted == false && dragObject.isDragged)
        {
            dragStarted = true;
        }

        if(dragStarted)
        {
            currentTime += Time.deltaTime;
        }

	    if(dragObject.isDragged == false && control.isInsideTarget)
        {
            lastTime = currentTime;
            currentTime = 0.0f;
            deviceManager.SetNewPositions();
            dragStarted = false;
        }
	}

    void OnGUI()
    {
        GUILayout.Space(300);
        GUILayout.Label(lastTime.ToString("F2"));
        GUILayout.Label(currentTime.ToString("F2"));
    }
}
