using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour, ITouchSource
{
    public static bool Tap, SwipeLeft, SwipeRight, SwipeUp;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    private void Update()
    {
        Swipe();
        PcSwipe();
        MobileSwipe();
    }

    public void Swipe()
    {
        Tap = SwipeUp = SwipeLeft = SwipeRight = false;

        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length < 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        if (swipeDelta.magnitude > 100)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {

                if (x < 0)
                    SwipeLeft = true;
                else
                    SwipeRight = true;
            }
            else
            {
                if (y > 0)
                    SwipeUp = true;
            }
            Reset();
        }
    }

    private void PcSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TapAndDrag(true, true);
            //Tap = true;
            //isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            TapAndDrag(false, false);
            //isDraging = false;
            Reset();
        }
    }

    private void MobileSwipe()
    {
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                TapAndDrag(true, true);
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                TapAndDrag(false, false);
                Reset();
            }
        }
    }

    private void TapAndDrag(bool isTap, bool isDraging)
    {
        Tap = isTap;
        this.isDraging = isDraging;
    }

    public void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
