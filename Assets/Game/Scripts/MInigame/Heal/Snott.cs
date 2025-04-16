using UnityEngine;

public class Snott : MonoBehaviour
{
    public bool mMove;
    public Vector3 lastMousePos;

    public float timer;
    public float wipeTime;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lastMousePos == Input.mousePosition)
        {
            mMove = true;
        }
        else
        {
            mMove = false;
        }
        lastMousePos = Input.mousePosition;

        if(timer >= wipeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!mMove)
        {
            timer = timer + Time.deltaTime;
        }
    }
}
