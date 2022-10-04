using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : IState
{

    BossSM _bossSM;
    GameObject _laserHolder;
    GameObject _warningLine;

    public BossIdleState(BossSM bossSM, GameObject laserHolder, GameObject warningLine)
    {
        _bossSM = bossSM;
        _laserHolder = laserHolder;
        _warningLine = warningLine;
    }

    public void Enter()
    {

    }

    public void Tick()
    {

    }

    public void FixedTick()
    {

    }

    public void Exit()
    {

    }

}
