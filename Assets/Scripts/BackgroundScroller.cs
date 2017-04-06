using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public Camera cam;
    public float HORIZONTAL_FACTOR;
    public float VERTICAL_FACTOR;

    private Transform camTrans;
    private Vector3 CAM_START;
    private Vector3 BG_START;

    private void Start()
    {
        camTrans = cam.transform;
        CAM_START = camTrans.position;
        BG_START = transform.position;
    }

    void Update () {
        Vector3 offset = camTrans.position - CAM_START;
        offset.x *= HORIZONTAL_FACTOR;
        offset.y *= VERTICAL_FACTOR;
        transform.position = BG_START + offset;
    }
}
