using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState2 : IState
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

    public BossMoveState2(BossSM bossSM, GameObject laserHolder, GameObject warningLine, float moveSpeed)
    {
        _bossSM = bossSM;
        _laserHolder = laserHolder;
        _warningLine = warningLine;
        _moveSpeed = moveSpeed;
    }

    bool direction = false;
    int stateRepeats = 0;

    List<Vector3> _pos = new List<Vector3>();
    List<Quaternion> _rot = new List<Quaternion>();
    Vector3 finalPos;
    Quaternion finalRot;

    public void Enter()
    {
        _bossObject = _bossSM.gameObject;

        _pos.Add(new Vector3(-22f, 1.15f, -14f));
        _pos.Add(new Vector3(-20f, 1.15f, 20f));
        _pos.Add(new Vector3(20f, 1.15f, 20f));
        _pos.Add(new Vector3(22f, 1.15f, -14f));
        _rot.Add(Quaternion.Euler(90, -90, 0));
        _rot.Add(Quaternion.Euler(90, -45, 0));
        _rot.Add(Quaternion.Euler(90, 45, 0));
        _rot.Add(Quaternion.Euler(90, 90, 0));

        finalPos = _pos[0];
        finalRot = _rot[0];
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
            _delayDuration = 0.2f;

            if (stateRepeats < 4)
            {
                finalPos = _pos[stateRepeats];
                finalRot = _rot[stateRepeats];
            }

            stateRepeats++;
            if (stateRepeats == 1) _delayDuration = .75f;
            if (stateRepeats == 2) _warningLine.SetActive(false);
            if (stateRepeats == 5) { finalPos = new Vector3(0,1.15f,22); finalRot = Quaternion.Euler(90,0,0); }
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

        if (stateRepeats == 6)
        {
            _bossSM.RandomState();
            stateRepeats = 0;
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
        if (stateRepeats == 1) _warningLine.SetActive(true);
        if (stateRepeats == 4) _laserHolder.SetActive(false);

        _timerActive = true;
        _elapsedTime = 0;
    }

    void StopTimer()
    {
        if (stateRepeats == 1) _laserHolder.SetActive(true);

        _timerActive = false;
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {

    }

}
