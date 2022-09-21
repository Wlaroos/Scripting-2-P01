using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject _projectile;
    [SerializeField] float _shootVelocity = 1000f;

    [SerializeField] ParticleSystem _shootParticles;
    [SerializeField] AudioClip[] _shootSounds;

    [SerializeField] private float _fireRate = 0.15f;
    private float _nextFire = 0.1f;
    [SerializeField] private float _heatDecrease = 0.1f;
    [SerializeField] private float _heatIncrease = 1f;

    private float _heatAmount = 0f;

    private bool _cooling = false;

    private bool _flipping = false;

    private void FixedUpdate()
    {
        //Debug.Log(_heatAmount);

        if(_heatAmount > 0 && _cooling == true)
        {
            _heatAmount -= _heatDecrease;
        }
        else if(_heatAmount > 0 && Input.GetButton("Fire1") == false && _cooling == false)
        {
            _heatAmount -= (2f * _heatDecrease);
        }
        else
        {
            _cooling = false;
        }

        if(_heatAmount > 20)
        {
            _cooling = true;
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > _nextFire && _cooling == false && _flipping == false)
        {
            _nextFire = Time.time + _fireRate;

            _heatAmount += _heatIncrease;

            Feedback();

            GameObject bullet = Instantiate(_projectile, gameObject.transform.GetChild(2).transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, _shootVelocity));
        }
    }

    private void Feedback()
    {
        //particles
        if (_shootParticles != null)
        {
            _shootParticles = Instantiate(_shootParticles, gameObject.transform.GetChild(2).transform.position, transform.rotation);
        }
        //audio -- consider object pooling for performance
        if (_shootSounds[0] != null)
        {
            AudioHelper.PlayClip2D(_shootSounds[Random.Range(0, 3)], 0.25f);
        }
    }

    public void Flip()
    {
        _flipping = true;
    }

    public void FlipEnd()
    {
        _flipping = false;
    }
}
