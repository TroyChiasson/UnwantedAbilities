using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{  
    public Animator animator;
    //x movement speed
    public static float movementSpeed = 5f;

    public int walking;

    // health 
    public int playerHealth;

    // respawn scene
    public int Respawn = 2; 
    //y movement speed
    public static float jumpSpeed = 10f;

    //longest amount of time the jump key is allowed to be held
    public static float jump_tot_time = 0.5f;

    //how long the jump key has been held for
    public static float jump_cur_time = 0f;

    public static double health;
    //keyboard presses
    private bool upIsHeld = false;
    private bool downIsHeld = false;
    private bool leftIsHeld = false;
    private bool rightIsHeld = false;
    public static bool jumpIsHeld = false;


    public bool fireImmunity = true;
    public bool waterBreathing = true;
    public bool doubleJump = true;

    private int jumpsAvailable;
    public static int maxJumps = 2;
    public Rigidbody2D rb;
    public static double stamina;
    public LayerMask groundLayer;
    private float raycastDistance = .5f;
    public AudioSource Jump;
    public AudioSource Die;
    public AudioSource Walk;
    public AudioSource Damage;
    public AudioSource Spawn;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 30;
        stamina = 100;
        health = 100;
        rb = GetComponent<Rigidbody2D>();
        ResetJumps();
    }

    public void ResetJumps() {
        jumpsAvailable = maxJumps;
    }

    public void noFireImmunity()
    {
        fireImmunity = false;     
    }

    public void noWaterBreathing()
    {
        waterBreathing = false;
    }
    public void noDoubleJump()
    {
        maxJumps = 1;
    }

    public void ResetYVelocity() {
        var curVel = rb.velocity;
        curVel.y = 0;
        rb.velocity = curVel;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Ground") && Physics2D.Raycast(transform.position, Vector2.right, raycastDistance, groundLayer) == false && Physics2D.Raycast(transform.position, Vector2.up, raycastDistance, groundLayer)==false && Physics2D.Raycast(transform.position, Vector2.left, raycastDistance, groundLayer)==false)
        {
            jumpsAvailable = maxJumps;
        }
    }
            // Update is called once per frame
            void Update() {
          

            //rb.AddForce(new Vector2(0, -1f) * rb.mass * 1.2f);
            //rb.AddForce(new Vector2(0, -1f) * rb.mass * 1.2f);

            // A key
            if (Game.Instance.input.Default.MoveLeft.WasPressedThisFrame()) { leftIsHeld = true; 
            
                animator.SetTrigger("Run");}
            if (Game.Instance.input.Default.MoveLeft.WasReleasedThisFrame()) { leftIsHeld = false; 
                animator.SetTrigger("Idle");}

            // move left
            if (leftIsHeld)
            {
                var curVector = transform.localPosition;
                curVector.x = curVector.x - movementSpeed * Time.deltaTime;
                transform.localPosition = curVector;
                transform.localScale = new Vector3(2,2,1);
                
            }

            // D key
            if (Game.Instance.input.Default.MoveRight.WasPressedThisFrame()) {
                 rightIsHeld = true;
                animator.SetTrigger("Run");
            }
            if (Game.Instance.input.Default.MoveRight.WasReleasedThisFrame()) { rightIsHeld = false;
            
                animator.SetTrigger("Idle");}

            // move right
            if (rightIsHeld)
            {
                var curVector = transform.localPosition;
                curVector.x = curVector.x + movementSpeed * Time.deltaTime;
                transform.localPosition = curVector;
                transform.localScale = new Vector3(-2,2,1);
                
            }

            // space key
            if (Game.Instance.input.Default.Jump.WasPressedThisFrame() && jumpsAvailable > 0)
            {
                jumpIsHeld = true;
                jumpsAvailable--;

                ResetYVelocity();
                rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);

                var vel = rb.velocity;
                Jump.Play();

                animator.SetTrigger("Jump");
            }
            if (Game.Instance.input.Default.Jump.WasReleasedThisFrame() && jumpIsHeld) {
                jumpIsHeld = false;

                ResetYVelocity();
                rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);

                animator.SetTrigger("Idle");
            }

            // move up
            // ***NOTE rigid body is affected by gravity automatically in unity
            //         gravity is exponential, this movement up is linear
            //         if there is choppy jump movement this is why
            if (jumpIsHeld)
            {
                //var curVector = transform.localPosition;
                //curVector.y = curVector.y + jumpSpeed * Time.deltaTime;
                //transform.localPosition = curVector;

                //you cant hold jump forever
                var vel = rb.velocity;
                if (vel.y <= 0)
                {
                    jumpIsHeld = false;
                    ResetYVelocity();
                    rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
                    animator.SetTrigger("Idle");
                }
            }

            if (playerHealth <= 0)
            {
           
            RespawnPlayer();
            }

       
    }

    public void RespawnPlayer()
    {
        Die.Play();
        SceneManager.LoadScene(Respawn);
        playerHealth = 30;
    }

    public void RelocatePlayer()
    {
        Spawn.Play();
        transform.localPosition = new Vector3(3.50822997f, 0.633381486f, 0.0486094989f);
        playerHealth = 30;
    }
    public void TakeDamage(int trapDamage) {
        playerHealth -= trapDamage;
    }
}
