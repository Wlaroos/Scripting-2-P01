using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    [SerializeField] int _maxHealth = 3;

    [SerializeField] TextMeshProUGUI _tmp;

    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] AudioClip _deathSound;

    int _treasureCount;
    int _currentHealth;

    bool invin = false;

    TankController _tankController;

    GameObject[] tankObjects;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    void Start()
    {
        _currentHealth = _maxHealth;

        GameObject[] tankArt = new GameObject[transform.GetChild(0).childCount];

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            tankArt[i] = transform.GetChild(0).GetChild(i).gameObject;
        }

        tankObjects = tankArt;
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

    public void PowerUp(Color color1, Color color2)
    {

        invin = true;

        foreach (GameObject j in tankObjects)
        {
            if (j.name.Contains("Tread"))
            {
                j.GetComponent<MeshRenderer>().material.color = color1;
            }
            else
            {
                j.GetComponent<MeshRenderer>().material.color = color2;
            }
        }
    }

    public void PowerDown(Color color1, Color color2)
    {

        invin = false;

        foreach (GameObject j in tankObjects)
        {
            if (j.name.Contains("Tread"))
            {
                j.GetComponent<MeshRenderer>().material.color = color1;
            }
            else
            {
                j.GetComponent<MeshRenderer>().material.color = color2;
            }
        }
    }

    public void Kill()
    {
        if (invin == false)
        {
            gameObject.SetActive(false);

            //particles
            if (_deathParticles != null)
            {
                _deathParticles = Instantiate(_deathParticles, transform.position, Quaternion.identity);
            }
            //audio -- consider object pooling for performance
            if (_deathSound != null)
            {
                AudioHelper.PlayClip2D(_deathSound, 1f);
            }
        }
    }

}
