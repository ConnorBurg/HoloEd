using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class properRotation : MonoBehaviour
{

    public GameObject myEarth;
    public XRGrabInteractable earthInteractable;
    public InputDevice targetDeviceR, targetDeviceL;
    public ActionBasedController rightHand, leftHand;
    public bool myToggle;
    Vector3 initRot, startPos;
    bool gripBool, primBool, lTrigBool, rTrigBool;
    float[] rhandPos = new float[9]; // cx, cy, cz, px, py, pz, dx, dy, dz;
    public GameObject player;
    private bool myAxis = false;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize your input system here and get some initial locations as well as add event listeners
        TryInit();
        startPos = myEarth.transform.position;
        initRot = rightHand.transform.localEulerAngles;

        rhandPos[0] = startPos.x;
        rhandPos[1] = startPos.y;
        rhandPos[2] = startPos.z;

        earthInteractable.selectEntered.AddListener(BoolOn);
        earthInteractable.selectExited.AddListener(BoolOff);

    }

    //Event listeners toggle the boolean that indicates the object is being grabbed
    private void BoolOn(SelectEnterEventArgs arg0)
    {
        myToggle = true;
    }

    private void BoolOff(SelectExitEventArgs arg0)
    {
        myToggle = false;
    }

    void TryInit()
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

        //return targetDeviceR;
    }

    // Update is called once per frame
    void Update()
    {
        // Get current hand position
        rhandPos[0] = rightHand.transform.position.x;
        rhandPos[1] = rightHand.transform.position.y;
        rhandPos[2] = rightHand.transform.position.z;

        //Manage where we are relative to the globe
        if(Math.Abs(player.transform.position.x) > Math.Abs(player.transform.position.z))
        {
            myAxis = false;
        }
        else if(Math.Abs(player.transform.position.z) > Math.Abs(player.transform.position.x))
        {
            myAxis = true;
        }

        // If we're currently grabbing
        if (myToggle)
        {
            RotateModel(myAxis);
        }

        // Update previous at end of the update
        rhandPos[3] = rightHand.transform.position.x; // px
        rhandPos[4] = rightHand.transform.position.y; // py
        rhandPos[5] = rightHand.transform.position.z; // pz
    }


    void RotateModel(bool axisOfRotation)
    {
        //Debug.Log("Selecting Object");
        rhandPos[6] = rhandPos[0] - rhandPos[3]; // dx
        rhandPos[7] = rhandPos[1] - rhandPos[4]; // dy
        rhandPos[8] = rhandPos[2] - rhandPos[5]; // dz

        if (Math.Abs(rhandPos[6]) > Math.Abs(rhandPos[7]))
        {
            myEarth.transform.Rotate(0, rhandPos[6] * 250 * -1, 0, Space.World);
        }
        else
        {
            if (myAxis)
            {
                if(player.transform.position.z < 0)
                {
                    myEarth.transform.Rotate(rhandPos[7] * 250, 0, 0, Space.World);
                }
                else
                {
                    myEarth.transform.Rotate(rhandPos[7] * 250 * -1, 0, 0, Space.World);
                }
            }
            else
            {
                if (player.transform.position.x < 0)
                {
                    myEarth.transform.Rotate(0, 0, rhandPos[7] * 250 * -1, Space.World);
                }
                else
                {
                    myEarth.transform.Rotate(0, 0, rhandPos[7] * 250, Space.World);
                }
            }
        }

    }
}