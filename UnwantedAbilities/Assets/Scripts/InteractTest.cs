using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTest : MonoBehaviour
{
    private Input @input;
    public GameObject interact;
    private bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        input = new Input();
        input.Enable();
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.Instance.input.Default.Interact.triggered && inRange){
            Destroy(interact);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            inRange = false; 
        }
    }
}
