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
    }

    public void Tick()
    {
        Vector3 startingPos = _bossObject.transform.position;
        Vector3 finalPos = _pos[stateRepeats];
        Quaternion startingRot = _bossObject.transform.rotation;
        Quaternion finalRot = _rot[stateRepeats];

        //float elapsedTime = 0;

        /*if (elapsedTime < _lerpTime)
        {
            _bossObject.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / _lerpTime));
            _bossObject.transform.rotation = Quaternion.Lerp(startingRot, finalRot, (elapsedTime / _lerpTime));
            elapsedTime += Time.deltaTime;
            //yield return null;
        }
        else
        {
            if (direction == false)
            {
                stateRepeats++;
            }
            else
            {
                stateRepeats--;
            }

            if (stateRepeats < 4 && direction == false)
            {
                _bossSM.ChangeState(_bossSM.MoveState2);
            }
            else if (stateRepeats >= 4 && direction == false)
            {
                stateRepeats = 2;
                direction = true;
                _bossSM.ChangeState(_bossSM.MoveState2);
            }
            else if (stateRepeats >= 0 && direction == true)
            {
                _bossSM.ChangeState(_bossSM.MoveState2);
            }
            else if (stateRepeats < 0 && direction == true)
            {
                stateRepeats = -1;
                _bossSM.ChangeState(_bossSM.MoveState1);
            }
        }

        */
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {

    }

}
