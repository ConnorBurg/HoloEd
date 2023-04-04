using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navGameManager : MonoBehaviour
{
    public GameObject holoSphere;
    public GameObject holoEarth;
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

    }
}
