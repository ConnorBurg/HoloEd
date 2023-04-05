using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class navGameManager : MonoBehaviour
{
    public GameObject holoSphere;
    public GameObject holoEarth;
    public GameObject tutText;
    public SphereManager mySphereManager;
    public GameObject scoreboard;
    public GameObject winboard;
    public GameObject ExitTrigger;


    public XRRayInteractor myRight;
    public XRRayInteractor myLeft;
 
    public InputActionReference a_Button = null, lGrip = null, b_button = null, x_Button = null;

    private int[] state = new int[2];
    private int myIndex;
    private int score;
    private int questions;
    private TMP_Text scoreText;
    private BoxCollider exitCollider;

    private string[] regions = { "Africa", "South America", "Central America", "North America", "The Arctic", "Europe", "Asia", 
        "The Middle East", "Oceania", "The Atlantic Ocean", "The Pacific Ocean", "The Mediterranean Sea", "The Indian Ocean", "The Caribbean"};
    private string[] myTags = { "Africa", "SouthAmerica", "CentralAmerica", "NorthAmerica", "Arctic", "Europe", "Asia", 
        "MiddleEast", "Oceania", "AtlanticOcean", "PacificOcean", "MediterraneanSea", "IndianOcean", "Caribbean"};

    // Start is called before the first frame update
    void Start()
    {
        state[0] = 0;
        state[1] = 0;

        mySphereManager = holoSphere.GetComponent<SphereManager>();
        score = 0;
        questions = 0;
        scoreText = scoreboard.GetComponentInChildren<TMP_Text>();

        exitCollider = ExitTrigger.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //First check is to toggle tutorial text (we can do that after we've gone past the welcome message)
        if((x_Button.action.triggered) && (state[1] >= 1))
        {
            tutText.SetActive(!tutText.activeSelf);
        }

        //Once we've scored more than five, we can let the user know they've calibrated the machine
        if(score >= 5)
        {
            winboard.SetActive(true);
            exitCollider.enabled = true;
        }

       //Take actions dependent on current state and current state alone (i.e. listening for submit inputs, showing feedback text, etc.)
       if(state[1] == 0)
        {
            if (a_Button.action.triggered)
            {
                state[1] = 1;
                tutText.GetComponentInChildren<TMP_Text>().text = "You can grab the holo-globe to rotate it and place the holo-cube inisde. Give it a try! \n (toggle this tutorial text with X)";
                myLeft.selectEntered.AddListener(ShowInstructions);
                myRight.selectEntered.AddListener(ShowInstructions);
            }
        }
       else if (state[1] == 2)
        {
            if (a_Button.action.triggered)
            {
                state[1] = 3;
                ShowCountry();
            }
        }
       else if (state[1] == 3)
        {
            if (a_Button.action.triggered)
            {
                //If there's an intersection, we can check that it's the correct one
                if (mySphereManager.GetColStatus())
                {
                    //If the intersection is correct, we can give the user more score and move on
                    if (mySphereManager.getCurrentNations().Contains(myTags[myIndex]))
                    {
                        score++;
                        questions++;
                        scoreText.text = "Score: " + score.ToString() + "/" + questions.ToString();
                        ShowCountry();
                    }
                    else
                    {
                        questions++;
                        scoreText.text = "Score: " + score.ToString() + "/" + questions.ToString();
                        ShowCountry();
                    }
                }
                //If there's no intersection, we need to prompt the user to intersect properly
                else
                {
                    tutText.GetComponentInChildren<TMP_Text>().text = "Please find: " + regions[myIndex] + 
                        "\n (Press A to lock in your answer)" + "\n Make sure the sphere is glowing white!";
                }
            }
        }
    }

    private void ShowInstructions(SelectEnterEventArgs arg0)
    {
        if(state[1] == 1)
        {
            tutText.GetComponentInChildren<TMP_Text>().text = "Well done! We need a navigator to re-calibrate the system. We'll ask you to place the " +
                "holo-sphere inside some geographic areas, and if you get us enough correct references, we should be able to get back up and running" +
                "\n (press A to start the calibration process)";
            state[1] = 2;
        }
    }

    private void ShowCountry()
    {
        myIndex = Random.Range(0, regions.Length);
        tutText.GetComponentInChildren<TMP_Text>().text = "Please find: " + regions[myIndex] + "\n (Press A to lock in your answer)";
    }
}
