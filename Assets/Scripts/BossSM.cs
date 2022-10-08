using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSM : StateMachineMB
{

    public BossIdleState IdleState { get; private set; }
    public BossMoveState1 MoveState1 { get; private set; }
    public BossMoveState2 MoveState2 { get; private set; }
    public BossMoveState3 MoveState3 { get; private set; }

    [Header("Required References")]
    [SerializeField] GameObject _laserHolder = null;
    [SerializeField] GameObject _warningLine = null;
    [SerializeField] GameObject _bossBullet = null;
    [SerializeField] GameObject _floorRef = null;

    //bool direction = false;
    //int stateRepeats = 0;
    List<Vector3> _pos = new List<Vector3>();
    List<Quaternion> _rot = new List<Quaternion>();
    float _moveSpeed = 15f;
    float _shootVelocity = 750f;

    private void Awake()
    {
        IdleState = new BossIdleState(this, _laserHolder, _warningLine);
        MoveState1 = new BossMoveState1(this, _laserHolder, _warningLine, _moveSpeed);
        MoveState2 = new BossMoveState2(this, _laserHolder, _warningLine, _moveSpeed);
        MoveState3 = new BossMoveState3(this, _bossBullet, _moveSpeed);
    }

    private void Start()
    {
        ChangeState(IdleState);
        //Debug.Log("IDLE");

        Invoke(nameof(Testing), 3f);
    }

    private void Testing()
    {
        ChangeState(MoveState3);
        //Debug.Log("MOVE 01");
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(_bossBullet, gameObject.transform.Find("ShootPos1").transform.position, transform.rotation);
        GameObject bullet2 = Instantiate(_bossBullet, gameObject.transform.Find("ShootPos2").transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * _shootVelocity);
        bullet2.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * _shootVelocity);
    }

}
