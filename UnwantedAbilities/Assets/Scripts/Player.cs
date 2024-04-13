using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //x movement speed
    private float movementSpeed = 5f;

    //y movement speed
    private float jumpSpeed = 10f;

    //longest amount of time the jump key is allowed to be held
    private float jump_tot_time = 0.5f;

    //how long the jump key has been held for
    private float jump_cur_time = 0f;

    //keyboard presses
    private bool upIsHeld = false;
    private bool downIsHeld = false;
    private bool leftIsHeld = false;
    private bool rightIsHeld = false;
    private bool jumpIsHeld = false;

    private int jumpsAvailable;
    private int maxJumps = 1;

    public BoxCollider2D bc;
    public Rigidbody2D rb;
    public BuoyancyEffector2D buoyancy;
    public GameObject water;
    public PolygonCollider2D waterCollider;
	
    public double Stamina;
    public LayerMask groundLayer;
    private float raycastDistance = 1;

    // Start is called before the first frame update
    void Start()
    {
        Stamina = 100;
        water = GameObject.FindWithTag("ShouldBouyant");
        buoyancy = water.GetComponent<BuoyancyEffector2D>();
        waterCollider = water.GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
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

        if (!bc.IsTouching(waterCollider))
        {
            
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
                jump_cur_time = 0f;
                jumpsAvailable--;

                ResetYVelocity();
                rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);

                var vel = rb.velocity;
                print(vel.y);
            }
            if (Game.Instance.input.Default.Jump.WasReleasedThisFrame() && jumpIsHeld) {
                jumpIsHeld = false;

                ResetYVelocity();
                rb.AddForce(Vector2.down * 10f, ForceMode2D.Impulse);
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

        //SWIMMING MOVEMENT
        if (bc.IsTouching(waterCollider))
        {
            //CHECK IF DROWNING
            if (bc.transform.localPosition.y < waterCollider.transform.localPosition.y + 8)
            {
                Stamina += -.1;
            }
            else
            {
                Stamina = 100;
            }

            if (Stamina < 0)
            {
                print("death");
            }


            //rb.AddForce(new Vector2(0, -1f) * rb.mass * 1.2f);

            // W key
            if (Game.Instance.input.Default.MoveUp.WasPressedThisFrame()) { upIsHeld = true; }
            if (Game.Instance.input.Default.MoveUp.WasReleasedThisFrame()) { upIsHeld = false; }

            // move Up
            if (upIsHeld)
            {
                var curVector = transform.localPosition;
                curVector.y = curVector.y + movementSpeed * Time.deltaTime;
                transform.localPosition = curVector;
            }

            // S key
            if (Game.Instance.input.Default.MoveDown.WasPressedThisFrame()) { downIsHeld = true; }
            if (Game.Instance.input.Default.MoveDown.WasReleasedThisFrame()) { downIsHeld = false; }

            // move left
            if (downIsHeld)
            {
                var curVector = transform.localPosition;
                curVector.y = curVector.y - movementSpeed * Time.deltaTime;
                transform.localPosition = curVector;
            }

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
                jump_cur_time = 0f;
                jumpsAvailable--;

                ResetYVelocity();
                rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            }
            if (Game.Instance.input.Default.Jump.WasReleasedThisFrame() && jumpIsHeld)
            {
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

    }


}
