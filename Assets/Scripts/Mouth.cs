using System;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public MeshCollider road;
    public LayerMask foodLayer;
    [Range(0,1)]
    public float interpolationRatio;
    [Range(0,1)]
    public float feverInterpolationRatio;

    public float mouthRange;
    public float feverMouthRange;
    public float mouthAngle;
    public int maximumFood;

    private Collider[] _colliders;
    private float _roadWidth;
    private Transform _mouthPosition;
    
    private CrystalCollected _crystalCollected;
    private float _mouthRangeTMP;
    private float _mouthAngleTMP;
    private float _interpolationRatioTMP;

    // Debugging
    private bool _isPlayMode;

    private void Start()
    {
        _isPlayMode = true;
        _mouthPosition = GetComponent<Transform>();
        _roadWidth = road.bounds.extents.x;
        _colliders = new Collider[maximumFood];
        _crystalCollected = GetComponentInParent<CrystalCollected>();
        _mouthAngleTMP = mouthAngle;
        _mouthRangeTMP = mouthRange;
        _interpolationRatioTMP = interpolationRatio;
    }

    // Update is called once per frame
    void Update()
    {
        int size;
        if (_crystalCollected.isFever)
        {
            mouthAngle = 180;
            mouthRange = feverMouthRange;
            interpolationRatio = feverInterpolationRatio;
            
            size = Physics.OverlapBoxNonAlloc(transform.position + new Vector3(0, 0, mouthRange/2),
                new Vector3(_roadWidth*2, 1, mouthRange), _colliders);
        }
        else
        {
            mouthAngle = _mouthAngleTMP;
            mouthRange = _mouthRangeTMP;
            interpolationRatio = _interpolationRatioTMP;
            
            size = Physics.OverlapBoxNonAlloc(transform.position + new Vector3(0, 0, mouthRange/2),
                new Vector3(_roadWidth*2, 1, mouthRange), _colliders, Quaternion.identity, foodLayer);
        }
        
        //Debug.Log(size);

        foreach (var col in _colliders)
        {
            if (col is null || col.CompareTag("ColorZone")) continue;

            var colPosition = col.gameObject.transform.position; 
            var distance = Vector3.Distance(colPosition, _mouthPosition.position);
            
            if (distance < mouthRange && 
                Vector3.Angle(_mouthPosition.forward, colPosition - _mouthPosition.position) < mouthAngle/2)
            {
                col.transform.position = Vector3.Lerp(colPosition, _mouthPosition.position, interpolationRatio / distance);
            }
        }
        Array.Clear(_colliders, 0, size);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_isPlayMode)
        {
            Gizmos.DrawWireCube(transform.position + new Vector3(0, 0, mouthRange/2),
                new Vector3(_roadWidth*2, 1, mouthRange));   
        }
    }
}
