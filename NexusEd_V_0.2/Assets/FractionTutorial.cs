using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FractionTutorial : MonoBehaviour
{

    public InputActionReference a_Button = null, lGrip = null, b_button = null, x_Button = null, y_Button = null;
    public GameObject scoreboardDisplay, fractionDisplay, tutorialDisplay, FractionZoneObject, throwZoneObject, mainLight, particleLight0, particleLight1;
    public GameObject doorTrigger;
    public bool canLeave = false;

    // Private Variables
    int[] tmpFrac = new int[2];
    int score = 0, difficulty = 0, tutState = 0;
    bool tutTextActive = true, tutActive = true;

    // Reference Variables
    FractionDisplay displayRef; 
    Scoreboard scorebordRef;
    FractionZoneManager fractionZoneRef;
    ThrowManager throwZoneRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
