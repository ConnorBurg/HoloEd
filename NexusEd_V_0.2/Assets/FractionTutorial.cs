using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class FractionTutorial : MonoBehaviour
{
    public TMP_Text tutText;
    public static int tutState = 0;

    // Start is called before the first frame update
    void Start()
    {
    }
    void OnEnable()
    {
        FractionManager.onPickUp += IncrementState1;
        ThrowManager.onSuccessfulThrow += IncrementState2;
        GameManager.onComplete += IncrementState3;
    }
    void IncrementState1()
    {
        if(tutState == 0)
            tutState += 1;
    }
    void IncrementState2()
    {
        if (tutState == 1)
            tutState += 1;
    }
    void IncrementState3()
    {
        if (tutState == 2)
            tutState += 1;
    }
    // Update is called once per frame
    void Update()
    {
        switch (tutState) {
            case 0: tutText.text = "Aim at one of the two boxes in front of you, and press A to pick up a cell."; break;
            case 1: tutText.text = "The next step is to throw this cell forward (past the boxes)."; break;
            case 2: tutText.text = "Each green cell is full of charge and each gray cell is empty of charge. Your goal is to achieve the same fraction shown on the scoreboard. Press A when you finish correctly."; break;
            case 3: tutText.text = "You have completed the tutorial! Press A again to start the game."; tutState = -1; break;

        }
    }
}
