using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;                             
    private Animator animator;
    private CapsuleCollider cCollider;
    private CharacterStats playerStats;                                               // Mainly used for the player's stamina value.
    public GameObject auraSphere;                                                     // Effect when charging for teleportation.
    AudioManager audioManager;

    ParticleSystem followParticles;                                                   // Particles used when running.
    public VisualEffect chargeBurst;                                                  // Particles used when charging for teleportation.

    Transform cameraRotation;

    float rotationVelocity;                                                           // Used for smoothing player rotation.
    readonly float smoothSpeed = 0.1f;                                                // Amount of smoothing.

    float fallPoint = -20f;                                                           // Point where level restarts if player fall off (Has to be a negative number).

    public float speedMultiplier;
    public float godSpeedMultiplier;

    float x;                                                                          // Axis used for movement.
    float y;                                                                          //
    Vector2 input;                                                                    // Holds axis values.
    Vector2 inputDirection;                                                           // Normalized version.

    public Vector3 sphereOffset;                                                      // Bounds for ground and platform detection.
    public float radius;                                                              //
    [HideInInspector]
    public bool grounded;

    private readonly float normalMaxSpeed = 40;                                       // Player's normal max speed.
    public float maxSpeed;                                                            // Max speed can change through boosts.

    private bool reachedMaxSpeed;
    
    private float switchSpeed;                                                        // Speed that forcibly changes form if the player is too slow...
    private float switchSpeedGround;                                                  // ...when on ground.
    private readonly float switchSpeedAir = 0;                                        // ...when in air.

    [Header("Teleport Settings")]

    [HideInInspector]
    public readonly float minDistance = 2.5f, maxDistance = 80f;                      // Min and Max distance for teleportation.
    private float currentDistance;                                                    // The distance the player can teleport through charging.
    public float CurrentDistance { get; set; }

    public float chargeRate = 40;
    Material chargeIndicator;                                                         // Used in charging UI.
    private float depleteAmount = 15f;                                                // The rate of stamina depleting when charging.
    private bool tired;                                                               // Used to limit the amount of times the player can teleport.


    [ColorUsage(true, true)]                                                          // Makes it so that the colours are HDR.
    public Color electricBlue, darkBlue, darkerBlue;

    ChargeLevel levelZero = new ChargeLevel(0, 0);

    [HideInInspector]
    public ChargeLevel levelOne = new ChargeLevel(1, 0.1f);

    [HideInInspector]
    public ChargeLevel levelTwo = new ChargeLevel(2, 0.5f);

    [HideInInspector]
    public ChargeLevel levelThree = new ChargeLevel(3, 1);

    ChargeLevel currentLevel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cCollider = GetComponent<CapsuleCollider>();
        playerStats = GetComponent<CharacterStats>();
        followParticles = GetComponentInChildren<ParticleSystem>();
        chargeIndicator = auraSphere.GetComponent<MeshRenderer>().material;
    }
    void Start()
    {
        audioManager = AudioManager.instance;
        cameraRotation = Camera.main.transform;

        maxSpeed = normalMaxSpeed;

        levelOne.AuraColor = electricBlue;
        levelTwo.AuraColor = darkBlue;
        levelThree.AuraColor = darkerBlue;

        auraSphere.SetActive(false);
    }

    void Run()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y);
        inputDirection = input.normalized;

        switchSpeedGround = normalMaxSpeed * 0.5f;

        if (inputDirection != Vector2.zero)
        {
            playerStats.RecoverStamina(playerStats, 10 * Time.deltaTime);

            // Smoothly rotates player based on the camera's position.
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraRotation.eulerAngles.y;
            transform.eulerAngles = Vector3.up * (Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, smoothSpeed));

            // Normal Movement.
            if (animator.GetBool("GodSpeed") == false && grounded)
            {
                transform.Translate(transform.forward * inputDirection.magnitude * speedMultiplier * Time.deltaTime, Space.World);
            }
            // GodSpeed Movment.
            else if (animator.GetBool("GodSpeed") == true && grounded)
            {
                Vector3 movement = transform.forward * godSpeedMultiplier * Time.fixedDeltaTime;
                rb.velocity = movement;
            }

            animator.SetFloat("MovementSpeed", 1f);
        }
        else
        {
            // Cancels GodSpeed movment if the player is grounded any not moving.
            if (grounded) { rb.AddForce(-new Vector3(rb.velocity.x, 0, rb.velocity.z) * Time.fixedDeltaTime * 75, ForceMode.Acceleration); }

            animator.SetFloat("MovementSpeed", 0);
        }
    }

    void Teleport()
    {
        // Makes sure that the player doesn't go past the maxDistance and minDistance threshold.
        CurrentDistance = Mathf.Clamp(CurrentDistance, minDistance, maxDistance);

        // Effect changes 
        // based on how long the player charges for.
        if(Input.GetKey(KeyCode.Space) && animator.GetBool("GodSpeed") == true)
        {
            CurrentDistance += Time.deltaTime * chargeRate;

            // Activates an effect if player charges for long enough.
            // Chargelevel indicates the level of charge.
            if (CurrentDistance >= maxDistance * levelThree.Threshold && currentLevel.Level == 2)
            {
                currentLevel = levelThree;

                audioManager.Play("ChargePop");
                chargeIndicator.SetColor("AuraColour", currentLevel.AuraColor);

                // Tells the visual effect graph to play particles
                chargeBurst.SendEvent("PlayBurst", null);
            }
            else if (CurrentDistance > maxDistance * levelTwo.Threshold && currentLevel.Level == 1)
            {
                currentLevel = levelTwo;

                audioManager.Play("ChargePop");
                chargeIndicator.SetColor("AuraColour", currentLevel.AuraColor);
                chargeBurst.SendEvent("PlayBurst", null);
            }
            else if (CurrentDistance >= maxDistance * levelOne.Threshold && currentLevel.Level == 0)
            {
                currentLevel = levelOne;

                audioManager.Play("ChargePop");
                chargeIndicator.SetColor("AuraColour", currentLevel.AuraColor);
                chargeBurst.SendEvent("PlayBurst", null);
                auraSphere.SetActive(true);
            }
        }
        else
        {
            // Cancels everything.
            currentLevel = levelZero;
            auraSphere.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(animator.GetBool("GodSpeed") == true && playerStats.Stamina > 0 && !tired)
            {
                transform.Translate(transform.forward * CurrentDistance, Space.World);

                followParticles.Play();

                playerStats.DepleteStamina(playerStats, 20);
            }

            // Resets charge
            CurrentDistance = minDistance;
        }
    }

    // Just a method to play footsteps.
    public void PlayFootSteps()
    {
        int random = Random.Range(1, 5);

        if(grounded)
        {
            switch (random)
            {
                case 1:
                    audioManager.Play("Running1");
                    break;
                case 2:
                    audioManager.Play("Running2");
                    break;
                case 3:
                    audioManager.Play("Running3");
                    break;
                case 4:
                    audioManager.Play("Running4");
                    break;
            }
        }
    }

    // Detects if the player hits the ground or a platform.
    void DetectCollision()
    {
        int layermask = 1 << 8;
        Collider[] collisions = Physics.OverlapSphere(transform.position + sphereOffset, radius, layermask);

        // Used to avoid any errors if the player is touching multiple objects.
        if (collisions.Length > 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    // Shows the bounds of DetectCollision() in the inspector.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (cCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position + sphereOffset, radius);
        }
    }



    void FixedUpdate()
    {
        Run();
        DetectCollision();

        // Limits the player GodSpeed speed.
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        switchSpeed = (grounded) ? switchSpeedGround : switchSpeedAir;

        if (animator.GetBool("GodSpeed") == true)
        {
            // Used to avoid an error...
            if (rb.velocity.magnitude >= (normalMaxSpeed * 0.8f))
            {
                reachedMaxSpeed = true;
            }

            // ...based on this code.
            if (rb.velocity.magnitude < switchSpeed && reachedMaxSpeed)
            {
                animator.SetBool("GodSpeed", false);
            }

            // Player would instantly transform back to normal if they were slow enough.
            // Which would usually mean the player would turn back to normal after they transformed.
        }
        else
        {
            reachedMaxSpeed = false;
        }

        // Not important.
        // Used to reset maxspeed if the player used a booster.
        if(maxSpeed > normalMaxSpeed && grounded)
        {
            maxSpeed = normalMaxSpeed;
        }
    }

    private void Update()
    {
        Teleport();

        // Stops the player from teleporting if they're tired.
        if(playerStats.Stamina <= 0) { tired = true; }

        if (tired)
        {
            // Makes the player not tired if they have enough stamina.
            if (playerStats.Stamina >= depleteAmount) { tired = false; }
        }

        // Resets level if player falls off.
        if(transform.position.y < fallPoint)
        {
            GameManager.instance.Restart();
        }
    }

}

// Used as an easy way to hold values for teleporting
public struct ChargeLevel
{
    int level;
    public int Level { get; }
    float threshold;
    public float Threshold { get; }
    Color auraColor;
    public Color AuraColor { get; set; }


/// <summary>
/// A type used to determine what the player values when charging past a certain threshold
/// </summary>
/// <param name="level"> Indicates where this sits in the ChargeLevel hiarchy.</param>
/// <param name="threshold"> The value that must be met before activating. Has to be a decimal so that it can be converted to a percent (example: 0.5f = 50% threshold)</param>
/// <param name="auraColor"> Colour of the aura when it hits the threshold. </param>
    public ChargeLevel(int level, float threshold, Color auraColor = default)
    {
        this.level = level;
        this.threshold = threshold;
        this.auraColor = auraColor;

        Level = this.level;
        AuraColor = this.auraColor;
        Threshold = this.threshold;
    }
}
