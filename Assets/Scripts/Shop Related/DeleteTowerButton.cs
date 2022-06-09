using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteTowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    private bool _isMouseOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isMouseOver = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_isMouseOver)
        {

        }
    }
}
