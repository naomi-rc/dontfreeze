using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxModifierHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject lightSource;

    static private float[] defaultSkyboxValues = { 0.04f, 1f, 2f, 0f };
    static private float[] night = { 0f, 0.35f, 0.2f, 1f };
    static private float[] morning = { 0.04f, 3.5f, 1.30f, 0f };
    static private float[] evening = { 0.02f, 5f, 0.35f, 0f };
    static private float[] noon = { 0.04f, 2f, 2.5f, 0f };
    static private float[][] presets = {
       defaultSkyboxValues,
       morning,
       noon,
       evening,
       night       
    };

    void Start()
    {
        LevelManager levelManager = GetComponent<LevelManager>();
        Level.TimeSetting timeSetting = levelManager.level.timeSetting;
        if(timeSetting == Level.TimeSetting.TIMEBASED)
        {
            timeSetting = ChooseSkyboxBasedOnTime();
        }
        float[] skyboxValues = presets[(int)timeSetting];

        RenderSettings.skybox.SetFloat("_SunSize", skyboxValues[0]);
        RenderSettings.skybox.SetFloat("_AtmosphereThickness", skyboxValues[1]);
        RenderSettings.skybox.SetFloat("_Exposure", skyboxValues[2]);
        if (skyboxValues[3].Equals(1f) && lightSource != null)
        {
            lightSource.SetActive(false);
        }
    }

    void OnDestroy()
    {
        RenderSettings.skybox.SetFloat("_SunSize", defaultSkyboxValues[0]);
        RenderSettings.skybox.SetFloat("_AtmosphereThickness", defaultSkyboxValues[1]);
        RenderSettings.skybox.SetFloat("_Exposure", defaultSkyboxValues[2]);
        if (lightSource != null)
        {
            lightSource.SetActive(true);
        }
    }

    private Level.TimeSetting ChooseSkyboxBasedOnTime()
    {
        int hour = DateTime.Now.Hour;
        
        if(6 <= hour && hour < 12)
        {
            return Level.TimeSetting.MORNING;
        }
        else if(12 <= hour && hour < 16)
        {
            return Level.TimeSetting.NOON;
        }
        else if(16 <= hour && hour < 20)
        {
            return Level.TimeSetting.EVENING;
        }
        else if(19 <= hour || hour < 6)
        {
            return Level.TimeSetting.NIGHT;
        }
        return Level.TimeSetting.DEFAULT;
    }
}
