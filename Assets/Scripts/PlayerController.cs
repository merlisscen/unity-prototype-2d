using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float normalSpeed;
    private float speedBoostEndTime = 0f;  // когда закончится ускорение

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalSpeed = moveSpeed;  // запоминаем начальную скорость
    }

    void Update()
    {
        // получаем ввод от менеджера ввода
        Vector2 movement = InputManager.instance.moveInput;
        rb.linearVelocity = movement * moveSpeed;

        // проверяем не кончилось ли ускорение
        if (Time.time > speedBoostEndTime && moveSpeed != normalSpeed)
        {
            moveSpeed = normalSpeed;
        }
    }

    public void BoostSpeed(float duration)
    {
        moveSpeed = normalSpeed * 1.5f;
        speedBoostEndTime = Time.time + duration;
    }
}