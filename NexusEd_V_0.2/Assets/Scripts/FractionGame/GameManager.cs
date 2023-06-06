using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Public Variables
    public InputActionReference a_Button = null, lGrip = null, b_button = null, x_Button = null, y_Button = null;
    public GameObject scoreboardDisplay, fractionDisplay, tutorialDisplay, FractionZoneObject, throwZoneObject, mainLight, particleLight0, particleLight1;
    public GameObject doorTrigger;
    public bool canLeave = false;

    // Private Variables
    int[] tmpFrac = new int[2];
    int score = 0, difficulty = 0;
    bool tutActive = true;

    // Reference Variables
    FractionDisplay displayRef; 
    Scoreboard scorebordRef;
    FractionZoneManager fractionZoneRef;
    ThrowManager throwZoneRef;
    
   

    // Start is called before the first frame update
    void Start(){
        displayRef = fractionDisplay.GetComponent<FractionDisplay>();
        scorebordRef = scoreboardDisplay.GetComponent<Scoreboard>();
        fractionZoneRef = FractionZoneObject.GetComponent<FractionZoneManager>();
        throwZoneRef = throwZoneObject.GetComponent<ThrowManager>();
        doorTrigger.SetActive(false);
        toggleLights(false);
        fractionValueGen();
    }

    // Update is called once per frame
    void Update(){
        if (canLeave && y_Button.action.triggered) {
            SceneManager.LoadScene("MainArea", LoadSceneMode.Single);
        }

        if (x_Button.action.triggered) {
            tutActive = !tutActive;
            tutorialDisplay.SetActive(tutActive);
        }

        if (a_Button.action.triggered) {
            checkFraction();
        }

        if(b_button.action.triggered){
            throwZoneRef.resetCount();
            fractionZoneRef.clearFraction();
        }// end of clear
    }// end of update

    void fractionValueGen(){

        if(difficulty == 0){
            int[] denOps = { 2, 3, 4};
            int[] numOps = { 1, 2, 3};
            tmpFrac[1] = denOps[Random.Range(0, denOps.Length)]; // denominator
            tmpFrac[0] = numOps[Random.Range(0, tmpFrac[1] - 1)]; // numerator
        
        }else if(difficulty == 1){
            int[] denOps = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
            int[] numOps = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            tmpFrac[1] = denOps[Random.Range(0, 13)]; // denominator
            tmpFrac[0] = numOps[Random.Range(0, tmpFrac[1] - 1)]; // numerator

        }else if(difficulty == 2){
            int[] denOps = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] numOps = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            tmpFrac[1] = denOps[Random.Range(0, 13)]; // denominator
            tmpFrac[0] = numOps[Random.Range(0, tmpFrac[1] - 1)]; // numerator
        }

        // Generate fractions
        displayRef.setDisplay(tmpFrac);
    }

    void checkFraction() {
        // Need to get the current fraction from the FractionZoneManager
        int[] frac = fractionZoneRef.getFraction();
        int num = frac[0];
        int den = frac[1] + frac[0];

        if ((num == tmpFrac[0]) && (den == tmpFrac[1])){ // Correct!

            // Reset the fraction zone and generate a new fraction
            throwZoneRef.resetCount();
            fractionZoneRef.clearFraction();
            score++;
            scorebordRef.setDisplay(score);
            scorebordRef.correctAnswer();
            displayRef.correctAnswer();

            if (score == 5)
            {
                scorebordRef.winner();
                canLeave = true;
                doorTrigger.SetActive(true);
                toggleLights(true);
                difficulty++;
                score = 0;
            }
            else
            {
                fractionValueGen();
            }
        }else {
            // Reset the fraction zone and generate a new fraction
            scorebordRef.incorrectAnswer();
            displayRef.incorrectAnswer();
            throwZoneRef.resetCount();
            fractionZoneRef.clearFraction();
            fractionValueGen();     
        }
    }

    private void toggleLights(bool val){
        mainLight.SetActive(val);
        particleLight0.SetActive(val);
        particleLight1.SetActive(val);
    }
}


