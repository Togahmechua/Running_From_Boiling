using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private Frog player;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    [SerializeField] private float minSwipeDistance = 50f;
    private bool isSwiping = false;

    private bool isOnRight = false; // ✅ Mặc định ếch ở bên trái

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                isSwiping = true;
            }

            if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                isSwiping = false;
                endTouchPosition = touch.position;

                Vector2 inputVector = endTouchPosition - startTouchPosition;

                if (inputVector.magnitude < minSwipeDistance) return;

                if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
                {
                    if (inputVector.x > 0)
                        RightSwipe();
                    else
                        LeftSwipe();
                }
            }
        }
    }

    private void LeftSwipe()
    {
        if (isOnRight && !player.isDead)
        {
            player.MoveLeft();      // Gọi sang Frog
            isOnRight = false;
        }
    }

    private void RightSwipe()
    {
        if (!isOnRight && !player.isDead)
        {
            player.MoveRight();     // Gọi sang Frog
            isOnRight = true;
        }
    }
}
