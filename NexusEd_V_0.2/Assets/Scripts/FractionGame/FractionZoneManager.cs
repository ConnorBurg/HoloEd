using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FractionZoneManager : MonoBehaviour
{

    public GameObject fullCell, emptyCell;
    GameObject tempCell;
    List<GameObject> currFractionUpper = new List<GameObject>();
    List<GameObject> currFractionLower = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void redraw(int[] fractionCount){

        // Instantiate empty cells equal to fractionCount[1]
        // Destroy the current fraction
        if(currFractionUpper.Count > 0){

            foreach(GameObject obj in currFractionUpper)
            {
                Destroy(obj);
            }

            currFractionUpper.Clear();
        }

        if(currFractionLower.Count > 0){

            foreach(GameObject obj in currFractionLower)
            {
                Destroy(obj);
            }
            currFractionLower.Clear();
        }

        // Create the new fraction
        for(int i = 0; i < fractionCount[0]; i++)
        {
            if(i % 2 == 0){
                currFractionUpper.Add(Instantiate(fullCell, new Vector3(i*.125f,2,0), Quaternion.identity, this.transform));
                
            }
            else{
                currFractionUpper.Add(Instantiate(fullCell, new Vector3(i*.125f*(-1),2,0), Quaternion.identity, this.transform));
                
            }
        }
        for(int i = 0; i < fractionCount[1]; i++)
        {
            if(i % 2 == 0){
                currFractionLower.Add(Instantiate(emptyCell, new Vector3(i*.125f,1,0), Quaternion.identity, this.transform));
                
            }
            else{
                currFractionLower.Add(Instantiate(emptyCell, new Vector3(i*.125f*(-1),1,0), Quaternion.identity, this.transform));
                
            } 
        }
    }
}
