using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    GameObject player;
    PlayerMovement movement;

    public float boostAmount = 40f;

    //public Vector3 offset;
    //public Vector3 size;
    //public Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player.gameObject;
        movement = player.GetComponent<PlayerMovement>();
    }

    public void BoostSpeed(float amount, Rigidbody rb)
    {    
        if (rb.CompareTag("Player"))
        {
            movement.maxSpeed = 100f;
        }

        rb.AddForce(transform.forward * amount * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        int layermask = 1 << 8;

        if (other.gameObject.layer != layermask && other.attachedRigidbody != null)
        {
            BoostSpeed(boostAmount, other.attachedRigidbody);
        }
    }
}
