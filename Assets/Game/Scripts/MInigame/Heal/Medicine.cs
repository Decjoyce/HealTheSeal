using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Medicine : MonoBehaviour
{
    public Image sealImage;
    public Color[] tColour;
    public bool canMove;

    public bool shouldMove;

    Vector3 mousePos;
    Vector3 mousePos2;

    public Vector3 mouseDir;
    public Vector3 mouseDir2;
    public Vector3 lastMousePos;

    public Transform resetPos;

    public float lookTime;

    public float backAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastMousePos = Input.mousePosition;
        
        StartCoroutine(Look(lookTime));
    }

    private IEnumerator Look(float waitTime)
    {
        while (true)
        {
            shouldMove = false;
            sealImage.color = tColour[0];
            yield return new WaitForSeconds(waitTime);
            shouldMove = true;
            sealImage.color = tColour[1];
            yield return new WaitForSeconds(waitTime*1.5f);
            sealImage.color = tColour[2];
            yield return new WaitForSeconds(waitTime/2);
        }
    }

    private IEnumerator Return()
    {
        while (transform.localPosition.y < resetPos.localPosition.y)
        {
            canMove = false;
            var step = 200 * Time.deltaTime; 
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, resetPos.localPosition, step);
            yield return new WaitForFixedUpdate();
        }
        canMove = true;
        Debug.Log("h");
        StartCoroutine(Look(lookTime));
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

        if(!shouldMove && mouseDir.y != 0)
        {
            StopAllCoroutines();
            StartCoroutine(Return());
        }

        if(mouseDir.y > 5 || mouseDir.y < -5)
        {
            StopAllCoroutines();
            StartCoroutine(Look(lookTime));
        }
    }

    void OnMouseEnter()
    {
        canMove = true;
    }

    void OnMouseExit()
    {
        //canMove = false;
    }

    void OnMouseDrag()
    {
        mouseDir = Input.mousePosition - lastMousePos;
        mouseDir2 = Input.mousePosition - lastMousePos;
        if(canMove)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, mousePos2.y, transform.localPosition.z);
            resetPos.localPosition = new Vector3(transform.localPosition.x, (Mathf.Min((transform.localPosition.y + backAmount), 650)), transform.localPosition.z);
        }
        
    }

    void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
