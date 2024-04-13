using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEdward : MonoBehaviour
{ 
    // Rigidbody for physics simulation
    public Rigidbody2D rb;

    // x movement speed
    public float movementSpeed = 5f;

    // y movement speed
    public float jumpForce = 10f;

    // Jump time variables
    private bool isJumping = false;
    private float jumpTimeCounter;
    public float jumpTime = 0.5f;

    // Player stats
    public int playerHealth = 30;

    // Input actions
    private Input @input;

    // Start is called before the first frame update
    void Start()
    {
        GameObject water = GameObject.FindWithTag("ShouldBouyant");
        BuoyancyEffector2D Buoyancy = water.GetComponent<BuoyancyEffector2D>();
        Buoyancy.density = 0;
        rb = GetComponent<Rigidbody2D>(); 
        @input = new Input();
        @input.Enable();
    }

    private void OnDestroy()
    {
        @input.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float moveInput = @input.Default.MoveRight.ReadValue<float>() - @input.Default.MoveLeft.ReadValue<float>();
        rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);

        // Jumping
        if (@input.Default.Jump.triggered && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }


        if (@input.Default.Jump.ReadValue<float>() > 0 && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (@input.Default.Jump.triggered)
        {
            isJumping = false;
        }

        //Swimming Movement

    }
}