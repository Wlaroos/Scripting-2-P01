using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] AudioClip[] _dodgeRollSFX;
    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    bool flipped = false;

    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveTank();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && flipped == false)
        {
            FlipTank();
        }
    }

    public void MoveTank()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 newPosition = new Vector3(moveHorizontal, 0.0f, moveVertical);

        transform.LookAt(newPosition + transform.position);

        transform.Translate(newPosition * _moveSpeed * Time.deltaTime, Space.World);
    }

    public void FlipTank()
    {
        _rb.AddForce(75f * transform.forward, ForceMode.Impulse);
        AudioHelper.PlayClip2D(_dodgeRollSFX[Random.Range(0, 3)], 1f);
        flipped = true;
        transform.GetComponent<Player>().Flip();
        transform.GetChild(0).GetChild(1).GetComponent<Shoot>().Flip();
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Flipped");
    }

    public void FlipEnd()
    {
        flipped = false;
    }

}
