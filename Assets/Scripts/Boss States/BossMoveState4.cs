using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState4 : IState
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

    public BossMoveState4(BossSM bossSM, GameObject laserHolder, GameObject warningLine, float moveSpeed)
    {
        _bossSM = bossSM;
        _laserHolder = laserHolder;
        _warningLine = warningLine;
        _moveSpeed = moveSpeed;
    }

    bool direction = false;
    int stateRepeats = 0;

    List<Quaternion> _rot = new List<Quaternion>();
    Quaternion finalRot;

    public void Enter()
    {
        _bossObject = _bossSM.gameObject;

        _rot.Add(Quaternion.Euler(90, 50, 0));
        _rot.Add(Quaternion.Euler(90, -50, 0));
        _rot.Add(Quaternion.Euler(90, 50, 0));
        _rot.Add(Quaternion.Euler(90, 0, 0));

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
            _delayDuration = .75f;

            if (stateRepeats < 4) finalRot = _rot[stateRepeats];

            stateRepeats++;

            if (stateRepeats == 5) finalRot = Quaternion.Euler(90,0,0);
            //Debug.Log("REPEAT: " + stateRepeats);
        }

        float distanceFromTarget = Quaternion.Angle(finalRot, _bossObject.transform.rotation);

        if (distanceFromTarget < 0.1f && _timerActive == false)
        {
            StartTimer();
        }
        else
        {
            Rotate();
        }

        if (stateRepeats == 5)
        {
            _bossSM.RandomState();
            stateRepeats = 0;
        }
    }

    void Rotate()
    {
        _bossObject.transform.rotation = Quaternion.RotateTowards(_bossObject.transform.rotation, finalRot, _moveSpeed * 2.15f * Time.deltaTime);
    }

    void StartTimer()
    {
        if (stateRepeats == 1) { _warningLine.SetActive(true); _bossSM.LaserChargeSFX(); }
        if (stateRepeats == 2) { _warningLine.SetActive(true); _bossSM.LaserChargeSFX(); }
        if (stateRepeats == 2) _laserHolder.SetActive(false);
        if (stateRepeats == 3) _laserHolder.SetActive(false);

        _timerActive = true;
        _elapsedTime = 0;
    }

    void StopTimer()
    {
        if (stateRepeats == 1) _laserHolder.SetActive(true);
        if (stateRepeats == 2) _laserHolder.SetActive(true);

        if (stateRepeats == 1) _warningLine.SetActive(false);
        if (stateRepeats == 2) _warningLine.SetActive(false);


        _timerActive = false;
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {

    }

}
