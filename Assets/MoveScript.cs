using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;

    private float xRotation = 0f;

    void Start()
    {
        // Опционально заблокировать курсор
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        // Получаем значения для движения по горизонтали и вертикали
        float horizontal = Input.GetAxis("Horizontal"); // Ввод для оси X (A/D)
        float vertical = Input.GetAxis("Vertical");     // Ввод для оси Z (W/S)

        // Направление движения без перемещения по Y
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void Rotate()
    {
        // Вращение камеры по X и Y
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничиваем вращение по Y, чтобы камера не переворачивалась

        transform.localRotation = Quaternion.Euler(xRotation, transform.eulerAngles.y + mouseX, 0f);
    }
}
