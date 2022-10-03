using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Player : MonoBehaviour
{

    [SerializeField] int _maxHealth = 3;

    [SerializeField] TextMeshProUGUI _tmp;

    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] AudioClip _deathSound;
    [SerializeField] ParticleSystem _hitParticles;
    [SerializeField] AudioClip _hitSound;

    public event Action PlayerDamage;

    int _treasureCount;
    int _currentHealth;

    bool invin = false;

    TankController _tankController;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's Health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
        if (invin == false)
        {
            _currentHealth -= amount;
            PlayerDamage?.Invoke();
            HitFeedback();
            Debug.Log("Player's Health: " + _currentHealth);
            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void IncreaseTreasure(int amount)
    {
        _treasureCount += amount;
        _tmp.text = "Treasure: " + _treasureCount;
        Debug.Log("Player's Treasure: " + _treasureCount);
    }

    public void Flip()
    {
        invin = true;
    }

    public void FlipEnd()
    {
        invin = false;
    }

    public void PowerUp(Color color1, Color color2)
    {

        invin = true;

     /*   foreach (GameObject j in tankObjects)
        {
            if (j.name.Contains("Tread"))
            {
                j.GetComponent<MeshRenderer>().material.color = color1;
            }
            else
            {
                j.GetComponent<MeshRenderer>().material.color = color2;
            }
        }*/
    }

    public void PowerDown(Color color1, Color color2)
    {

        invin = false;

/*        foreach (GameObject j in tankObjects)
        {
            if (j.name.Contains("Tread"))
            {
                j.GetComponent<MeshRenderer>().material.color = color1;
            }
            else
            {
                j.GetComponent<MeshRenderer>().material.color = color2;
            }
        }*/
    }

    public void Kill()
    {
        if (invin == false)
        {
            gameObject.SetActive(false);

            //particles
            if (_deathParticles != null)
            {
                _deathParticles = Instantiate(_deathParticles, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(-90, 0, 0));
            }
            //audio -- consider object pooling for performance
            if (_deathSound != null)
            {
                AudioHelper.PlayClip2D(_deathSound, 1f);
            }

            ScreenShake.ShakeOnce(1f, 5f, new Vector3(3, 3, 0));
        }
    }

    public float HealthPercent()
    {
        return ((float)_currentHealth / (float)_maxHealth);
    }

    private void HitFeedback()
    {
        //particles
        if (_hitParticles != null)
        {
            _hitParticles = Instantiate(_hitParticles, transform.position + new Vector3(0,1,0), Quaternion.Euler(-90, 0, 0));
        }
        //audio -- consider object pooling for performance
        if (_hitSound != null)
        {
            AudioHelper.PlayClip2D(_hitSound, 1f);
        }
    }

}
