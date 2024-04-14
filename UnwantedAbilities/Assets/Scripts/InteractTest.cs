using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class InteractTest : MonoBehaviour
{
    private Input @input;
    public GameObject interact;
    public GameObject fireStatue;
    public GameObject waterStatue;
    public GameObject airStatue;
    public GameObject replacement;
    public GameObject textObject;
    public TMP_Text popUp;
    public string textStr;
    private Vector2 pos;
    private Vector2 textPos;
    private bool inRange;
    public Player player;
    public bool fireCheck = false;
    public bool waterCheck = false;
    public bool jumpCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        input = new Input();
        input.Enable();
        inRange = false;
        interact = gameObject.GetComponent<GameObject>();
        pos = interact.transform.position;
        textPos = textObject.transform.position;
    }
    public void callFireStatue()
    {
        player.noFireImmunity();
        fireCheck = true;
        player.RelocatePlayer();
        checkIfWin();
        Destroy(fireStatue);
 
    }

    public void callWaterStatue()
    {
            player.noWaterBreathing();
            waterCheck = true;
            player.RelocatePlayer();
            checkIfWin();
            Destroy(waterStatue);
    
    }
    public void callAirStatue()
    {
        player.noDoubleJump();
        jumpCheck = true;
        player.RelocatePlayer();
        checkIfWin();
        Destroy(airStatue);
    }


    // Update is called once per frame
    void Update()
    {
        

       
    }
    public void checkIfWin()
    {
        if(waterCheck == true && fireCheck == true && jumpCheck == true)
        {

            player.CongratsPlayer();
        }

    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;
            popUp.text = textStr;
            popUp.transform.position = new Vector2(pos.x, pos.y + 2f);
            
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            inRange = false; 
            popUp.transform.position = new Vector2(textPos.x, textPos.y);
        }
    }
}
