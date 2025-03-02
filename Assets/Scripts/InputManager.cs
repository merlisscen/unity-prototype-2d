using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [HideInInspector] public Vector2 moveInput;

    private bool usingKeyboard = true;
    private bool usingJoystick = false;
    private bool usingGyro = false;

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

    void Update()
    {
        // только один метод ввода активен одновременно
        if (usingKeyboard)
        {
            GetKeyboardInput();
        }
        else if (usingJoystick)
        {
        }
        else if (usingGyro)
        {
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