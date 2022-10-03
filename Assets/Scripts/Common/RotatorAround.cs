using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorAround : MonoBehaviour
{
    [SerializeField] Transform _pivotPoint;
    [SerializeField] Vector3 _rotateDirection = new Vector3(1, 0, 0);
    [SerializeField] float _rotateSpeed = 25;

    void Update()
    {
        transform.RotateAround(_pivotPoint.position, _rotateDirection, _rotateSpeed * Time.deltaTime);
    }
}