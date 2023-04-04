using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public GameObject display;
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
    public void setDisplay(int score)
    {
        this.GetComponentInChildren<TMP_Text>().text = "Score: " + score;
    }

    public void winner() {
        this.GetComponentInChildren<TMP_Text>().text = "Engine Restart Complete!";
    }
}
