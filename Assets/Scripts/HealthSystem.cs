using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Text healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            RestartLevel();
        }

        UpdateHealthUI();
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Жизни: " + currentHealth;
        }
    }

    void RestartLevel()
    {
        // перезагружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}