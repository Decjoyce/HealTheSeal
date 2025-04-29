using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed = 0.01f;
    public float minY, maxY; // set bounds clearly in inspector
    public float pc_minY, pc_maxY; // set bounds clearly in inspector

    Vector3 touchStart;
    Vector3 newPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = transform.position + new Vector3(0, -direction.y * scrollSpeed * Time.deltaTime, 0);

            newPos = Vector3.Lerp(transform.position, newPosition, 0.01f * Time.deltaTime);

            if(!GameManagement.instance.is_pc)
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            else
                newPosition.y = Mathf.Clamp(newPosition.y, pc_minY, pc_maxY);

            transform.position = newPosition;
        }
        //transform.position = newPos;

    }
}
