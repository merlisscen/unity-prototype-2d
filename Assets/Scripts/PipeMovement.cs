using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
    }
}