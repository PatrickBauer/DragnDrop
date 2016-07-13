using UnityEngine;
using System.Collections;

public class TouchActivator : DragDropActivator {

    // Update is called once per frame
    public override void FixedUpdate () {
        base.FixedUpdate();

        if (!isActive && isHovering)
        {
            isActive = true;
            changedSinceLastFrame = true;
            return;
        }

        if (isActive && !isHovering)
        {
            isActive = false;
            changedSinceLastFrame = true;
            return;
        }

        changedSinceLastFrame = false;
    }
}
