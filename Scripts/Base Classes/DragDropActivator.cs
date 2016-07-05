using UnityEngine;
using System.Collections;

public class DragDropActivator : MonoBehaviour {
    
    public bool isActive { get; set; }
    public bool changedSinceLastFrame { get; set; }

    void Start () {
        isActive = false;
	}
	
	void Update () {
	    
	}
}
