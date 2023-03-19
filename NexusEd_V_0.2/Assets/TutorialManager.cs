using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialManager : MonoBehaviour
{

    public GameObject display;
    public GameObject baton, menu, tutCharge;
    TutCharge tutChargeScript;
    bool tutDone = false;
    ChargeManager chargeManager;
    public int tutState = 0;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Set script references
        tutChargeScript = tutCharge.GetComponent<TutCharge>();
        tutState = 0;
        chargeManager = baton.GetComponent<ChargeManager>();

        
         if(tutState == 0){
            display.GetComponent<DisplayController>().setDisplay("Welcome to the tutorial!");
            yield return new WaitForSeconds(3);
            tutState++;
        }  
        
    }

    // Update is called once per frame
    void Update()
    {
       
       // Each stage of the tutorial is handled by a different if statement
        if(tutState == 1){
                display.GetComponent<DisplayController>().setDisplay("Grab the baton to start the game!");
                if(baton.GetComponent<XRGrabInteractable>().isSelected){// if baton grabed, move on
                    tutState = 2;
                }   
        }

        if(tutState == 2){
            display.GetComponent<DisplayController>().setDisplay("Select Game Mode!");
            // Activate the menu
            menu.SetActive(true); 

        }
        if(tutState == 3){// They've pressed a button on the menu
            display.GetComponent<DisplayController>().setDisplay("Swing at charges with the same value as the expression shown here: 2");
            // Hide the menu
            menu.SetActive(false);
            // Activate the charge
            tutChargeScript.onStart();
            
        }

        if(tutState == 4){
            display.GetComponent<DisplayController>().setDisplay("Good Luck!");
        }  
    }

    // Called when the parent gameobject is enabled
    public void onStart(){
        this.gameObject.SetActive(true);
        tutState = 1;
    }
    // Called to start the main minigame
    void startGame(){
        chargeManager.toggleStart();
        chargeManager.onStart();
        this.gameObject.SetActive(false);
    }

    // Called when the player hits a charge in the tutorial
    public void hitCharge(){
        tutDone = true;
        tutState = 2;
    }

    // Menu item choices
    public void setMode(int mode){
        if(mode == 1){ // They choose to start game
            menu.SetActive(false);
            startGame();
        }else{ // Choose to start tutorial
            tutState++;
        }

    }
}
