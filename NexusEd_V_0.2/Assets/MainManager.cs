using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Three gameobject fields to hold the three triggers for the three levels
    public GameObject trigger1;
    public GameObject trigger2;
    public GameObject trigger3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // This method is called when trigger1's collider is entered. Once the trigger is
    // entered, we will start the new scene that is associated with that trigger.
    public void Trigger1Entered()
    {
        // Load the scene that is associated with trigger1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
    // This method is called when trigger2's collider is entered. Once the trigger is
    // entered, we will start the new scene that is associated with that trigger.
    public void Trigger2Entered()
    {
        // Load the scene that is associated with trigger2
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
    }
    // This method is called when trigger3's collider is entered. Once the trigger is
    // entered, we will start the new scene that is associated with that trigger.
    public void Trigger3Entered()
    {
        // Load the scene that is associated with trigger3
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level3");
    }


 

    
    
}