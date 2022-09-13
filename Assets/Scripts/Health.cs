using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{

    [SerializeField] private int maxHealth = 5;
    private int currentHealth;

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
    }

    private void Kill()
    {
        Destroy(this.gameObject);
    }

}
