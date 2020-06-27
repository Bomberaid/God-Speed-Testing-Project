using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TransformationBehaviour : StateMachineBehaviour
{
    public VisualEffect transformationBurst;                          // Particles used when transforming.
    public Material electricity;                                      // Electric sparks that flicker when transforming.

    PlayerMovement playerMovement;
    AudioManager audioManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMovement = animator.GetComponent<PlayerMovement>();

        // Makes sure player doesn't move or propell forward during transformation.
        animator.SetBool("GodSpeed", false);
        playerMovement.enabled = false;

        transformationBurst = animator.GetComponent<Transformation>().transformationBurst;
        electricity = animator.GetComponent<Transformation>().electricity;
        audioManager = AudioManager.instance;
        audioManager.Play("ElectricSparks");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Creates a flickering effect for the sparks
        int flicker = Random.Range(0, 4);

        if(flicker == 1)
        {
            electricity.SetFloat("ElectricityAmount", Time.deltaTime * 250);
        }
        else
        {
            electricity.SetFloat("ElectricityAmount", 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("GodSpeed", true);
        animator.ResetTrigger("Transform");
        playerMovement.enabled = true;

        // Disables sparkes after transformation is finished.
        electricity.SetFloat("ElectricityAmount", 0f);
        audioManager.Stop("ElectricSparks");
    }
}
