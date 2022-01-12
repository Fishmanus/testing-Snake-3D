using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    private Vector3 _camOffset;

    private void Start()
    {
        _camOffset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        position.z = _camOffset.z + target.transform.position.z;

        transform.position = position;
    }
}
