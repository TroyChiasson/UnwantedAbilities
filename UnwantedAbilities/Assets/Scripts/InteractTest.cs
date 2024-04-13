using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractTest : MonoBehaviour
{
    private Input @input;
    public GameObject interact;
    public GameObject replacement;
    public GameObject textObject;
    public TMP_Text popUp;
    public string textStr;
    private Vector2 pos;
    private Vector2 textPos;
    private bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        input = new Input();
        input.Enable();
        inRange = false;
        pos = transform.position;
        textPos = textObject.transform.position;
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
