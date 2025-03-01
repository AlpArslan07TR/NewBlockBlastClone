using UnityEngine;
using UnityEngine.EventSystems;

public class Movable : MonoBehaviour, IPointerDownHandler,IDragHandler,IPointerUpHandler
{
    public Vector3 offset; 
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        var target=Camera.main.ScreenToWorldPoint(eventData.position);
        offset = transform.position - target;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        
        var target=Camera.main.ScreenToWorldPoint(eventData.position);
        target+=offset;
        target.z=0;
        transform.position=target;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
    }
}