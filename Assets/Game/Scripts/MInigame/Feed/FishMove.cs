using UnityEngine;

public class FishMove : MonoBehaviour
{
    public bool direction;

    Vector3 mousePos;
    Vector3 mousePos2;

    public Vector3 mouseDir;
    public Vector3 mouseDir2;
    public Vector3 lastMousePos;

    public bool canMove;

    public BoxCollider2D col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            lastMousePos = Input.mousePosition;
        }
        mousePos = Input.mousePosition;
        mousePos.x = mousePos.x - Screen.width / 2;
        mousePos.y = mousePos.y - Screen.height / 2;
        mousePos2 = mousePos / GetComponentInParent<Canvas>().scaleFactor;
        
    }

    void OnMouseDrag()
    {
        mouseDir = Input.mousePosition - lastMousePos;
        mouseDir2 = Input.mousePosition - lastMousePos;
        if (direction)
        {
            if(canMove)
            {
                transform.localPosition = new Vector3(mousePos2.x, transform.localPosition.y, transform.localPosition.z);
            }
            mouseDir = new Vector3(Input.mousePosition.x - lastMousePos.x, 0, 0);
            mouseDir.Normalize();
            col.offset = new Vector2(col.offset.x, Mathf.Abs(col.offset.y) * mouseDir.x);
        }
        else
        {
            if(canMove)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, mousePos2.y, transform.localPosition.z);
            }
            mouseDir = new Vector3(0, Input.mousePosition.y - lastMousePos.y, 0);
            mouseDir.Normalize();
            if(mouseDir.y != 0)
            {
                col.offset = new Vector2(col.offset.x, Mathf.Abs(col.offset.y) * mouseDir.y);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("h");
        canMove = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("h");
        canMove = true;
    }
}
