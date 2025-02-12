using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image joystickBackground;
    private Image joystick;
    private Vector2 posInput;

    private void Start()
    {
        joystickBackground = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out posInput))
        {
            posInput.x = posInput.x / (joystickBackground.rectTransform.sizeDelta.x);
            posInput.y = posInput.y / (joystickBackground.rectTransform.sizeDelta.y);

            // Normalize
            if(posInput.magnitude > 1f)
            {
                posInput = posInput.normalized;
            }


            // Joystick move

            joystick.rectTransform.anchoredPosition = new Vector2(

                posInput.x * (joystickBackground.rectTransform.sizeDelta.x / 2),
                posInput.y * (joystickBackground.rectTransform.sizeDelta.y / 2)
                );

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        posInput = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public Vector2 GetJoystickInput()
    {
        return posInput;
    }

}
