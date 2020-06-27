using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeStatus : SliderUIScript
{
    PlayerMovement movement;

    Image fillBackground;

    RectTransform sliderTransform;
    float sliderWidth;

    RectTransform indicatorOne;
    RectTransform indicatorTwo;
    RectTransform indicatorThree;

    GradientColorKey[] colors = new GradientColorKey[5];
    GradientAlphaKey[] alphas = new GradientAlphaKey[2];
    // Start is called before the first frame update
    //private void Awake()
    //{

    //}
    void Start()
    {
        movement = GameManager.instance.player.GetComponent<PlayerMovement>();

        fillBackground = GetComponent<Image>();
        sliderTransform = GetComponent<RectTransform>();
        sliderWidth = sliderTransform.rect.width;

        indicatorOne = transform.Find("IndicatorOne").GetComponent<RectTransform>();
        indicatorTwo = transform.Find("IndicatorTwo").GetComponent<RectTransform>();
        indicatorThree = transform.Find("IndicatorThree").GetComponent<RectTransform>();

    }

    public override void SliderProperties(float value, float minValue, float maxValue)
    {
        base.SliderProperties(value, minValue, maxValue);

        Color sliderColor = slider.image.color;

        //Sets both the slider and fill transparent (Didn't intend to, but WORKS!)
        if (value == minValue)
        {
            sliderColor.a = 0;
            slider.image.color = sliderColor;
        }
        else
        {
            sliderColor.a = 1;
            slider.image.color = sliderColor;
        }

        //Sets the indicators positions to when chargeLevel increases
        //The /2 is because the width expands outwards and not left to right. If 100 were the width then (for children) the midline would be 0.
        indicatorOne.anchoredPosition = new Vector3(Mathf.Lerp(-sliderWidth / 2, sliderWidth / 2, movement.levelOne.Threshold), indicatorOne.anchoredPosition.y);
        indicatorTwo.anchoredPosition = new Vector3(Mathf.Lerp(-sliderWidth / 2, sliderWidth / 2, movement.levelTwo.Threshold), indicatorTwo.anchoredPosition.y);
        indicatorThree.anchoredPosition = new Vector3(Mathf.Lerp(-sliderWidth / 2, sliderWidth / 2, movement.levelThree.Threshold), indicatorThree.anchoredPosition.y);
    }

    void SetGradient()
    {
        alphas[0].alpha = 1;
        alphas[0].time = 0;
        alphas[1].alpha = 1;
        alphas[1].time = 1;

        colors[0].color = Color.white;
        colors[0].time = 0f;

        colors[1].color = Color.white;
        colors[1].time = movement.levelOne.Threshold; // Mathf.Lerp(0f, 100f, movement.levelOne.Threshold);

        colors[2].color = movement.levelOne.AuraColor;
        colors[2].time = movement.levelTwo.Threshold; // Mathf.Lerp(0f, 100f, movement.levelTwo.Threshold);

        colors[3].color = movement.levelTwo.AuraColor;
        colors[3].time = movement.levelThree.Threshold - 0.01f; // Mathf.Lerp(0f, 100f, movement.levelThree.Threshold) - 1;

        colors[4].color = movement.levelThree.AuraColor;
        colors[4].time = movement.levelThree.Threshold; // Mathf.Lerp(0, 100f, movement.levelThree.Threshold);

        gradient.SetKeys(colors, alphas);
    }
    
    private void Update()
    {
        SliderProperties(movement.CurrentDistance, movement.minDistance, movement.maxDistance);
        SetGradient();
        //Debug.Log(slider.image.color);
    }

}
