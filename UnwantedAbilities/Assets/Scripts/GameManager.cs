using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates {
    GAME,
    DEAD,
    END,
    MENU
}

public class GameManager : MonoBehaviour
{
    public GameStates state { get; set;}
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
