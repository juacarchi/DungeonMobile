using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    Vector2 anchoredPosInitial;
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(true);
        anchoredPosInitial = background.anchoredPosition;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.anchoredPosition = anchoredPosInitial;
        base.OnPointerUp(eventData);
    }
}