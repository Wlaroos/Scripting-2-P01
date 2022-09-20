using System.Collections;
using System;

public interface IState
{
    // automatically gets called in the state machine, allows you to delay flow
    void Enter();
    // simulates Update() method
    void Tick();
    // simulates FixedUpdate() method
    void FixedTick();
    // automatically gets called in the state machine, allows you to delay flow
    void Exit();

}
