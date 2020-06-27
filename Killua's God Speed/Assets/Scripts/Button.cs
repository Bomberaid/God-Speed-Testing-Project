using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Button : EventTrigger
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        //Plays a soundeffect whenever the player hovers over the button.
        //I used this because the event trigger component keeps resetting upon reload...
        //...and I can change the sound effect for all buttons at once.
        audioManager.Play("ButtonHighlight");
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        //Plays a sound effect when the button is clicked
        //Used for convienince purposes (i.e. don't have to change every button's sound effect)
        audioManager.Play("ButtonClick");
    }

    public virtual void Event()
    {
        //Put whatever you want to happen
    }
}
