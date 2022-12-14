using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] ParticleSystem _projectileParticles;
    [SerializeField] AudioClip[] _projectileSounds;
    [SerializeField] int _damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponentInParent<Player>() == null)
        {
            if (collision.transform.GetComponentInParent<IDamageable>() != null)
            {
                collision.transform.GetComponentInParent<IDamageable>().TakeDamage(_damage);
                Feedback();
                Destroy(gameObject);
            }
            else
            {
                Feedback();
                Destroy(gameObject);
            }
        }
    }

    private void Awake()
    {
        Invoke(nameof(Delt), 2f);
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
