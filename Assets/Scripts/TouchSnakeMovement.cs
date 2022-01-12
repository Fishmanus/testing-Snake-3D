using System;
using UnityEngine;

public class TouchSnakeMovement : MonoBehaviour
{
    public GameObject snake;
    public MeshCollider road;
    
    [Range(0, 1)]
    public float interpolationRatio;
    
    private float _widthDelta;
    private CrystalCollected _crystalCollected;

    private void Start()
    {
        _crystalCollected = GetComponent<CrystalCollected>();
        _widthDelta = road.bounds.extents.x - snake.GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        var transformPosition = snake.transform.position;
        
        if (_crystalCollected.isFever)
        {
            transformPosition.x = Mathf.Lerp(transformPosition.x, 0, interpolationRatio); 
            snake.transform.position = transformPosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Plane objPlane = new Plane(Vector3.up, transformPosition);
            var mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (objPlane.Raycast(mRay, out var rayDist))
            {
                //Debug.Log(mRay.GetPoint(rayDist));
                transformPosition.x = Mathf.Lerp(transformPosition.x, mRay.GetPoint(rayDist).x, interpolationRatio);
                
                if (transformPosition.x > _widthDelta || transformPosition.x < -_widthDelta)
                {
                    transformPosition.x = _widthDelta * Math.Sign(transformPosition.x);
                    snake.transform.position = transformPosition;
                }else snake.transform.position = transformPosition;
            }
            
        }
    }
}
