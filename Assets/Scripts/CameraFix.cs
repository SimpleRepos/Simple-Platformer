using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour {

    private GameObject king;
    public GameObject King
    {
        get { return king; }
        set
        {
            king = value;
            Update();
        }
    }
    
    void Update () {
        if (king) {
            Vector3 pos = transform.position;
            pos.x = king.transform.position.x;
            transform.position = pos;
        }
    }
}
