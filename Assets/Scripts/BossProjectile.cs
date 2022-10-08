using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{

    [SerializeField] ParticleSystem _projectileParticles;
    [SerializeField] AudioClip[] _projectileSounds;
    Transform _playerRef;
    [SerializeField] Rigidbody _rb;
    [SerializeField] int _damage;
    [SerializeField] int _health;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponentInParent<Player>() != null)
        {
            collision.transform.GetComponentInParent<Player>().DecreaseHealth(_damage, false);
            Feedback();
            Delt();
        }
        else if (collision.transform.GetComponent<Projectile>() != null)
        {
            _rb.AddForce(collision.transform.forward * 3000);
            _health--;
            if(_health <= 0)
            {
                Feedback();
                Delt();
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 directionVector = (_playerRef.transform.position - transform.position).normalized;
        _rb.AddForce(directionVector * 150);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerRef = GameObject.Find("Tank").transform;
    }

    private void Delt()
    {
        Destroy(gameObject);
    }

    private void Feedback()
    {
        //particles
        if (_projectileParticles != null)
        {
            _projectileParticles = Instantiate(_projectileParticles, transform.position, Quaternion.identity);
        }
        //audio -- consider object pooling for performance
        if (_projectileSounds[0] != null)
        {
            AudioHelper.PlayClip2D(_projectileSounds[Random.Range(0, 3)], 0.25f);
        }
    }
}
