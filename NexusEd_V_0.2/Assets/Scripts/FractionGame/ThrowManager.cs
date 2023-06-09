using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    public GameObject fractionZoneObject;
    FractionZoneManager fractionZoneRef;
    public delegate void OnSuccessfulThrow();
    public static OnSuccessfulThrow onSuccessfulThrow;
    public Scoreboard scorebordRef;

    // fractionCount[0] == Number of fullCells 
    // fractionCount[1] == Number of emptyCells
    public int[] fractionCount = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        fractionCount[0] = 0;
        fractionCount[1] = 0;
        fractionZoneRef = fractionZoneObject.GetComponent<FractionZoneManager>();
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void resetCount(){

        fractionCount[0] = 0;
        fractionCount[1] = 0;
        fractionZoneRef.redraw(fractionCount);
    }
            
    //on trigger enter
    private void OnTriggerEnter(Collider other)
    {
        //if the object is a fraction
        if (other.gameObject.tag == "fullCell" || other.gameObject.tag == "emptyCell")
        {
            onSuccessfulThrow?.Invoke();
            if (other.gameObject.tag == "fullCell")
            {
                fractionCount[0]++;
            }
            else if(other.gameObject.tag == "emptyCell")
            {
                fractionCount[1]++;
            }

            Destroy(other.gameObject);
            fractionZoneRef.redraw(fractionCount);
            int[] tmpFrac = GameManager.tmpFrac;
            if (FractionTutorial.tutState != -1)
                scorebordRef.tutorialPrompt(tmpFrac[0] - fractionCount[0], tmpFrac[1] - tmpFrac[0] - fractionCount[1]);
        }
    }
}
