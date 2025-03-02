using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool scored = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Что-то вошло в триггер счета: " + other.gameObject.name);

        if (other.CompareTag("Player") && !scored)
        {
            Debug.Log("Игрок прошел через трубу!");
            scored = true;

            ScoreSystem scoreSystem = FindObjectOfType<ScoreSystem>();
            if (scoreSystem != null)
            {
                scoreSystem.AddPoints(1);
                Debug.Log("Очко добавлено!");
            }
            else
            {
                Debug.LogError("ScoreSystem не найден!");
            }
        }
    }
}