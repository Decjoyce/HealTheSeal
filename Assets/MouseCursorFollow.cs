using UnityEngine;

public class MouseCursorFollow : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 mousePos2;

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.x = mousePos.x - Screen.width / 2;
        mousePos.y = mousePos.y - Screen.height / 2;
        mousePos2 = mousePos / GetComponentInParent<Canvas>().scaleFactor;
        transform.localPosition = mousePos2;
    }
}
