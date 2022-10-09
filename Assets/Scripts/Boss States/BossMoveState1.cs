using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState1 : IState
{

    BossSM _bossSM;
    GameObject _bossObject;
    GameObject _laserHolder;
    GameObject _warningLine;
    float _moveSpeed;

    //Delay variables
    float _delayDuration = 0.2f;
    float _elapsedTime = 0f;
    bool _timerActive = false;

    public BossMoveState1(BossSM bossSM, GameObject laserHolder, GameObject warningLine, float moveSpeed)
    {
        _bossSM = bossSM;
        _laserHolder = laserHolder;
        _warningLine = warningLine;
        _moveSpeed = moveSpeed;
    }

    int stateRepeats = 0;

    Vector3 finalPos = new Vector3(14f, 1.15f, 22f);
    Quaternion finalRot = Quaternion.Euler(90, 0, 0);


    public void Enter()
    {
        _bossObject = _bossSM.gameObject;
    }

    public void Tick()
    {

        if(_timerActive)
        {
            _elapsedTime += Time.deltaTime;
        }

        if(_elapsedTime > _delayDuration && _timerActive == true)
        {
            StopTimer();
            _delayDuration = 0.2f;
            finalPos.x *= -1;
            stateRepeats++;
            if (stateRepeats == 1) _delayDuration = .75f;
            if (stateRepeats == 2) _warningLine.SetActive(false);
            if (stateRepeats == 4) finalPos.x = 0;
            //Debug.Log("REPEAT: " + stateRepeats);
        }

        float distanceFromTarget = Vector3.Distance(finalPos, _bossObject.transform.position);

        if (distanceFromTarget < 0.1f && _timerActive == false)
        {
            StartTimer();
        }
        else
        {
            Move();
            Rotate();
        }

        if (stateRepeats == 5)
        {
            //Debug.Log("BACK TO IDLE");
            _bossSM.ChangeState(_bossSM.IdleState);
            //_bossSM.ChangeState(_bossSM.MoveState2);
        }
    }

    void Move()
    {
         _bossObject.transform.position = Vector3.MoveTowards(_bossObject.transform.position, finalPos, _moveSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        _bossObject.transform.rotation = Quaternion.RotateTowards(_bossObject.transform.rotation, finalRot, _moveSpeed * 2.15f * Time.deltaTime);
    }

    void StartTimer()
    {
        if(stateRepeats == 1) _warningLine.SetActive(true);
        if(stateRepeats == 3) _laserHolder.SetActive(false);

        _timerActive = true;
        _elapsedTime = 0;
    }

    void StopTimer()
    {
        if(stateRepeats == 1) _laserHolder.SetActive(true);

        _timerActive = false;
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {

    }

}
