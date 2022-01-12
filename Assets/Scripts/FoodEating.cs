using UnityEngine;

public class FoodEating : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            if (other.GetComponent<Renderer>().material.color == GetComponent<Renderer>().material.color || GetComponent<CrystalCollected>().isFever)
            {
                GetComponent<SegmentMovement>().GrowSegment(); 
                Destroy(other.gameObject);   
            }
            else
            {
                GetComponent<SegmentMovement>().KillSnake();
                Destroy(other.gameObject);
                //GameOver();
                Debug.Log("Game over");
            }
        }
    }

}
