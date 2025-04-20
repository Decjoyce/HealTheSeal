using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed = 0.01f;
    public float minY, maxY; // set bounds clearly in inspector

    Vector3 touchStart;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = transform.position + new Vector3(0, direction.y, 0);

            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            transform.position = newPosition;
        }
    }
}
