using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    //x movement speed
    public static float movementSpeed = 5f;

    // health 
    public int playerHealth;

    // respawn scene
    public int Respawn = 1; 
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
    private bool jumpIsHeld = false;

    private int jumpsAvailable;
    private int maxJumps = 1;
    public Rigidbody2D rb;
    public static double stamina;
    public LayerMask groundLayer;
    private float raycastDistance = 1;

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

    public void ResetYVelocity() {
        var curVel = rb.velocity;
        curVel.y = 0;
        rb.velocity = curVel;
    }

    // Update is called once per frame
    void Update() {          
            if (Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer)) {
                jumpsAvailable = maxJumps;
            }

            //rb.AddForce(new Vector2(0, -1f) * rb.mass * 1.2f);
            //rb.AddForce(new Vector2(0, -1f) * rb.mass * 1.2f);

            // A key
            if (Game.Instance.input.Default.MoveLeft.WasPressedThisFrame()) { leftIsHeld = true; }
            if (Game.Instance.input.Default.MoveLeft.WasReleasedThisFrame()) { leftIsHeld = false; }

            // move left
            if (leftIsHeld)
            {
                var curVector = transform.localPosition;
                curVector.x = curVector.x - movementSpeed * Time.deltaTime;
                transform.localPosition = curVector;
            }

            // D key
            if (Game.Instance.input.Default.MoveRight.WasPressedThisFrame()) { rightIsHeld = true; }
            if (Game.Instance.input.Default.MoveRight.WasReleasedThisFrame()) { rightIsHeld = false; }

            // move right
            if (rightIsHeld)
            {
                var curVector = transform.localPosition;
                curVector.x = curVector.x + movementSpeed * Time.deltaTime;
                transform.localPosition = curVector;
            }

            // space key
            if (Game.Instance.input.Default.Jump.WasPressedThisFrame() && jumpsAvailable > 0)
            {
                jumpIsHeld = true;
                jumpsAvailable--;

                ResetYVelocity();
                rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);

                var vel = rb.velocity;
                print(vel.y);
            }
            if (Game.Instance.input.Default.Jump.WasReleasedThisFrame() && jumpIsHeld) {
                jumpIsHeld = false;

                ResetYVelocity();
                rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
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
                }
            }
    }

    public void TakeDamage(int trapDamage) {
        playerHealth -= trapDamage;
    }
}
