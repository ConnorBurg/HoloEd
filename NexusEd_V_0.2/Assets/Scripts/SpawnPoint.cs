using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnPoint : MonoBehaviour
{

    public GameObject chargeObj;
    GameObject tmpCharge;
    int ctr = 0, tagCtr = 0; 
    public int expCtr = 0;
    Queue<Charge1> charges = new Queue<Charge1>();
    GameObject display, scoreboard;
    DisplayController displayScript;

    // Start is called before the first frame update
    void Start()
    {
        displayScript = display.GetComponent<DisplayController>();
        tmpCharge = Instantiate(chargeObj, this.transform.position, new Quaternion(), this.transform);
        tmpCharge.name = "charge_" + tagCtr.ToString();
        charges.Enqueue(new Charge1(tmpCharge, int.Parse(display.GetComponentInChildren<TMP_Text>().text)));
    }

    // Update is called once per frame
    void Update()
    {

          if(ctr == 500){
            addCharge();
            ctr = 0;
        }
        ctr++;
        
    }

    void addCharge(){

        // Generate random number
        switch(Random.Range(-1,2)){
            case -1:
                tmpCharge = Instantiate(chargeObj, new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z + .75f),
                 new Quaternion(), this.transform);
                break;
            case 0:
                tmpCharge = Instantiate(chargeObj, new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z),
                 new Quaternion(), this.transform);
                break;
            case 1:
                tmpCharge = Instantiate(chargeObj, new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z - .75f),
                 new Quaternion(), this.transform);
                break;
        }
        
        tmpCharge.name = "charge_" + tagCtr.ToString();
        charges.Enqueue(new Charge1(tmpCharge, int.Parse(display.GetComponentInChildren<TMP_Text>().text)));
        Debug.Log("Added charge: " + tmpCharge.name);
        Debug.Log("Front: " + charges.Peek().getChargeObj().name);
        tagCtr++;
    }
    bool compareCharge(Charge charge){
        return charge.getValue() == displayScript.getGlobalValue();
    }

    public void removeCharge(GameObject chargeObj){
        Debug.Log("Charge :" + chargeObj.name + " removed");
        charges.Dequeue();;
        Destroy(chargeObj);
    }

    public Charge1 takePeek(){
        return charges.Peek();
    }

}


public class Charge1{
    int value, value0, value1, globalVal;
    string op; 
    string[] ops = {"+", "-", "*"};

    TMP_Text chargeText;
    GameObject chargeObj;
    public Charge1(GameObject chargeObj, int globalVal){
        this.chargeObj = chargeObj;
        this.op = ops[Random.Range(0, 3)];
        this.globalVal = globalVal;

        if(Random.Range(1,4) == 1)
            this.value = globalVal;
        else
            this.value = Random.Range(1, 16);
        

        chargeText = chargeObj.GetComponentInChildren<TMP_Text>();
        chargeText.text = this.value.ToString();
        
    }

    void setValue(){
        switch(op){
            case "+":
                value = value0 + value1;
                break;
            case "-":
                value = value0 - value1;
                break;
            case "*":
                value = value0 * value1;
                break;
        }
        chargeText = chargeObj.GetComponentInChildren<TMP_Text>();
        chargeText.text = value0 + op + value1;
    }

    public int getValue(){
        return value;
    }

    public GameObject getChargeObj(){
        return chargeObj;
    }
}

