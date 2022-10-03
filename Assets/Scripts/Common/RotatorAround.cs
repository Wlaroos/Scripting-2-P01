using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorAround : MonoBehaviour
{
    [SerializeField] Transform _pivotPoint;
    [SerializeField] bool _axisManual = false;
    [SerializeField] Vector3 _rotateDirection = new Vector3(1, 0, 0);
    [SerializeField] float _rotateSpeed = 25;

    void Update()
    {
        if (_axisManual == false)
        {
            transform.RotateAround(_pivotPoint.position, _pivotPoint.up, _rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(_pivotPoint.position, _rotateDirection, _rotateSpeed * Time.deltaTime);
        }
    }
}