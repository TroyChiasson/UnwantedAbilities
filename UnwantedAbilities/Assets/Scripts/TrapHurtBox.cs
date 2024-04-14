using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapHurtBox : MonoBehaviour
{
    // Object
    public Player player;

    public int trapDamage; 

    // Trap Damage on Hit
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject == player.gameObject) {
            
            player.TakeDamage(trapDamage);
                   
        }
    }
}
