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

    void Start () {
        isActive = false;
        isHovering = false;

        DragObject = GameObject.Find("DragObject");

        if(GameObject.Find("Text"))
            text = GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>();
    }

    void OnEnable()
    {
        DragObject = GameObject.Find("DragObject");

        if (GameObject.Find("Text"))
            text = GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>();
    }

    public virtual void FixedUpdate () {
        if(!Initiator)
        {
            return;
        }

        float distance = Vector3.Distance(Initiator.transform.position, DragObject.transform.position);

        //set distance string
        if(text)
            text.text = distance.ToString();

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
