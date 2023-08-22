using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timelight : MonoBehaviour
{
    public SpriteRenderer Img_Outside_Back;
    public GameObject LightShadow;
    public GameObject Light_Middle_Baking;
    public List<GameObject> Light_Pseudo_Volumetri_lighting;

    private void Set_Light_Pseudo_Volumetri_lighting_State (bool _enable)
    {
        //我给你写上了，明天好好干！
        foreach (var item in Light_Pseudo_Volumetri_lighting)
        {
            item.SetActive(_enable);
        }
        return;
    }

    private void Start ()
    {
        UpdateTime();
        StartCoroutine(HandleTime());
    }

    private IEnumerator HandleTime ()
    {
        float wait_second = 3;
        while (true)
        {
            yield return new WaitForSeconds(wait_second);
            if (!Temp_skybox.Temp_skybox_state)
            {
                UpdateTime();
                wait_second = 3;
            }
            else
            {
                wait_second = 0.5f;
            }
        }
    }

    public void FixedUpdate ()
    {
    }

    private void UpdateTime ()
    {
        DateTime now = DateTime.Now.AddHours(UsualValue.TimeDifference);
        int nowHour = now.Hour;
        Debug.Log($"当前时间{nowHour}");
        if (nowHour >= 8 && nowHour <= 11)//上午
        {
            Img_Outside_Back.sprite = Resources.Load<Sprite>("Texture2D/Outside/Outside_Morning");
            Set_Light_Pseudo_Volumetri_lighting_State(true);
            Light_Middle_Baking.SetActive(true);
            SetShadow(true, 5f);
            Debug.Log("Usually");
            PlaySkybox("Usually");
            return;
        }
        else if (nowHour >= 12 && nowHour < 13)//正中午
        {
            Img_Outside_Back.sprite = Resources.Load<Sprite>("Texture2D/Outside/Outside_Midday");
            Set_Light_Pseudo_Volumetri_lighting_State(true);
            Light_Middle_Baking.SetActive(true);
            SetShadow(true, 5f);
            Debug.Log("Noon");
            PlaySkybox("Noon");
            return;
        }
        else if (nowHour >= 13 && nowHour < 19)//下午
        {
            Img_Outside_Back.sprite = Resources.Load<Sprite>("Texture2D/Outside/Outside_Midday");
            Set_Light_Pseudo_Volumetri_lighting_State(true);
            Light_Middle_Baking.SetActive(true);
            SetShadow(true, 5f);
            Debug.Log("Usually");
            PlaySkybox("Usually");
            return;
        }
        else if (nowHour >= 23 || (nowHour >= 0 && nowHour < 6))//晚上
        {
            Img_Outside_Back.sprite = Resources.Load<Sprite>("Texture2D/Outside/Outside_Midnight");
            Set_Light_Pseudo_Volumetri_lighting_State(true);
            Light_Middle_Baking.SetActive(false);
            SetShadow(true, 1f);
            Debug.Log("Night");
            PlaySkybox("Night");
            return;
        }
        else
        {
            Img_Outside_Back.sprite = Resources.Load<Sprite>("Texture2D/Outside/Outside_night");
            Light_Middle_Baking.SetActive(false);
            SetShadow(true, 1f);
            Debug.Log("Night_To_Morning");
            PlaySkybox("Usually");//快要上午的时候，6点到8点
        }
    }

    private void PlaySkybox (string material)
    {
        RenderSettings.skybox = Resources.Load<Material>($"Skybox/{material}");
        DynamicGI.UpdateEnvironment();
    }

    private void SetShadow (bool isLight, float _lightLevel)
    {
        if (isLight)
        {
            LightShadow.SetActive(true);
            LightShadow.GetComponent<Light>().intensity = _lightLevel;
        }
        else
        {
            LightShadow.SetActive(false);
        }
    }
}