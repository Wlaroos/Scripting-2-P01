using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ShaderMASK : MonoBehaviour
{

    [SerializeField] MeshRenderer _meshRenderer;
    Material _mat;
    private float radius;

    private void Start()
    {
        radius = GetComponent<SphereCollider>().radius;
        _mat = _meshRenderer.sharedMaterial;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _mat.SetVector("_SphereParams", new Vector4(transform.position.x, transform.position.y, transform.position.z, radius));
    }
}
