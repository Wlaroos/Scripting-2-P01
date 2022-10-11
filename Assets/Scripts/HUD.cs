using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Slider _bossSlider;
    [SerializeField] Slider _playerSlider;
    [SerializeField] Slider _gunSlider;
    [SerializeField] Image _bloodImg;
    [SerializeField] Player _playerRef;
    [SerializeField] Health _bossRef;
    [SerializeField] Shoot _shootRef;
    [SerializeField] FloorController _floorRef;


    private void Awake()
    {
        _bossSlider.value = 1;
        _playerSlider.value = 1;
        _gunSlider.value = 0;
    }

    private void OnEnable()
    {
        _playerRef.PlayerDamage += OnPlayerDamage;
        _bossRef.EntityDamage += OnBossDamage;
        _shootRef.HeatChange += OnHeatChange;
        _shootRef.CoolingChange += OnCoolingChange;
    }

    private void OnDisable()
    {
        _playerRef.PlayerDamage -= OnPlayerDamage;
        _bossRef.EntityDamage -= OnBossDamage;
        _shootRef.HeatChange -= OnHeatChange;
        _shootRef.CoolingChange -= OnCoolingChange;
    }

    void OnPlayerDamage()
    {
        _playerSlider.value = _playerRef.HealthPercent();
        _bloodImg.color = new Color32(255, 0, 0, 150);
        StartCoroutine(LerpColor(new Color32(0,0,0,0),0.5f));
        ScreenShake.ShakeOnce(0.5f);
    }

    void OnBossDamage()
    {
        _bossSlider.value = _bossRef.HealthPercent();

        if (_bossSlider.value % 0.2f <= 0.00001f && _bossSlider.value > 0)
        {
            FallMethod();
        }
    }

    void OnHeatChange()
    {
        _gunSlider.value = _shootRef.HeatPercent();
    }

    void OnCoolingChange(bool cooling)
    {
        if (cooling == true)
        {
            _gunSlider.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = new Color32(255, 60, 0, 255);
        }
        else
        {
            _gunSlider.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = new Color32(255, 221, 0, 255);
        }
    }

    IEnumerator LerpColor(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = _bloodImg.color;
        while (time < duration)
        {
            _bloodImg.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        _bloodImg.color = endValue;
    }

    void FallMethod()
    {
        switch (Random.Range(1, 11))
        {
            case 1: { _floorRef.StartCoroutine(_floorRef.FallOrder(5, 1, 2, 3)); }  break;
            case 2: { _floorRef.StartCoroutine(_floorRef.FallOrder(2, 4, 5, 6)); }  break;
            case 3: { _floorRef.StartCoroutine(_floorRef.FallOrder(5, 7, 8, 9)); }  break;
            case 4: { _floorRef.StartCoroutine(_floorRef.FallOrder(5, 1, 4, 7)); }  break;
            case 5: { _floorRef.StartCoroutine(_floorRef.FallOrder(4, 2, 5, 8)); }  break;
            case 6: { _floorRef.StartCoroutine(_floorRef.FallOrder(5, 3, 6, 9)); }  break;
            case 7: { _floorRef.StartCoroutine(_floorRef.FallOrder(2, 1, 5, 9)); }  break;
            case 8: { _floorRef.StartCoroutine(_floorRef.FallOrder(8, 3, 5, 7)); }  break;
            case 9: { _floorRef.StartCoroutine(_floorRef.FallOrder(5, 2, 3, 6)); }  break;
            case 10: { _floorRef.StartCoroutine(_floorRef.FallOrder(5, 4, 7, 8)); }  break;

        }
    }
}
