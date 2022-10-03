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
    }

    void OnHeatChange()
    {
        _gunSlider.value = _shootRef.HeatPercent();
    }

    void OnCoolingChange(bool cooling)
    {
        if (cooling == true)
        {
            _gunSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 60, 0, 255);
        }
        else
        {
            _gunSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 221, 0, 255);
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
}
