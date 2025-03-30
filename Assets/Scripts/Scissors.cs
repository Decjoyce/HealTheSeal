using UnityEngine;

public class Scissors : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 mousePos2;

    public bool a;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
