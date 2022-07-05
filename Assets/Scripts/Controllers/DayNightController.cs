using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class DayNightController : MonoBehaviour
{
    [Range(0, 24)] public float timeofDay;
    public Text timeText;
    public Text dateText;
    private string _textShow;
    
    public float orbitSpeed = 1.0f;
    public Light sun;
    public Light moon;
    public Volume skyVolume;
    public AnimationCurve starsCurve;

    [Header("CubeMaps")] 
    public CubemapParameter dayMap;
    public CubemapParameter nightMap;

    public int currentDay;
    
    private PhysicallyBasedSky _sky;
    private bool _isNight;
    
    private void Start()
    {
        skyVolume.profile.TryGet(out _sky);
    }

    void Update()
    {
        timeofDay += Time.deltaTime * orbitSpeed;
        if (timeofDay > 24)
        {
            timeofDay = 0;
            currentDay++;
        }
        
        dateText.text = currentDay.ToString();
        timeText.text = timeofDay.ToString("F2");
        UpdateTime();
    }

    private void OnValidate()
    {
        skyVolume.profile.TryGet(out _sky);
        UpdateTime();
    }

    private void UpdateTime()
    {
        float alpha = timeofDay / 24.0f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180;
        sun.transform.rotation = Quaternion.Euler(sunRotation,-150.0f,0.0f);
        moon.transform.rotation = Quaternion.Euler(moonRotation,-150.0f,0.0f);

        _sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(alpha) * 1000.0f;
        
        CheckNightDayTransition();
    }

    private void CheckNightDayTransition()
    {
        if (_isNight)
        {
            if (moon.transform.rotation.eulerAngles.x > 180)
            {
                StartDay();    
            }
        } 
        else
        {
            if (sun.transform.rotation.eulerAngles.x > 180)
            {
                StartNight();
            }
        }
    }

    private void StartDay()
    {
        _isNight = false;
        sun.shadows = LightShadows.Soft;
        moon.shadows = LightShadows.None;
        _sky.spaceEmissionTexture = dayMap;
    }

    private void StartNight()
    {
        _isNight = true;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.Soft;
        _sky.spaceEmissionTexture = nightMap;
    }
}
