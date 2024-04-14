using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTest : MonoBehaviour
{
    public Player player;
    private bool inWater;
    private int waitTime;
    private bool jumpIsHeld;
    public Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        inWater = false;
        waitTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (inWater == true && player.waterBreathing == false) {

            waitTime++;
            if (waitTime == 100) {
                Player.stamina--;
                print(Player.stamina);
                waitTime = 0;
            }
            if (waitTime == 100 && Player.stamina == 0 && player.playerHealth > 0) {
                player.playerHealth--;
                print(player.playerHealth);
                waitTime = 0;
            }
        }
        
       
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Player")){
            Player.movementSpeed = 2.5f;
            Player.jumpSpeed = .1f;
            Player.jump_tot_time = 100000f;
            rb2d.gravityScale = 0;
            inWater = true;
            if (Player.jumpIsHeld) {
                rb2d.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            Player.movementSpeed = 5f;
            Player.jumpSpeed = 10f;
            Player.jump_tot_time = .5f;
            Player.stamina = 15;
            rb2d.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
            rb2d.gravityScale = 1;
            inWater = false;        
        }
    }
}
