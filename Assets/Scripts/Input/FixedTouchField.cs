using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerUpHandler,IPointerDownHandler
{
    [HideInInspector]
    public Vector2 TouchDistance;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;
    // Update is called once per frame
    void Update()
    {
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                TouchDistance = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                TouchDistance = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDistance = new Vector2();
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerOld = eventData.position;
        PointerId = eventData.pointerId;
    }
}
