using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;

    public void OnDrag(PointerEventData eventData)
    {
        // находим позицию касания
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            eventData.pressEventCamera,
            out pos);

        // считаем направление (-1 до 1)
        Vector2 direction = pos / (background.sizeDelta.x / 2);

        // ограничиваем длину
        if (direction.magnitude > 1)
            direction = direction.normalized;

        // двигаем ручку
        handle.anchoredPosition = direction * (background.sizeDelta.x / 2) * 0.8f;

        // передаем в менеджер
        if (InputManager.instance != null)
            InputManager.instance.joystickInput = direction;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InputManager.instance.SetControlMethod("joystick");
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // сбрасываем все
        handle.anchoredPosition = Vector2.zero;

        if (InputManager.instance != null)
            InputManager.instance.joystickInput = Vector2.zero;
    }
}