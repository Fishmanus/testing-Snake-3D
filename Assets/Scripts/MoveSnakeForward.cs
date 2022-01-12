using UnityEngine;

public class MoveSnakeForward : MonoBehaviour
{
    public float snakeSpeed;

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        position.z += snakeSpeed * Time.deltaTime;

        transform.position = position;
    }
}
