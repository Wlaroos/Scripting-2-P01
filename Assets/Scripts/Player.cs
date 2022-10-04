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

    float iFrameDuration = 0.5f;
    int flashes = 4;
    bool _invincible = false;

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
        if (_invincible == false)
        {
            _currentHealth -= amount;
            StartCoroutine(IFrames());
            PlayerDamage?.Invoke();
            HitFeedback();
            Debug.Log("Player's Health: " + _currentHealth);
            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void KillZone(int amount)
    {
        _currentHealth -= amount;
        StartCoroutine(IFrames());
        PlayerDamage?.Invoke();
        HitFeedback();
        Debug.Log("Player's Health: " + _currentHealth);
        if (_currentHealth <= 0)
        {
            Kill();
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
        _invincible = true;
    }

    public void FlipEnd()
    {
        _invincible = false;
    }

    public void PowerUp(Color color1, Color color2)
    {

        _invincible = true;

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

        _invincible = false;

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

    private IEnumerator IFrames()
    {
        _invincible = true;
        for (int i = 0; i < flashes; i++)
        {
            //Change Color to Red
            yield return new WaitForSeconds(iFrameDuration / flashes * 2);
            //Reset Color to White
            yield return new WaitForSeconds(iFrameDuration / flashes * 2);
        }
        _invincible = false;
    }

}
