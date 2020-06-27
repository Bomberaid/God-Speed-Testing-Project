using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class RunnerMovement : AIMovement
{
    //readonly int maxDodges = 1;
    //int dodges;
    //Animator animator;

    public float captureRadius = 5f;

    public float chasedSpeed = 75f;
    public float finalSpeed = 65;

    public override void Running()
    {
        base.Running();
        Vector3 rotation = transform.InverseTransformDirection(agent.velocity).normalized;
        float turn = rotation.x;

        animator.SetFloat("Forward", 1f);
        animator.SetFloat("Turn", turn);

        if (distance <= runningRadius)
        {
            //animator.SetFloat("Forward", 1f);
            agent.speed = chasedSpeed;
            speedUpBurst.SendEvent("Burst", null);
        }

        if (distance < captureRadius)
        {
            ObjectiveFail();
        }

        //if (distance < dodgeRadius && dodges < maxDodges)
        //{
        //    rb.AddForce(transform.forward * 100, ForceMode.Impulse);
        //    //transform.Translate(transform.forward * 1000 * Time.deltaTime, Space.World);
        //    dodges++;

        //    agent.speed = finalSpeed;
        //}
        //else if (distance < dodgeRadius && dodges == maxDodges)
        //{
        //    ObjectiveFail();
        //}
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, captureRadius);
    }
}
