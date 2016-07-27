using UnityEngine;
using System.Collections;


public class DragObject : MonoBehaviour
{
    public DragInitiator dragInitiator;
    private Transform interactionPoint;
    public Transform original;
    public GameObject control;

    public bool allowRotation = false;
    private Vector3 initialPosition;
    private Vector3 lastPosition;

    public float maxDistanceFromLine = 0.05f;

    public bool isDragged = false;
    public bool changedSinceLasteFrame = false;
    bool isHovering = false;

    // Use this for initialization
    protected void OnEnable()
    {
        interactionPoint = new GameObject("Interaction Point").transform;
        initialPosition = transform.position;

        //original = transform.parent;
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
        //set colors
        if(isHovering && isDragged)
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        else if (isHovering)
            GetComponent<Renderer>().material.SetColor("_Color", new Color(0.263f, 0.686f, 1.0f));
        else
            GetComponent<Renderer>().material.SetColor("_Color", Color.white);



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
    
    public void ForceParent()
    {
        transform.SetParent(original, true);
    }

    private void OnDestroy()
    {
        if (interactionPoint) Destroy(interactionPoint.gameObject);
    }

    public void OnHoverEnter()
    {
        isHovering = true;
    }


    public void OnHoverLeave()
    {
        isHovering = false;
    }
}