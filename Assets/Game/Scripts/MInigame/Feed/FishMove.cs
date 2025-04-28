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
    public bool check;

    float turn = 90f;

    public bool test;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        /*RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.up * 0.55f), Vector2.up, 0.1f);
        // Does the ray intersect any objects excluding the player layer
        if (hit)
        {
            Debug.DrawRay(transform.position + (Vector3.up * 0.55f), transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            Debug.Log("hit" + hit.distance);
            check = !check;
        }*/
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
        if (canMove && mouseDir != Vector3.zero)
        {
            if (direction)
            {
                
                mouseDir = new Vector3(Input.mousePosition.x - lastMousePos.x, 0, 0);
                mouseDir.Normalize();
                RaycastHit2D hit = Physics2D.Raycast(transform.position + (mouseDir * 0.55f), mouseDir, 0.1f);
                if (!hit)
                {
                    transform.localPosition = new Vector3(mousePos2.x, transform.localPosition.y, transform.localPosition.z);
                }
            }
            else
            {
                mouseDir = new Vector3(0, Input.mousePosition.y - lastMousePos.y, 0);
                mouseDir.Normalize();
                RaycastHit2D hit = Physics2D.Raycast(transform.position + (mouseDir * 0.55f), mouseDir, 0.1f);
                if (!hit)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, mousePos2.y, transform.localPosition.z);
                }

            }
        }
        /*if (direction)
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
            
        }*/
    }

    void OnMouseDown()
    {
        transform.hasChanged = false;
        canMove = true;
    }

    void OnMouseUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.TransformDirection(Vector3.right) * 0.30f), transform.TransformDirection(Vector3.right), 0.4f);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + (transform.TransformDirection(Vector3.left) * 0.30f), transform.TransformDirection(Vector3.left), 0.4f);

        if (hit)
        {
            Debug.DrawRay(transform.position + (transform.TransformDirection(Vector3.right) * 0.30f), transform.TransformDirection(Vector3.right) * hit.distance, Color.red);
        }
        else if (hit2)
        {
            Debug.DrawRay(transform.position + (transform.TransformDirection(Vector3.left) * 0.30f), transform.TransformDirection(Vector3.left) * hit2.distance, Color.green);
        }
        if (!transform.hasChanged && !hit && !hit2)
        {
            transform.Rotate(0f, 0f, turn, Space.Self);
            turn = -turn;
            direction = !direction;
        }
        canMove = false;
    }

    void OnMouseExit()
    {
        canMove = false;
    }

    /* private void OnTriggerEnter2D(Collider2D collision)
     {
         Debug.Log("h");
         canMove = false;
     }

     private void OnTriggerExit2D(Collider2D collision)
     {
         Debug.Log("h");
         canMove = true;
     }*/
}
