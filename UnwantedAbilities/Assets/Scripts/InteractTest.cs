using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTest : MonoBehaviour
{
    private Input @input;
    public GameObject interact;
    public GameObject replacement;
    private Vector2 pos;
    private bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        input = new Input();
        input.Enable();
        inRange = false;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.Instance.input.Default.Interact.triggered && inRange){
            Destroy(interact);
            replacement.transform.position = new Vector2(pos.x, pos.y);
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
