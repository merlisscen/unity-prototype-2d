using UnityEngine;

public class SimplePipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnInterval = 2f;
    public float minY = -3f;
    public float maxY = 3f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPipe();
            timer = 0f;
        }
    }

    void SpawnPipe()
    {
        // определяем случайную высоту для проема
        float randomY = Random.Range(minY, maxY);

        // создаем трубу справа от экрана
        Vector3 spawnPosition = new Vector3(10f, randomY, 0f);
        Instantiate(pipePrefab, spawnPosition, Quaternion.identity);

        Debug.Log("Создана новая труба на позиции: " + spawnPosition);
    }
}