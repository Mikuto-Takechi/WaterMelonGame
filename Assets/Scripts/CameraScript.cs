using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.2f;
    [SerializeField] float verticalSpeed = 0.2f;
    public bool reverse;
    private Vector2 lastMousePosition;
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetButton("Fire2"))
        {
            if (!reverse)
            {
                var newPositionY = (Input.mousePosition.y - lastMousePosition.y) * verticalSpeed;
                var y = (lastMousePosition.x - Input.mousePosition.x);

                var newAngle = Vector3.zero;
                newAngle.y = y * rotationSpeed;
                transform.position = new Vector3(transform.position.x, transform.position.y + newPositionY, transform.position.z);
                transform.Rotate(newAngle);
                lastMousePosition = Input.mousePosition;
            }
            else
            {
                var newPositionY = (lastMousePosition.y - Input.mousePosition.y) * verticalSpeed;
                var y = (Input.mousePosition.x - lastMousePosition.x);

                var newAngle = Vector3.zero;
                newAngle.y = y * rotationSpeed;
                transform.position = new Vector3(transform.position.x, transform.position.y + newPositionY, transform.position.z);
                transform.Rotate(newAngle);
                lastMousePosition = Input.mousePosition;
            }
            var clampVertical = transform.position;
            clampVertical.y = Mathf.Clamp(clampVertical.y, -5, 10);
            transform.position = clampVertical;
        }
    }
}
