using UnityEngine;
using UnityEngine.Device;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed = 0.01f;
    public float minY, maxY; // set bounds clearly in inspector
    public float minY_189, maxY_189; // set bounds clearly in inspector
    public float pc_minY, pc_maxY; // set bounds clearly in inspector
    public float pc_minY_189, pc_maxY_189; // set bounds clearly in inspector

    Vector3 newPosition;
    Vector3 direction;
    Vector3 touchStart;
    Vector3 newPos;

    static Vector3 saved_pos;

    public bool is_scrolling;

    public bool can_scroll = true;

    bool is_pc, is_189;

    [SerializeField] Camera cma;

    private void Start()
    {
        transform.position = saved_pos;
        can_scroll = !GameManagement.instance.first_time;
    }

    void Update()
    {
        if (!can_scroll)
            return;

        if (Input.GetMouseButtonDown(0))
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition = transform.position + new Vector3(0, -direction.y, 0);
        }

        if(newPosition.y != 0f)
        {

            newPos = Vector3.Lerp(transform.position, newPosition, scrollSpeed * Time.deltaTime);

            if (!GameManagement.instance.is_pc)
            {
                if (!is_189)
                    newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
                else
                    newPos.y = Mathf.Clamp(newPos.y, minY_189, maxY_189);
            }
            else
            {
                if (!is_189)
                    newPos.y = Mathf.Clamp(newPos.y, pc_minY, pc_maxY);
                else
                    newPos.y = Mathf.Clamp(newPos.y, pc_minY_189, pc_maxY_189);
            }

            transform.position = newPos;
            saved_pos = newPos;
        }

        //transform.position = newPos;

    }

    public void ToggleScrolling(bool y)
    {
        can_scroll = y;
    }
}
