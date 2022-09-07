using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] ParticleSystem _projectileParticles;
    [SerializeField] AudioClip _projectileSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponentInParent<Player>() == null)
        {
            Feedback();
            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        Invoke(nameof(Delt), 2f);
    }

    private void Delt()
    {
        Destroy(this.gameObject);
    }

    private void Feedback()
    {
        //particles
        if (_projectileParticles != null)
        {
            _projectileParticles = Instantiate(_projectileParticles, transform.position, Quaternion.identity);
        }
        //audio -- consider object pooling for performance
        if (_projectileSound != null)
        {
            AudioHelper.PlayClip2D(_projectileSound, 1f);
        }
    }
}
