using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : SliderUIScript
{
    // Start is called before the first frame update
    private void Update()
    {
        SliderProperties(playerStats.Stamina, 0, playerStats.maxStamina);
    }
}
