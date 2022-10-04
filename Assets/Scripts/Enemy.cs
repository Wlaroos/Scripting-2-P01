using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int _damageAmount = 1;

    Rigidbody _rb;

    bool once = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PlayerImpact(player);
            ImpactFeedback();
        }
    }

    public void Move()
    {

    }

    protected virtual void PlayerImpact(Player player)
    {
        player.DecreaseHealth(_damageAmount, false);
    }

    public void ImpactFeedback()
    {

    }
}
