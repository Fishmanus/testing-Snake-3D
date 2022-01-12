using System.Collections.Generic;
using UnityEngine;


public class SegmentMovement : MonoBehaviour
{
    private List<Transform> _segments;
    public Transform head;
    public Transform segmentPrefab;
    public int segmentsMaximum;
    public float segmentOffset; // distance between segments

    [Range(0, 1)] public float interpolationRatio;

    // Start is called before the first frame update
    void Start()
    {
        _segments = new List<Transform> {head};
    }


    void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = Vector3.Lerp(_segments[i].position,
                _segments[i - 1].position + new Vector3(0, 0, -segmentOffset), interpolationRatio);
        }
    }

    public void GrowSegment()
    {
        if (_segments.Count >= segmentsMaximum) return;

        var segment = Instantiate(segmentPrefab);
        segment.transform.position = _segments[_segments.Count - 1].position;
        segment.gameObject.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

        _segments.Add(segment);
    }

    public void KillSnake()
    {
        PopSegment();
    }

    private void PopSegment()
    {
        if (_segments.Count > 0)
        {
            _segments[_segments.Count-1].gameObject.SetActive(false);
            _segments.RemoveAt(_segments.Count-1);
            Invoke(nameof(PopSegment), 0.075f);
        }
    }
}
