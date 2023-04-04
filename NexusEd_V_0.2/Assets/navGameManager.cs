using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class navGameManager : MonoBehaviour
{
    public GameObject holoSphere;
    public GameObject holoEarth;
    public GameObject tutText;

    public InputActionReference a_Button = null, lGrip = null, b_button = null, x_Button = null;

    private int[] state = new int[2];

    private string[] regions = { "Africa", "South America", "Central America", "North America", "The Arctic", "Europe", "Asia", 
        "The Middle East", "Oceania", "The Atlantic Ocean", "The Pacific Ocean", "The Mediterranean Sea", "The Indian Ocean", "The Caribbean"};

    // Start is called before the first frame update
    void Start()
    {
        state[0] = 0;
        state[1] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Change tutorial text and other elements on state changes
       if(state[0] == 0 && state[1] == 1)
        {

        }
       else if(state[0] == 1 && state[1] == 2)
        {

        }
       else if(state[0] == 2 && state[1] == 3)
        {

        }

       //Take actions dependent on current state and current state alone (i.e. listening for submit inputs, showing feedback text, etc.)
       if(state[1] == 0)
        {
            if (a_Button.action.triggered)
            {
                state[1] = 1;
                tutText.GetComponentInChildren<TMP_Text>().text = "You can grab the holo-globe to rotate it and place the holo-cube inisde. \n (press A to continue)";
            }
        }
    }
}
