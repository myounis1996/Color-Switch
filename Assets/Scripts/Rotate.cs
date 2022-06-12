using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private float[] direction = { -1f, 1f };
    int index;

    void Start()
    {
        index = Random.Range(0, direction.Length);
    }
    void Update()
    {
        transform.Rotate(0f, 0f, direction[index] * rotationSpeed * Time.deltaTime);
    }
}
