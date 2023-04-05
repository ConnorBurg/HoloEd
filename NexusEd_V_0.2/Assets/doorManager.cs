using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;

public class doorManager : MonoBehaviour
{
    public string attachedSceneName;
    public bool exitDoor;
    public BoxCollider myGuy;
    public GameObject thePlayer;
    public GameObject doorButton;
    private bool inZone;
    public InputActionReference a_Button = null;
    public GameObject mainController;

    private MainManager myMainManager;

    // Start is called before the first frame update
    void Start()
    {

        
        if ((dataManager.myInstance.gamesCompleted > 0) && (SceneManager.GetActiveScene().name == "MainArea"))
        {
            thePlayer.transform.position = dataManager.myInstance.spawnLocation;
            thePlayer.transform.RotateAround(thePlayer.transform.position, thePlayer.transform.up, 180f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (inZone)
        {
            if (!exitDoor)
            {
                if (a_Button.action.triggered)
                {
                    dataManager.myInstance.gamesCompleted++;
                    dataManager.myInstance.spawnLocation = thePlayer.transform.position;
                    SceneManager.LoadScene(attachedSceneName, LoadSceneMode.Single);
                }
            }
            else if (exitDoor)
            {
                if (a_Button.action.triggered)
                {
                    SceneManager.LoadScene("MainArea", LoadSceneMode.Single);
                }
            }


        }
    }


    //OnCollisionEnter is called when a collision starts
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("COLLISION IS HAPPENING");
        if(collision.gameObject.name == thePlayer.name)
        {
            Debug.Log("PLAYER COLLISION IS OCCURING");

            inZone = true;
            /*
            //This is where we spawn the UI
            doorButton.SetActive(true);
            StartCoroutine(GrowOrShrink(true));*/
        }
    }

    //OnCollisionExit is called when a collision ends
    private void OnTriggerExit(Collider collision)
    {
        
        if(collision.gameObject.name == thePlayer.name)
        {
            inZone = false;
            /*
            //This is where we de-spawn the UI
            StartCoroutine(GrowOrShrink(false));
            doorButton.SetActive(false); */
        }
    }

    IEnumerator GrowOrShrink(bool amIGrowing)
    {
        Vector3 scalingVector = new Vector3(0.1f, 0.1f, 0.1f);

        //if growing, then grow
        if (amIGrowing == true)
        {
            for (int reps = 0; reps > 100; reps += 1)
            {
                doorButton.transform.localScale += scalingVector;
                yield return new WaitForSeconds(.1f);
            }
        }
        else
        {
            for (int reps = 0; reps > 100; reps += 1)
            {
                doorButton.transform.localScale -= scalingVector;
                yield return new WaitForSeconds(.1f);
            }
        }

        yield return null;
    }
}
