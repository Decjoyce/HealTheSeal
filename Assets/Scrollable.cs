using UnityEngine;

public class Scrollable : MonoBehaviour
{
    [SerializeField] CameraScroll scroller;

    private void OnMouseDown()
    {
        scroller.is_scrolling = true;
    }
}
