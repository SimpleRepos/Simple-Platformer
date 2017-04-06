using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public Camera cam;
    public float HORIZONTAL_FACTOR;

    private Transform camTrans;
    private float CAM_START_X;

    private void Start()
    {
        camTrans = cam.transform;
        CAM_START_X = camTrans.position.x;
    }

    void Update () {
        float camOffset = camTrans.position.x - CAM_START_X;
        float bgOffset = camOffset * HORIZONTAL_FACTOR;

        Vector3 pos = transform.position;
        pos.x = bgOffset;
        transform.position = pos;
    }
}
