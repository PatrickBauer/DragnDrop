using UnityEngine;
using System.Collections;

public class AccuracyTester : MonoBehaviour {
    public GameObject target;
    public float minAccuracy = 70.0f;

    protected float volume_max = 0.0f;
    protected float volume = 0.0f;
    public float accuracy = 0.0f;
    protected float factor = 100000;
    protected Vector3 difference;

    public bool isInsideTarget = false;


    void Update () {
        difference = transform.position - target.transform.position;

        var x = Mathf.Clamp(0.1f - Mathf.Abs(difference.x), 0.0f, 0.1f);
        var y = Mathf.Clamp(0.1f - Mathf.Abs(difference.y), 0.0f, 0.1f);
        var z = Mathf.Clamp(0.1f - Mathf.Abs(difference.z), 0.0f, 0.1f);

        accuracy = 0.0f;

        if (GetComponent<SphereCollider>().bounds.Intersects(target.GetComponent<SphereCollider>().bounds))
        {
            volume_max = 0.1f * 0.1f * 0.1f;
            volume = x * y * z;

            accuracy = volume * factor;

            if (accuracy >= minAccuracy)
            {
                isInsideTarget = true;

                if (gameObject.transform.name == "Control")
                    GetComponent<Renderer>().material.SetColor("_Color", new Color(0.0f, 1.0f, 0.0f, 0.4f));
            } else {
                isInsideTarget = false;

                if (gameObject.transform.name == "Control")
                    GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.4f));
            }
        } else
        {
            isInsideTarget = false;

            if (gameObject.transform.name == "Control")
                GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.4f));
        }
    }

    
}
