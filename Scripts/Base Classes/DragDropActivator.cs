using UnityEngine;
using System.Collections;

public class DragDropActivator : MonoBehaviour {
    
    public bool isActive { get; set; }
    public bool changedSinceLastFrame { get; set; }
    public bool isHovering { get; set; }

    float hoverStartDistance = 0.035f;
    float hoverStopDistance = 0.045f;

    public GameObject DragObject;
    UnityEngine.UI.Text text;

    public DragInitiator Initiator;

    public virtual void Start () {
        isActive = false;
        isHovering = false;

        if(!DragObject)
            DragObject = GameObject.Find("DragObject");
    }

    void OnEnable()
    {
        if(!DragObject)
            DragObject = GameObject.Find("DragObject");
    }

    public virtual void FixedUpdate () {
        if(!Initiator)
        {
            return;
        }

        float distance = Vector3.Distance(Initiator.transform.position, DragObject.transform.position);
        
        //active/deactivate hover state
        if (!isHovering && distance <= hoverStartDistance)
        {
            isHovering = true;
            DragObject.GetComponent<DragObject>().OnHoverEnter();
        }

        if (isHovering && distance >= hoverStopDistance)
        {
            isHovering = false;
            DragObject.GetComponent<DragObject>().OnHoverLeave();
        }
    }
}
