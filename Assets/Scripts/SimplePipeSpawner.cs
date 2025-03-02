using UnityEngine;

public class SimplePipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnInterval = 3f;
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
        float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x;

        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPosition = new Vector3(rightEdge, randomY, 0f);
        GameObject newPipe = Instantiate(pipePrefab, spawnPosition, Quaternion.identity);

        if (newPipe.GetComponent<PipeMovement>() == null)
        {
            newPipe.AddComponent<PipeMovement>();
        }

        Rigidbody2D rb = newPipe.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Destroy(rb);
        }
    }
}