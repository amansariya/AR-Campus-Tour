using UnityEngine;

public class ARInteraction : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector2 initialTouchPosition;
    private Vector2 currentTouchPosition;
    private float initialDistance;
    private Vector3 initialScale;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform != null)
                    {
                        selectedObject = hit.transform.gameObject;
                        initialTouchPosition = touch.position;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && selectedObject != null)
            {
                currentTouchPosition = touch.position;
                float rotationAngle = initialTouchPosition.x - currentTouchPosition.x;
                selectedObject.transform.Rotate(0, -rotationAngle * Time.deltaTime, 0);
                initialTouchPosition = currentTouchPosition;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                if (selectedObject != null)
                {
                    initialScale = selectedObject.transform.localScale;
                }
            }
            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                if (selectedObject != null && initialDistance > 0)
                {
                    float scaleFactor = currentDistance / initialDistance;
                    selectedObject.transform.localScale = initialScale * scaleFactor;
                }
            }
        }
    }
}