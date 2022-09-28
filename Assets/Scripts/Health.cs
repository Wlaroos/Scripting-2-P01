using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{

    [SerializeField] private int maxHealth = 5;
    [SerializeField] AudioClip _destroySound;
    [SerializeField] ParticleSystem _destroyParticles;
    private int currentHealth;

    public event Action EntityDamage;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        EntityDamage?.Invoke();
    }

    private void Kill()
    {
        Feedback();
        Destroy(this.gameObject);
    }

    private void Feedback()
    {
        //particles
        if (_destroyParticles != null)
        {
            _destroyParticles = Instantiate(_destroyParticles, transform.position, Quaternion.identity);
        }
        //audio -- consider object pooling for performance
        if (_destroySound != null)
        {
            AudioHelper.PlayClip2D(_destroySound, 1f);
        }
    }

    public float HealthPercent()
    {
        return ((float)currentHealth / (float)maxHealth);
    }
}
