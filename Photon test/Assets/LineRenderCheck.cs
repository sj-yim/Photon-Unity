using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderCheck : MonoBehaviour
{
    public LineRenderer lr;

    private void Start()
    {
        //lr = go.GetComponent<LineRenderer>();
        lr.startWidth = 0.7f;
        lr.endWidth = 0.7f;
    }

    void Update()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.forward * 1.2f + transform.position);

    }
}
