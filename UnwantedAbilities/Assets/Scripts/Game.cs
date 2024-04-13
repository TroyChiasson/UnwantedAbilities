using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Input input;

    public static Game Instance { get; private set; }

    private void Awake() {
        Instance = this;
        input = new();
        input.Enable();
        // if(Game.Instance.input.Default.GuageUp.WasPressedThisFrame()) /*do function*/ ;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
