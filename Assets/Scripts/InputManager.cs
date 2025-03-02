using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Vector2 joystickInput;

    private bool usingKeyboard = true;
    private bool usingJoystick = false;
    private bool usingGyro = false;

    public float gyroSensitivity = 2.0f;
    private bool gyroInitialized = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // проверяем есть ли гироскоп
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            gyroInitialized = true;
        }
    }

    void Update()
    {
        // проверяем какой метод ввода используется сейчас
        if (usingKeyboard)
        {
            GetKeyboardInput();
        }
        else if (usingJoystick)
        {
            GetJoystickInput();
        }
        else if (usingGyro)
        {
            GetGyroInput();
        }
    }

    void GetKeyboardInput()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            y = 1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            y = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            x = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            x = 1f;
        }

        moveInput = new Vector2(x, y);
        if (moveInput.magnitude > 1f)
        {
            moveInput.Normalize();
        }
    }

    void GetJoystickInput()
    {
        moveInput = joystickInput;
    }

    void GetGyroInput()
    {
        if (!gyroInitialized)
            return;

        // получаем наклон устройства
        float x = Input.gyro.attitude.x * gyroSensitivity;
        float y = Input.gyro.attitude.y * gyroSensitivity;

        // ограничиваем значения
        x = Mathf.Clamp(x, -1f, 1f);
        y = Mathf.Clamp(y, -1f, 1f);

        moveInput = new Vector2(x, y);
    }

    public void SetControlMethod(string method)
    {
        usingKeyboard = false;
        usingJoystick = false;
        usingGyro = false;

        if (method == "keyboard")
        {
            usingKeyboard = true;
        }
        else if (method == "joystick")
        {
            usingJoystick = true;
        }
        else if (method == "gyro")
        {
            usingGyro = true;
        }
    }
}