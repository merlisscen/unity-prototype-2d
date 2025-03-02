using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;

    private Vector2 input;

    public void OnDrag(PointerEventData eventData)
    {
        // простой расчет позиции
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            eventData.pressEventCamera,
            out pos);

        // находим направление
        input = pos / (background.sizeDelta.x / 2);

        // чтобы не двигался слишком быстро по диагонали
        if (input.magnitude > 1)
            input = input.normalized;

        // двигаем ручку джойстика
        handle.anchoredPosition = input * (background.sizeDelta.x / 2) * 0.8f;

        // передаем в систему ввода
        InputManager.instance.moveInput = input;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // при нажатии сразу обрабатываем позицию
        OnDrag(eventData);
        InputManager.instance.SetControlMethod("joystick");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // при отпускании сбрасываем всё
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        InputManager.instance.moveInput = Vector2.zero;
    }
}