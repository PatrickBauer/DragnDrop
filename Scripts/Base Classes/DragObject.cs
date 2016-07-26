using UnityEngine;
using System.Collections;


public class DragObject : MonoBehaviour
{
    private DragInitiator dragInitiator;
    private Transform interactionPoint;
    private Transform original;
    public GameObject control;

    public bool allowRotation = false;
    private Vector3 initialPosition;
    private Vector3 lastPosition;

    public float maxDistanceFromLine = 0.05f;

    public bool isDragged = false;
    public bool changedSinceLasteFrame = false;

    // Use this for initialization
    protected void OnEnable()
    {
        interactionPoint = new GameObject("Interaction Point").transform;
        initialPosition = transform.position;

        original = transform.parent;
    }

    public void Reset()
    {
        transform.position = initialPosition;
    }

    public void LateUpdate()
    {
        changedSinceLasteFrame = false;
    }

    protected void Update()
    {
        if (dragInitiator)
        {
            interactionPoint.transform.position = dragInitiator.transform.position;

            if (dragInitiator.hasTouchActivator)
            {

                //max distance to control object if touch is used
                if (Vector3.Distance(transform.position, control.transform.position) >= maxDistanceFromLine)
                {
                    transform.position = lastPosition;
                }

                lastPosition = transform.position;
            }



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

        if (dragInitiator.hasTouchActivator)
        {
            lastPosition = transform.position;
        }

        if (allowRotation)
        {
            interactionPoint.rotation = initiator.transform.rotation;
        }

        transform.SetParent(interactionPoint, true);

        isDragged = true;
        changedSinceLasteFrame = true;
    }

    public void OnDragStop(DragInitiator initiator)
    {
        if (initiator == dragInitiator)
        {
            dragInitiator = null;
            transform.SetParent(original, true);
        }

        isDragged = false;
        changedSinceLasteFrame = true;
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