using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    public GameObject fractionZoneObject;
    FractionZoneManager fractionZoneRef;
    

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
            
    //on trigger enter
    private void OnTriggerEnter(Collider other)
    {
        //if the object is a fraction
        if (other.gameObject.tag == "fullCell" || other.gameObject.tag == "emptyCell")
        {
            if(other.gameObject.tag == "fullCell")
            {
                fractionCount[0]++;
            }
            else if(other.gameObject.tag == "emptyCell")
            {
                fractionCount[1]++;
            }

            Destroy(other.gameObject);
            fractionZoneRef.redraw(fractionCount);

            //Debug.Log("Fraction Count: " + fractionCount[0] + " " + fractionCount[1]);
            //destroy the fraction
            
        }
    }
}
