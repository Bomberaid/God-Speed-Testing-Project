using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [HideInInspector]
    public readonly float maxStamina = 100;
    private float stamina;
    public float Stamina { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Stamina = maxStamina;
    }

    public void DepleteStamina(CharacterStats stat, float amount)
    {
        if(stat != null)
        {
            Stamina -= amount;
        }
    }

    public void RecoverStamina(CharacterStats stat, float amount)
    {
        if(stat != null)
        {
            Stamina += amount;
        }
    }

    private void Update()
    {
        Stamina = Mathf.Clamp(Stamina, 0, maxStamina);
    }
}
