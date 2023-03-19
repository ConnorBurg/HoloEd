using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChargeManager : MonoBehaviour
{
    public GameObject chargeObj;
    public GameObject spawnPoint, display, scoreboard;
    SpawnPoint spawnScript;
    DisplayController displayScript;
    public GameObject tutorial;
    TutorialManager tutorialManager;
    GameObject tmpCharge;
    int globalValue = 0, renewCtr = 0;
    string globalOp = " ";
    int ctr = 0, tagCtr = 0, expCtr = 0;
    bool startDelayed = true;
    Queue<Charge> charges = new Queue<Charge>();

    // Start is called before the first frame update
    void Start()
    {
        tutorialManager = tutorial.GetComponent<TutorialManager>();

        /*
        tmpCharge = Instantiate(chargeObj, spawnPoint.transform.position, new Quaternion(), spawnPoint.transform);
        tmpCharge.name = "charge_" + tagCtr.ToString();
        charges.Enqueue(new Charge(tmpCharge, globalValue));*/
    }
    public void onStart(){

        //startDelayed = false;
        spawnScript = spawnPoint.GetComponent<SpawnPoint>();
        displayScript = display.GetComponent<DisplayController>();
        globalValue = generateExpression();
        //Set the scoreboard to 0
        scoreboard.GetComponentInChildren<TMP_Text>().text = "Score: " + expCtr.ToString();
        
    }


    // Update is called once per frame
    void Update()
    {   
        // if start isn't delayed (in tutorial), start the game
        if(!startDelayed){

            // Have a counter to determine when to spawn a new charge. As the user's score increases, spawn charges more frequently
            if(ctr == 75 - (expCtr/2)){
                addCharge();
                ctr = 0;
            }
            ctr++;
        }

        // Check if their score is -5, if it is, restart the game
        if(expCtr < -5 ){
            expCtr = 0;
            tutorialManager.onStart();
            startDelayed = true;

        }
           
    }
    
    // Generate a math expression using multiplicaiton, addition, and subtraction of using integers 1-15, except multiplication of 1-10
    int generateExpression(){
        int value0, value1, value2;
        int op = Random.Range(1, 4);

        // if their score is high, make the expression harder
        if(expCtr > 15){
            // Switch statement to determine the operation based on random generation. 1 = addition, 2 = subtraction, 3 = multiplication. Generate the two random values
            //and calculate the answer
            if(op == 1){
                value0 = Random.Range(1, 50);
                value1 = Random.Range(1, 50);
                globalOp = "+";
                value2 = value0 + value1;
            }else if(op == 2){
                globalOp = "-";
                value0 = Random.Range(1, 50);
                value1 = Random.Range(1, value0);
                value2 = value0 - value1;
            }else{
                value0 = Random.Range(1, 13);
                value1 = Random.Range(1, 13);
                globalOp = "*";
                value2 = value0 * value1;
            }

            // if the answer is less than 15, display the answer
            if(value2 < 15){
                // Set the displays and return the the global value
                displayScript.setDisplay(value0.ToString() + " " + globalOp + " " + value1.ToString() + " = " + "?");
                displayScript.setGlobalValue(value2);
                return value2;

            }else{
                // Random generate a number to randomize the expression order 
                if(Random.Range(0, 2) == 0){
                    displayScript.setDisplay("?" + " " + globalOp + " " + value1.ToString() + " = " + value2.ToString());
                    displayScript.setGlobalValue(value0);
                    return value0;
                }else{   
                    displayScript.setDisplay(value0.ToString() + " " + globalOp + " " + "?" + " = " + value2.ToString());
                    displayScript.setGlobalValue(value1);
                    return value1;
                }
            }
        }else{// lower score block
            // Same expression generation as above, but with lower values
            if(op == 1){
                value0 = Random.Range(1, 16);
                value1 = Random.Range(1, 16);
                globalOp = "+";
                value2 = value0 + value1;
            }else if(op == 2){
                globalOp = "-";
                value0 = Random.Range(1, 16);
                value1 = Random.Range(1, value0);
                value2 = value0 - value1;
            }else{
                value0 = Random.Range(1, 11);
                value1 = Random.Range(1, 11);
                globalOp = "*";
                value2 = value0 * value1;
            }
            if(value2 < 15){
                
                displayScript.setDisplay(value0.ToString() + " " + globalOp + " " + value1.ToString() + " = " + "?");
                displayScript.setGlobalValue(value2);
                return value2;

            }else{
                // Random generate a number to randomize the expression order
                if(Random.Range(0, 2) == 0){
                    displayScript.setDisplay("?" + " " + globalOp + " " + value1.ToString() + " = " + value2.ToString());
                    displayScript.setGlobalValue(value0);
                    return value0;
                }
                else{
                    displayScript.setDisplay(value0.ToString() + " " + globalOp + " " + "?" + " = " + value2.ToString());
                    displayScript.setGlobalValue(value1);
                    return value1;
                }
            }
        }
    }

    // Spawn a charge at given point.
     public void addCharge(){

        // Generate random number to determine which side of the spawn point to spawn the charge
        switch(Random.Range(-1,2)){
            case -1:
                tmpCharge = Instantiate(chargeObj, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 1.5f, spawnPoint.transform.position.z + .75f),
                 new Quaternion(), spawnPoint.transform);
                break;
            case 0:
                tmpCharge = Instantiate(chargeObj, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 1.5f, spawnPoint.transform.position.z),
                 new Quaternion(), spawnPoint.transform);
                break;
            case 1:
                tmpCharge = Instantiate(chargeObj, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 1.5f, spawnPoint.transform.position.z - .75f),
                 new Quaternion(), spawnPoint.transform);
                break;
        }
        
        // Give the charge a name and add it to the queue. We also package the charge with its value to make a Charge object.
        tmpCharge.name = "charge_" + tagCtr.ToString();
        charges.Enqueue(new Charge(tmpCharge, globalValue));
        tagCtr++;
    }

    // Remove a charge from the queue and destroy the game object
    public void removeCharge(GameObject chargeObj){
        charges.Dequeue();;
        Destroy(chargeObj);
    }

    // Called when the baton collides with something
    void OnTriggerEnter(Collider other) {

        // Make sure that we're in the tutorial
        if(startDelayed){
            // Hit the tutorial charge and let the TutorialManager know.
            if(other.gameObject.name == charges.Peek().getChargeObj().name)
                tutorialManager.hitCharge();

        }else{// Not in the tutorial
            // Check if the charge is the correct one and if the value is correct
            if(other.gameObject.name == charges.Peek().getChargeObj().name){
                if(charges.Peek().getValue() == globalValue){
                    // If the value is correct, remove the charge and increase the score
                    removeCharge(other.gameObject);
                    expCtr++;
                    // Update the score display
                    scoreboard.GetComponentInChildren<TMP_Text>().text = "Score: " + expCtr.ToString();
                    // Generate a new expression
                    globalValue = generateExpression();
                }else{// If the value is incorrect, remove the charge and decrease the score
                    removeCharge(other.gameObject);
                    expCtr--;
                    scoreboard.GetComponentInChildren<TMP_Text>().text = "Score: " + expCtr.ToString();
                    if(renewCtr == 3){ // If the player gets 3 incorrect answers in a row, generate a new expression
                        globalValue = generateExpression();
                        renewCtr = 0;
                    }else{
                        renewCtr++;
                    }
                }
            }   
        }
    }

    // Access boolean to determine if the game has started
    public void toggleStart(){
        startDelayed = !startDelayed;
    }
}

// Class to package the charge with its value (and generate its value to display)
public class Charge{

    // Fields
    int value, globalVal;
    TMP_Text chargeText;
    GameObject chargeObj;

    // Constructor to generate the value and display it
    public Charge(GameObject chargeObj, int globalVal){
        
        // Set the fields
        this.chargeObj = chargeObj;
        this.globalVal = globalVal;

        // Generate the value to be shown on the charge
        if(Random.Range(1,4) == 1)
            this.value = globalVal;
        else
            this.value = Random.Range(1, 16);
        
        // Display the value on the charge
        chargeText = chargeObj.GetComponentInChildren<TMP_Text>();
        chargeText.text = this.value.ToString();

    }

    // Accessors
    public int getValue(){
        return value;
    }

    public GameObject getChargeObj(){
        return chargeObj;
    }
}
