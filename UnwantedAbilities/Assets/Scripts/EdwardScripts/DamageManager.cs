using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public int playerHealth;
    public int Respawn = 1; 
    public int spikeDamage = 10; 

    // Start is called before the first frame update
    void Start()
    {
        PlayerEdward player = GameObject.FindWithTag("PlayerEdward").GetComponent<PlayerEdward>();
        playerHealth = player.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(Respawn);
            playerHealth = 50; 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerEdward"))
        {
            playerHealth -= spikeDamage; 
            Debug.Log(playerHealth);
        }
    }
}
