using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapHurtBox : MonoBehaviour
{
    // Object
    public Player player;
    public static bool inTrap;
    public int trapDamage; 


    public int frameTimer = 1;
    void Update() {
        frameTimer++;
        if (inTrap == true && frameTimer % 10 == 0) {
            player.TakeDamage(trapDamage);
            Debug.Log(player.playerHealth);
            frameTimer = 1;
        }
    }
    // Trap Damage on Hit
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject == player.gameObject) {
            inTrap = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject == player.gameObject) {
            inTrap = false;
        }
    }
}
