using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Material _material;
    
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Segment") || other.CompareTag("Player"))
        {
            other.GetComponent<Renderer>().material = _material;
        }
    }
}
