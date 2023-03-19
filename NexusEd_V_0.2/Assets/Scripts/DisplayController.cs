using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    public GameObject display;
    int globalValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Default
        this.GetComponentInChildren<TMP_Text>().text = "0";
    }

    /* Update is called once per frame
    void Update()
    {
        
    }
*/
    // Called externally to set the display
    public void setDisplay(string expression){
        this.GetComponentInChildren<TMP_Text>().text = expression;
    }

    // Called externally to set the global value
    public void setGlobalValue(int globalValue){
        this.globalValue = globalValue;
    }

    // Called externally to get the global value
    public int getGlobalValue(){
        return globalValue;
    }

}
