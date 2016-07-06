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

    protected Color colorOriginal;

    void Update () {
        difference = transform.position - target.transform.position;

        var x = Mathf.Clamp(0.1f - Mathf.Abs(difference.x), 0.0f, 0.1f);
        var y = Mathf.Clamp(0.1f - Mathf.Abs(difference.y), 0.0f, 0.1f);
        var z = Mathf.Clamp(0.1f - Mathf.Abs(difference.z), 0.0f, 0.1f);

        accuracy = 0.0f;

        if (GetComponent<BoxCollider>().bounds.Intersects(target.GetComponent<BoxCollider>().bounds))
        {
            volume_max = 0.1f * 0.1f * 0.1f;
            volume = x * y * z;

            accuracy = volume * factor;

            if (accuracy >= minAccuracy)
            {
                colorOriginal = GetComponent<Renderer>().material.GetColor("_Color");
                GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            } else {
                GetComponent<Renderer>().material.SetColor("_Color", colorOriginal);
            }
        }
    }

    
}
