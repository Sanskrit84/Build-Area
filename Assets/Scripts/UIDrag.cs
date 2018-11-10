using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 offset;
    private Vector3 newpos;


    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = new Vector3(this.transform.position.x - eventData.position.x, this.transform.position.y - eventData.position.y, 0.0f);
        //Debug.Log("OnBeginDrag: " + offset);

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag: " + this.transform.position);
        newpos = new Vector3(eventData.position.x + offset.x, eventData.position.y + offset.y, 0.0f);
        this.transform.position = newpos;


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
    }



}
