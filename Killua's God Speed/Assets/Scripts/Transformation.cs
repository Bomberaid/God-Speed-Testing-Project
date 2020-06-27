using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Cinemachine;

public class Transformation : MonoBehaviour
{
    Animator animator;
    PlayerMovement movement;

    AudioManager audioManager;

    public GameObject[] normalHair = new GameObject[3];                                   // Used to change hair when transforming.
    public GameObject[] transformedHair = new GameObject[2];                              //

    // Used to change material values for transformation.
    public Material gsBody;
    public Material gsClothes;
    public Material gsEyes;
    public Material gsFace;
    public Material gsHair;
    public Material gsPants;

    [HideInInspector]
    public Material[] gsMaterial = new Material[6];                                       // Used as an easy way to change all material values at once.

    public GameObject[] gsLight = new GameObject[3];

    public ParticleSystem followParticles;                                                // Particles used for running.
    public VisualEffect transformationBurst;                                              // Particles used for transformation.

    [HideInInspector]
    public Material electricity;                                                          // Electrity sparks that are shown when the player is in the process of transforming.
    [HideInInspector]
    public GameObject electricityPivot;                                                   // Used to make the sparks face upright and face the camera.

    CinemachineImpulseSource cameraShake;                                                 // Tells cinemachine if the camera needs to shake.

    [ColorUsage(true, true)]                                                              // Makes it so that the colours are HDR.
    public Color setColor;                                                                // Used as a universal colour for GodSpeed

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        cameraShake = GetComponent<CinemachineImpulseSource>();
        
        normalHair = GameObject.FindGameObjectsWithTag("normalHair");
        transformedHair = GameObject.FindGameObjectsWithTag("transformedHair");
        gsLight = GameObject.FindGameObjectsWithTag("gsLights");

        electricityPivot = transform.Find("Electricity Pivot").gameObject;
    }

    void Start()
    {
        audioManager = AudioManager.instance;

        electricity = electricityPivot.GetComponentInChildren<Renderer>().material;
        followParticles.Stop();
        electricity.SetFloat("ElectricityAmount", 0);

        gsMaterial[0] = gsBody;
        gsMaterial[1] = gsClothes;
        gsMaterial[2] = gsEyes;
        gsMaterial[3] = gsFace;
        gsMaterial[4] = gsHair;
        gsMaterial[5] = gsPants;

        // Makes sure that every material is set to normal at start.
        foreach(Material gsMaterials in gsMaterial)
        {
            gsMaterials.SetFloat("GSEffect", 0);
            gsMaterials.SetColor("GSColor", setColor);
        }

        foreach (GameObject transformedHairObjects in transformedHair)
        {
            transformedHairObjects.SetActive(false);
        }

        foreach (GameObject lights in gsLight)
        {
            lights.SetActive(false);
        }
    }

    void Godspeed()
    {
        // Triggers TransformationBehaviour.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (animator.GetBool("GodSpeed") == false)
            {
                // Transforms to GodSpeed state.
                animator.SetTrigger("Transform");
            }
            else
            {
                // Transforms back to normal state.
                animator.SetBool("GodSpeed", false);
            }
        }

        // Makes sure running particles don't play if player isn't moving.
        if (animator.GetFloat("MovementSpeed") <= 0)
        {
            followParticles.Stop();
        }
    }

    // A method that is triggered from an animation event.
    // Animation: Transformation.
    public void GSChange()
    {
        // Changes all materials and effects to GodSpeed.
        foreach (GameObject normalHairObjects in normalHair)
        {
            normalHairObjects.SetActive(false);
        }

        foreach (GameObject transformedHairObjects in transformedHair)
        {
            transformedHairObjects.SetActive(true);
        }

        foreach (GameObject lights in gsLight)
        {
            lights.SetActive(true);
        }

        foreach (Material gsMaterials in gsMaterial)
        {
            gsMaterials.SetFloat("GSEffect", 1);
            gsMaterials.SetFloat("ColorIntensity", 0.2f);
        }

        // Triggers...
        // ...transformation particles.
        transformationBurst.SendEvent("Transform", null);
        // ...sound effect.
        audioManager.Play("TransformBoom");
        // ...camera shake.
        cameraShake.GenerateImpulse();
    }

    // Particles that play whenever the player takes a step.
    // When they are the GodSpeed form.
    public void PlayParticles()
    {
        // Prevents particles from playing every footstep.
        float random = Random.Range(0, 11);

        if (random > 5)
        {
            followParticles.Play();
        }

        // Prevents monotony in hearing the same footsteps.
        int randomStep = Random.Range(1, 5);

        if (movement.grounded)
        {
            switch (randomStep)
            {
                case 1:
                    audioManager.Play("GSSteps1");
                    break;
                case 2:
                    audioManager.Play("GSSteps2");
                    break;
                case 3:
                    audioManager.Play("GSSteps3");
                    break;
                case 4:
                    audioManager.Play("GSSteps4");
                    break;
            }
        }
    }

    public void StopParticles()
    {
        followParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Godspeed();
    }
}
