using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragInitiator : MonoBehaviour {

    public DragDropActivator externalActivator;
    public DragDropActivator dragDropActivator { get;set; }

    HashSet<DragObject> objectsHoveringOver = new HashSet<DragObject>();

    private DragObject closestItem;
    private DragObject interactingItem;

    public bool hasTouchActivator { get; set; }

    public void ResetActivator()
    {
        if (externalActivator)
        {
            dragDropActivator = externalActivator;
        }
        else if (GetComponent<DragDropActivator>())
        {
            dragDropActivator = GetComponent<DragDropActivator>();
        }
        else
        {
            Debug.LogError("Kein Activator definiert!");
        }

        hasTouchActivator = dragDropActivator is TouchActivator;
    }

	// Update is called once per frame
	void FixedUpdate() {
        if (!dragDropActivator) return;

        if(!dragDropActivator.Initiator)
        {
            dragDropActivator.Initiator = this;
        }

        interactingItem = dragDropActivator.DragObject.GetComponent<DragObject>();
        if (!interactingItem) return;

        //set colours of activatr
        if (dragDropActivator.isHovering)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }

        if (dragDropActivator.isActive && dragDropActivator.changedSinceLastFrame && dragDropActivator.isHovering)
        {
            interactingItem.OnDragStart(this);
        }

        if (!dragDropActivator.isActive && dragDropActivator.changedSinceLastFrame) {
            interactingItem.OnDragStop(this);
        }
    }
}
