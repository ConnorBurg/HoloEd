using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FractionZoneManager : MonoBehaviour
{

    public GameObject fullCell, emptyCell;
    List<GameObject> currFractionNum = new List<GameObject>();
    List<GameObject> currFractionDen = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void redraw(int[] fractionCount){
        // Destroy the current fraction
        if(currFractionNum.Count > 0)
            foreach(GameObject obj in currFractionNum)
            {
                Destroy(obj);
            }

        if(currFractionDen.Count > 0)
            foreach(GameObject obj in currFractionDen)
            {
                Destroy(obj);
            }
           

        // Create the new fraction
        for(int i = 0; i < fractionCount[0]; i++)
        {
            if(i % 2 == 0){
                currFractionNum.Add(Instantiate(fullCell, new Vector3(i*.125f,.6f,0), Quaternion.identity));
                currFractionNum[i].GetComponent<XRGrabInteractable>().enabled = false;
            }
            else{
                currFractionNum.Add(Instantiate(fullCell, new Vector3(i*.125f*(-1),.6f,0), Quaternion.identity));
                currFractionNum[i].GetComponent<XRGrabInteractable>().enabled = false;
            }
        }
        for(int i = 0; i < fractionCount[1]; i++)
        {
            if(i % 2 == 0){
                currFractionDen.Add(Instantiate(emptyCell, new Vector3(i*.125f,0,0), Quaternion.identity));
                currFractionDen[i].GetComponent<XRGrabInteractable>().enabled = false;
            }
            else{
                currFractionDen.Add(Instantiate(emptyCell, new Vector3(i*.125f*(-1),0,0), Quaternion.identity));
                currFractionDen[i].GetComponent<XRGrabInteractable>().enabled = false;
            } 
        }
    }
}
