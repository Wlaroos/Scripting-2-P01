using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp(Player player);
    protected abstract void PowerDown(Player player);

    [SerializeField] float _powerupDuration;
    [SerializeField] ParticleSystem _powerupParticles;
    [SerializeField] AudioClip _powerupSound;
    [SerializeField] AudioClip _powerdownSound;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PowerUp(player);
            Feedback();
            //particles and sfx because we need to disable

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(powerWait(player));
        }
    }

    IEnumerator powerWait(Player player)
    {
        yield return new WaitForSecondsRealtime(_powerupDuration);

        PowerDown(player);

        if (_powerdownSound != null)
        {
            AudioHelper.PlayClip2D(_powerdownSound, 1f);
        }

        Destroy(this.gameObject);
    }

    private void Feedback()
    {
        //particles
        if (_powerupParticles != null)
        {
            _powerupParticles = Instantiate(_powerupParticles, transform.position, Quaternion.identity);
        }
        //audio -- consider object pooling for performance
        if (_powerupSound != null)
        {
            AudioHelper.PlayClip2D(_powerupSound, 1f);
        }
    }

}
