using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GodSpeedBehaviour : StateMachineBehaviour
{
    //GameObject[] normalHair;
    //GameObject[] transformedHair;
    //GameObject[] gsLights;
    //Material[] gsMaterials;
    ////VisualEffect transformationBurst;

    //Rigidbody rb;
    //Transformation transformationScript;
    //PlayerMovement movement;

    //float maxGSSpeed;
    //public static bool reachedMaxSpeed;

    //ParticleSystem followParticles;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    //transformationScript = animator.GetComponent<Transformation>();
    //    //movement = animator.GetComponent<PlayerMovement>();
    //    //normalHair = transformationScript.normalHair;
    //    //transformedHair = transformationScript.transformedHair;
    //    //gsLights = transformationScript.gsLight;
    //    //gsMaterials = transformationScript.gsMaterial;
    //    //rb = animator.GetComponent<Rigidbody>();
    //    //maxGSSpeed = movement.maxGSSpeed;

    //    //speedToEngageThreshold = stopGSThreshold + 1;
    //    //transformationBurst = animator.GetComponent<Transformation>().transformationBurst;
    //    //followParticles = animator.GetComponent<Transformation>().followParticles;


    //    //foreach (GameObject normalHairObjects in normalHair)
    //    //{
    //    //    normalHairObjects.SetActive(false);
    //    //}

    //    //foreach(GameObject transformedHairObjects in transformedHair)
    //    //{
    //    //    transformedHairObjects.SetActive(true);
    //    //}

    //    //foreach (GameObject lights in gsLights)
    //    //{
    //    //    lights.SetActive(true);
    //    //}

    //    //foreach (Material gsMaterials in gsMaterials)
    //    //{
    //    //    gsMaterials.SetFloat("GSEffect", 1);
    //    //    gsMaterials.SetFloat("ColorIntensity", 0.2f);
    //    //}

    //    //transformationBurst.SendEvent("PlayBurst", null);
    //}

    //private bool SlowPunisher()
    //{
    //    if (rb.velocity.magnitude < maxGSSpeed / 2 && reachedMaxSpeed)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    //SlowPunisher();

    //    //Player has to maintain speed to maintain GS Mode.
    //    //if (rb.velocity.magnitude >= (maxGSSpeed * 0.8f))
    //    //{
    //    //    reachedMaxSpeed = true;
    //    //    //Debug.Log("Reached Max Speed");
    //    //}

    //    //if (rb.velocity.magnitude < (maxGSSpeed * 0.4) && reachedMaxSpeed)
    //    //{
    //    //    animator.SetBool("GodSpeed", false);

    //    //    Debug.Log("GodSpeed deactivated");
    //    //}
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    //foreach (GameObject normalHairObjects in normalHair)
    //    //{
    //    //    normalHairObjects.SetActive(true);
    //    //}

    //    //foreach (GameObject transformedHairObjects in transformedHair)
    //    //{
    //    //    transformedHairObjects.SetActive(false);
    //    //}
    //    reachedMaxSpeed = false;
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
