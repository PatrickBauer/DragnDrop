using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour
{
    private DragInitiator dragInitiator;
    private Transform interactionPoint;
    private Transform original;

    private float lastZValue;

    public bool allowRotation = false;
    private Vector3 initialPosition;

    // Use this for initialization
    protected void Start()
    {
        interactionPoint = new GameObject().transform;
        initialPosition = transform.localPosition;
    }

    public void Reset()
    {
        transform.localPosition = new Vector3(-0.23f, 1.9f, 0.383f);
    }

    protected void FixedUpdate()
    {
        if (dragInitiator)
        {
            interactionPoint.transform.position = dragInitiator.transform.position;

            if (dragInitiator.hasTouchActivator && dragInitiator.transform.position.z < lastZValue)
            {
                Vector3 temp = interactionPoint.transform.position;
                temp.z = lastZValue;

                interactionPoint.transform.position = temp;
            }

            lastZValue = interactionPoint.transform.position.z;

            if (allowRotation)
            {
                interactionPoint.rotation = dragInitiator.transform.rotation;
            }
        }
    }

    public void OnDragStart(DragInitiator initiator)
    {
        if (initiator != dragInitiator && original) transform.SetParent(original, true);

        dragInitiator = initiator;
        interactionPoint.transform.position = initiator.transform.position;

        if (allowRotation)
        {
            interactionPoint.rotation = initiator.transform.rotation;
        }

        if (initiator.hasTouchActivator)
        {
            lastZValue = interactionPoint.position.z;
        }

        original = transform.parent;
        transform.SetParent(interactionPoint, true);
    }

    public void OnDragStop(DragInitiator initiator)
    {
        if (initiator == dragInitiator)
        {
            dragInitiator = null;
            transform.SetParent(original, true);
        }
    }
    
    private void OnDestroy()
    {
        if (interactionPoint) Destroy(interactionPoint.gameObject);
    }

    public void OnHoverEnter()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    }


    public void OnHoverLeave()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }
}