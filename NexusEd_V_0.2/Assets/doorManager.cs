using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class doorManager : MonoBehaviour
{
    public string attachedSceneName;
    public bool exitDoor;
    public BoxCollider myGuy;
    public GameObject thePlayer;
    public GameObject doorButton;
    private bool inZone;
    public InputDevice targetDeviceR, targetDeviceL;
    bool aButton;

    // Start is called before the first frame update
    void Start()
    {

        TryInit();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!targetDeviceR.isValid || !targetDeviceL.isValid)
            TryInit();

        


        if (targetDeviceR.TryGetFeatureValue(CommonUsages.primaryButton, out aButton) && aButton)
            Debug.Log("A button being pressed");

        if (inZone)
        {
            if (!exitDoor)
            {
                if (targetDeviceR.TryGetFeatureValue(CommonUsages.primaryButton, out aButton) && aButton)
                {
                    SceneManager.LoadScene(attachedSceneName, LoadSceneMode.Additive);
                }
            }
            else if (exitDoor)
            {
                if (targetDeviceR.TryGetFeatureValue(CommonUsages.primaryButton, out aButton) && aButton)
                {
                    SceneManager.LoadScene("MainArea");
                }
            }


        }
    }


    private void TryInit()
    {

        List<InputDevice> devices = new List<InputDevice>();
        //InputDeviceCharacteristics ControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, devices);

        if (devices.Count > 0)
        {
            //Debug.Log("Number of Devices: " + devices.Count);
            targetDeviceL = devices[0];
            targetDeviceR = devices[1];
            //Debug.Log("targetDeviceL: " + devices[0].ToString() + " targetDeviceR: " + devices[1].ToString());
        }
    }

    //OnCollisionEnter is called when a collision starts
    private void OnCollisionEnter(Collision collision)
    {
        inZone = true;
        if(collision.gameObject.name == thePlayer.name)
        {
            //This is where we spawn the UI
            doorButton.SetActive(true);
            StartCoroutine(GrowOrShrink(true));
        }
    }

    //OnCollisionExit is called when a collision ends
    private void OnCollisionExit(Collision collision)
    {
        inZone = false;
        if(collision.gameObject.name == thePlayer.name)
        {
            //This is where we de-spawn the UI
            StartCoroutine(GrowOrShrink(false));
            doorButton.SetActive(false);
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
