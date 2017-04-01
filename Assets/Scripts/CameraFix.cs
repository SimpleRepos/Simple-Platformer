using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour {

    public float VSCROLL_SPEED;
    public float HSCROLL_SPEED;

    private List<BoxCollider2D> boxes;
    private BoxCollider2D outerBox;

    private GameObject king;
    public GameObject King
    {
        get { return king; }
        set
        {
            king = value;
            //ResetPosition();
        }
    }

    void ResetPosition()
    {
        Vector3 kp = king.transform.position;
        kp.z = -5;
        transform.position = kp;
    }

    private void Start()
    {
        boxes = new List<BoxCollider2D>(GetComponents<BoxCollider2D>());
        outerBox = boxes[boxes.Count - 1];
        boxes.RemoveAt(boxes.Count - 1);
    }

    void Update () {
        if (king) {
            Vector3 motion = new Vector3(0, 0, 0);
            Vector3 kp = king.transform.position;

            foreach (BoxCollider2D box in boxes)
            {
                float left   = box.bounds.min.x;
                float right  = box.bounds.max.x;
                float top    = box.bounds.max.y;
                float bottom = box.bounds.min.y;

                if (kp.x < left)   { motion.x -= HSCROLL_SPEED; }
                if (kp.x > right)  { motion.x += HSCROLL_SPEED; }
                if (kp.y < bottom) { motion.y -= VSCROLL_SPEED; }
                if (kp.y > top)    { motion.y += VSCROLL_SPEED; }
            }

            { //outer box - snap behavior
                float left   = outerBox.bounds.min.x;
                float right  = outerBox.bounds.max.x;
                float top    = outerBox.bounds.max.y;
                float bottom = outerBox.bounds.min.y;

                if (kp.x < left)   { motion.x = kp.x - left; }
                if (kp.x > right)  { motion.x = kp.x - right; }
                if (kp.y < bottom) { motion.y = kp.y - bottom; }
                if (kp.y > top)    { motion.y = kp.y - top; }
            }


            transform.position = transform.position + motion;
        }
    }
}
