using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState3 : IState
{

    BossSM _bossSM;
    GameObject _bossObject;
    RotatorAround _armRotator;
    float _moveSpeed;

    //Delay variables
    float _delayDuration = 1f;
    float _elapsedTime = 0f;
    bool _timerActive = false;

    public BossMoveState3(BossSM bossSM, float moveSpeed)
    {
        _bossSM = bossSM;
        _moveSpeed = moveSpeed;
    }

    int stateRepeats = 0;

    Vector3 finalPos = new Vector3(14f, 1.15f, 22f);

    public void Enter()
    {
        _bossObject = _bossSM.gameObject;
        _armRotator = _bossObject.transform.Find("ChargeSpinCenter").GetComponent<RotatorAround>();
    }

    public void Tick()
    {

        if (_timerActive)
        {
            _elapsedTime += Time.deltaTime;
        }

        if (_elapsedTime > _delayDuration && _timerActive == true)
        {
            StopTimer();
            finalPos.x *= -1;
            stateRepeats++;
            if (stateRepeats == 4) finalPos.x = 0;
        }

        float distanceFromTarget = Vector3.Distance(finalPos, _bossObject.transform.position);

        if (distanceFromTarget < 0.1f && _timerActive == false)
        {
            StartTimer();
        }
        else
        {
            Move();
        }

        if (stateRepeats == 5)
        {
            _bossSM.RandomState();
            finalPos = new Vector3(14f, 1.15f, 22f);
            stateRepeats = 0;
        }
    }

    void Move()
    {
        _bossObject.transform.position = Vector3.Lerp(_bossObject.transform.position, finalPos, (_moveSpeed / 2.5f) * Time.deltaTime);
    }

    void StartTimer()
    {
        if (stateRepeats == 0) SpinStart();
        if (stateRepeats == 1) SpinStart();
        if (stateRepeats == 2) SpinStart();
        if (stateRepeats == 3) SpinStart();

        _timerActive = true;
        _elapsedTime = 0;
    }

    void StopTimer()
    {
        if (stateRepeats == 0) SpinEnd();
        if (stateRepeats == 1) SpinEnd();
        if (stateRepeats == 2) SpinEnd();
        if (stateRepeats == 3) SpinEnd();
        _timerActive = false;
    }

    void SpinStart()
    {
        _armRotator._rotateSpeed = 720;
    }

    void SpinEnd()
    {
        _armRotator._rotateSpeed = 0;
        _armRotator.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        _bossSM.Shoot();
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {

    }

}
