using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    private float _heatMax = 20f;

    private bool _cooling = false;

    private bool _flipping = false;

    public event Action HeatChange;
    public event Action<bool> CoolingChange;

    private void FixedUpdate()
    {
        //Debug.Log(_heatAmount);

        if(_heatAmount > 0 && _cooling == true)
        {
            _heatAmount -= _heatDecrease;
            HeatChange?.Invoke();
        }
        else if(_heatAmount > 0 && Input.GetButton("Fire1") == false && _cooling == false)
        {
            _heatAmount -= (2.5f * _heatDecrease);
            HeatChange?.Invoke();
        }
        else
        {
            _cooling = false;
            CoolingChange?.Invoke(false);
        }

        if(_heatAmount > _heatMax)
        {
            _cooling = true;
            CoolingChange?.Invoke(true);
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > _nextFire && _cooling == false && _flipping == false)
        {
            _nextFire = Time.time + _fireRate;

            _heatAmount += _heatIncrease;
            HeatChange?.Invoke();

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
            AudioHelper.PlayClip2D(_shootSounds[UnityEngine.Random.Range(0, 3)], 0.25f);
        }
        ScreenShake.ShakeOnce(.25f, 5, new Vector3(.5f, .5f, 0));
    }

    public void Flip()
    {
        _flipping = true;
    }

    public void FlipEnd()
    {
        _flipping = false;
    }

    public float HeatPercent()
    {
        return ((float)_heatAmount / (float)_heatMax);
    }
}
