using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<CrystalCollected>().isFever)
            {
                Destroy(gameObject);
            }
            else
            {
                other.GetComponent<SegmentMovement>().KillSnake();
                Debug.Log("Game over!");
                //GameOver();   
            }
        }
    }
}
