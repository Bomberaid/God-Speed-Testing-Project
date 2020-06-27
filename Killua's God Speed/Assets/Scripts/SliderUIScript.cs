using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Meant to be used on Sliders
public class SliderUIScript : MonoBehaviour
{
    [HideInInspector]
    public CharacterStats playerStats;

    [HideInInspector]
    public Slider slider;
    [HideInInspector]
    public Image fill;

    public Gradient gradient;

    //[HideInInspector]
    //public float value, maxValue;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        fill = transform.Find("Fill").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameManager.instance.player.GetComponent<CharacterStats>();

        if(slider == null)
        {
            Debug.LogError(gameObject.name + " is not a UI slider");
        }

        fill.color = gradient.Evaluate(1f);
    }

    public virtual void SliderProperties(float value, float minValue, float maxValue)
    {
        slider.value = Mathf.InverseLerp(minValue, maxValue, value);
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
