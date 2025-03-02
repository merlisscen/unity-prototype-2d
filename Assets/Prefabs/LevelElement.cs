using UnityEngine;

public enum ElementType
{
    Damaging,
    Points,
    Speed,
    Health
}

public class LevelElement : MonoBehaviour
{
    public ElementType type;
    public int value = 1;
    public float duration = 3f; // для временных эффектов как скорость

    private PlayerController activeController; // добавляем сохранение ссылки

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case ElementType.Damaging:
                    other.GetComponent<HealthSystem>().TakeDamage(value);
                    break;

                case ElementType.Points:
                    other.GetComponent<ScoreSystem>().AddPoints(value);
                    break;

                case ElementType.Speed:
                    StartSpeedBoost(other.gameObject, duration);
                    break;

                case ElementType.Health:
                    other.GetComponent<HealthSystem>().AddHealth(value);
                    break;
            }

            // уничтожаем объект после взаимодействия
            Destroy(gameObject);
        }
    }

    void StartSpeedBoost(GameObject player, float duration)
    {
        activeController = player.GetComponent<PlayerController>();
        if (activeController != null)
        {
            activeController.moveSpeed *= 1.5f;

            // сохраняем ссылку и вызываем через duration секунд
            Invoke("EndSpeedBoost", duration);
        }
    }

    void EndSpeedBoost()
    {
        if (activeController != null)
        {
            activeController.moveSpeed /= 1.5f;
        }
    }
}