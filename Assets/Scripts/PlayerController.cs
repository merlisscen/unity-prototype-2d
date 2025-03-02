using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // получаем ввод от менеджера ввода
        Vector2 movement = InputManager.instance.moveInput;
        rb.linearVelocity = movement * moveSpeed;
    }
}