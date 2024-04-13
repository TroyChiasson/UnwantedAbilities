using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHurtBox : MonoBehaviour
{

    // this is player
    public Player player;

    // if player touch trap then player = die
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject == player.gameObject) {
            print("test");
            player.Respawn();
        }
    }
}
