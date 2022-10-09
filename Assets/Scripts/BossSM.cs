using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSM : StateMachineMB
{

    public BossIdleState IdleState { get; private set; }
    public BossMoveState1 MoveState1 { get; private set; }
    public BossMoveState2 MoveState2 { get; private set; }
    public BossMoveState3 MoveState3 { get; private set; }
    public BossMoveState4 MoveState4 { get; private set; }

    [Header("Required References")]
    [SerializeField] GameObject _laserHolder = null;
    [SerializeField] GameObject _warningLine = null;
    [SerializeField] GameObject _bossBullet = null;
    [SerializeField] GameObject _floorRef = null;

    //bool direction = false;
    //int stateRepeats = 0;
    List<Vector3> _pos = new List<Vector3>();
    List<Quaternion> _rot = new List<Quaternion>();
    float _moveSpeed = 20f;
    float _shootVelocity = 750f;

    private void Awake()
    {
        IdleState = new BossIdleState(this, _laserHolder, _warningLine);
        MoveState1 = new BossMoveState1(this, _laserHolder, _warningLine, _moveSpeed);
        MoveState2 = new BossMoveState2(this, _laserHolder, _warningLine, _moveSpeed);
        MoveState3 = new BossMoveState3(this, _moveSpeed);
        MoveState4 = new BossMoveState4(this, _laserHolder, _warningLine, _moveSpeed);
    }

    private void Start()
    {
        ChangeState(IdleState);

        Invoke(nameof(RandomState), 3f);
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(_bossBullet, gameObject.transform.Find("ShootPos1").transform.position, transform.rotation);
        GameObject bullet2 = Instantiate(_bossBullet, gameObject.transform.Find("ShootPos2").transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * _shootVelocity);
        bullet2.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * _shootVelocity);
    }

    public void RandomState()
    {
        switch (Random.Range(1, 5))
        {
            case 1: ChangeState(MoveState1); Debug.Log("State1"); break;
            case 2: ChangeState(MoveState2); Debug.Log("State2"); break;
            case 3: ChangeState(MoveState3); Debug.Log("State3"); break;
            case 4: ChangeState(MoveState4); Debug.Log("State4"); break;
        }

    }

}
