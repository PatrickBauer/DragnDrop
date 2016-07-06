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

        if (dragDropActivator.isActive)
        {
            float minDistance = float.MaxValue;
        }

        if (dragDropActivator.isActive && dragDropActivator.changedSinceLastFrame)
        {
            // Find the closest item to the hand in case there are multiple and interact with it
            float minDistance = float.MaxValue;

            float distance;
            foreach (DragObject item in objectsHoveringOver)
            {
                distance = (item.transform.position - transform.position).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestItem = item;
                }
            }

            interactingItem = closestItem;
            closestItem = null;

            if (interactingItem)
                interactingItem.OnDragStart(this);

        } else if(interactingItem != null && dragDropActivator.changedSinceLastFrame) {
            interactingItem.OnDragStop(this);
        }
    }


    // Adds all colliding items to a HashSet for processing which is closest
    private void OnTriggerEnter(Collider collider)
    {
        DragObject collidedItem = collider.GetComponent<DragObject>();
        if (collidedItem)
        {
            objectsHoveringOver.Add(collidedItem);
            collidedItem.OnHoverEnter();
        }
    }

    // Remove all items no longer colliding with to avoid further processing
    private void OnTriggerExit(Collider collider)
    {
        DragObject collidedItem = collider.GetComponent<DragObject>();
        if (collidedItem)
        {
            objectsHoveringOver.Remove(collidedItem);
            collidedItem.OnHoverLeave();
        }
    }
}
