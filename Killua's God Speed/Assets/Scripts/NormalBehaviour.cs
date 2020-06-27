using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBehaviour : StateMachineBehaviour
{
    // References to materials used for transformation.
    GameObject[] normalHair;
    GameObject[] transformedHair;
    GameObject[] gsLights;
    Material[] gsMaterials;

    ParticleSystem followParticles;
    Rigidbody rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        normalHair = animator.GetComponent<Transformation>().normalHair;
        transformedHair = animator.GetComponent<Transformation>().transformedHair;
        gsLights = animator.GetComponent<Transformation>().gsLight;
        gsMaterials = animator.GetComponent<Transformation>().gsMaterial;
        followParticles = animator.GetComponent<Transformation>().followParticles;
        rb = animator.GetComponent<Rigidbody>();

        // Resets everything back normal.
        // Hair and Materials have different pieces.
        // That's why everything is in foreach.
        foreach (GameObject normalHairObjects in normalHair)
        {
            normalHairObjects.SetActive(true);
        }

        foreach (GameObject transformedHairObjects in transformedHair)
        {
            transformedHairObjects.SetActive(false);
        }

        foreach (GameObject lights in gsLights)
        {
            lights.SetActive(false);
        }

        foreach (Material gsMaterials in gsMaterials)
        {
            gsMaterials.SetFloat("GSEffect", 0);
            gsMaterials.SetFloat("ColorIntensity", 0);
        }

        followParticles.Stop();

        // Stops any GodSpeed movment.
        // Except for any up and down forces, like when the player is in the air.
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(followParticles.isPlaying)
        {
            followParticles.Stop();
        }
    }
}
