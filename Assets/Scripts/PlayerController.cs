using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // скорость движения игрока
    public float moveSpeed = 5f;

    // ссылки на компоненты
    private Rigidbody2D rb;
    private Vector3 startPosition;

    // для буста скорости
    private float normalSpeed;
    private float speedBoostEndTime = 0f;

    // флаг для состояния игрока
    private bool isDead = false;

    void Start()
    {
        // получаем компоненты
        rb = GetComponent<Rigidbody2D>();

        // сохраняем начальную позицию для респавна
        startPosition = transform.position;

        // настраиваем физику
        if (rb != null)
        {
            rb.gravityScale = 0f; // отключаем гравитацию для 2D движения
            rb.freezeRotation = true; // запрещаем вращение
        }

        // запоминаем начальную скорость
        normalSpeed = moveSpeed;

        Debug.Log("PlayerController запущен");
    }

    void Update()
    {
        // если игрок мертв, не обрабатываем движение
        if (isDead)
            return;

        // проверяем завершился ли эффект ускорения
        if (Time.time > speedBoostEndTime && moveSpeed != normalSpeed)
        {
            moveSpeed = normalSpeed;
            Debug.Log("Эффект ускорения закончился");
        }
    }

    void FixedUpdate()
    {
        // если игрок мертв, не двигаемся
        if (isDead)
            return;

        // вектор движения
        Vector2 movement = Vector2.zero;

        // проверяем наличие InputManager
        if (InputManager.instance != null)
        {
            // используем ввод из менеджера
            movement = InputManager.instance.moveInput;

            // убираемся для отладки
            if (movement.magnitude > 0.1f)
            {
                Debug.Log("Движение из InputManager: " + movement);
            }
        }
        else
        {
            // если нет InputManager, используем простой ввод с клавиатуры
            float moveX = 0f;
            float moveY = 0f;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                moveY = 1f;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                moveY = -1f;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                moveX = -1f;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                moveX = 1f;
            }

            movement = new Vector2(moveX, moveY);

            // для диагоналей нормализуем вектор
            if (movement.magnitude > 1f)
            {
                movement.Normalize();
            }

            // вывод для отладки
            if (movement.magnitude > 0.1f)
            {
                Debug.Log("Движение с клавиатуры: " + movement);
            }
        }

        // применяем движение к физике
        if (rb != null)
        {
            rb.linearVelocity = movement * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Столкновение с: " + collision.gameObject.name + ", Тег: " + collision.gameObject.tag);

        // проверяем столкнулись ли с трубой
        if (collision.gameObject.CompareTag("Pipe"))
        {
            Debug.Log("Столкновение с трубой!");

            // получаем систему здоровья
            HealthSystem health = GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(1);
                Debug.Log("Здоровье: " + health.currentHealth);

                // телепортируем игрока в начальную позицию
                transform.position = startPosition;

                // останавливаем движение
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                }

                // если здоровье закончилось
                if (health.currentHealth <= 0)
                {
                    isDead = true;
                    Debug.Log("Игрок умер!");
                }
            }
            else
            {
                Debug.LogError("На игроке нет компонента HealthSystem!");
            }
        }
    }

    // метод для ускорения движения
    public void BoostSpeed(float duration)
    {
        moveSpeed = normalSpeed * 1.5f;
        speedBoostEndTime = Time.time + duration;
        Debug.Log("Ускорение активировано на " + duration + " секунд");
    }

    // сброс состояния игрока (для перезапуска)
    public void Reset()
    {
        isDead = false;
        moveSpeed = normalSpeed;
        transform.position = startPosition;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        Debug.Log("Состояние игрока сброшено");
    }
}