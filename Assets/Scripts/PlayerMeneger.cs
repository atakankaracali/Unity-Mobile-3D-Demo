using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeneger : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;
    public Rigidbody rb;
    public LineRenderer lr;

    Vector3 dragStartPos;
    Touch touch;

    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                DragStart();
            }
            if(touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if(touch.phase == TouchPhase.Ended)
            {
                DragRelease();
            }

        }
    }
    void DragStart()
    {
        dragStartPos = Camera.main.ScreenToViewportPoint(touch.position);
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }
    void Dragging()
    {
        Vector3 draggingPos = Camera.main.ScreenToViewportPoint(touch.position);
        lr.positionCount = 2;
        lr.SetPosition(1, dragStartPos);
    }
    void DragRelease()
    {
        lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToViewportPoint(touch.position);

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        rb.AddForce(clampedForce, ForceMode.Impulse);
    }

}


