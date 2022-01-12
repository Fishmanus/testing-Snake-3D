using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public float scrollSpeed;
    public Transform followTarget;

    private MeshRenderer _meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float y = followTarget.position.z * scrollSpeed;
        Vector2 offset = new Vector2(0, y);

        _meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
