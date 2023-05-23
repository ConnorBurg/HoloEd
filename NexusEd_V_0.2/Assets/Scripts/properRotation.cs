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
    float[] lhandPos = new float[9]; // cx, cy, cz, px, py, pz, dx, dy, dz;
    float[] resPos = new float[3];
    public GameObject player;
    private bool myAxis = false;
    private string myTag;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize your input system here and get some initial locations as well as add event listeners
        TryInit();
        startPos = myEarth.transform.localPosition;
        initRot = rightHand.transform.localEulerAngles;

        rhandPos[0] = startPos.x;
        rhandPos[1] = startPos.y;
        rhandPos[2] = startPos.z;

        lhandPos[0] = startPos.x;
        lhandPos[1] = startPos.y;
        lhandPos[2] = startPos.z;

        earthInteractable.selectEntered.AddListener(BoolOn);
        earthInteractable.selectExited.AddListener(BoolOff);

    }

    //Event listeners toggle the boolean that indicates the object is being grabbed
    private void BoolOn(SelectEnterEventArgs arg0)
    {
        myTag = arg0.interactorObject.transform.gameObject.tag;
        myToggle = true;
    }

    private void BoolOff(SelectExitEventArgs arg0)
    {
        myTag = "";
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
        rhandPos[0] = rightHand.transform.localPosition.x;
        rhandPos[1] = rightHand.transform.localPosition.y;
        rhandPos[2] = rightHand.transform.localPosition.z;

        lhandPos[0] = leftHand.transform.localPosition.x;
        lhandPos[1] = leftHand.transform.localPosition.y;
        lhandPos[2] = leftHand.transform.localPosition.z;

        //Manage where we are relative to the globe
        if (Math.Abs(player.transform.position.x) > Math.Abs(player.transform.position.z))
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
        rhandPos[3] = rightHand.transform.localPosition.x; // px
        rhandPos[4] = rightHand.transform.localPosition.y; // py
        rhandPos[5] = rightHand.transform.localPosition.z; // pz

        lhandPos[3] = leftHand.transform.localPosition.x; // px
        lhandPos[4] = leftHand.transform.localPosition.y; // py
        lhandPos[5] = leftHand.transform.localPosition.z; // pz
    }


    void RotateModel(bool axisOfRotation)
    {

        if (myTag == "RH")
        {
            resPos[0] = rhandPos[0] - rhandPos[3]; // dx
            resPos[1] = rhandPos[1] - rhandPos[4]; // dy
            resPos[2] = rhandPos[2] - rhandPos[5]; // dz
        }
        else if(myTag == "LH")
        {
            resPos[0] = lhandPos[0] - lhandPos[3]; // dx
            resPos[1] = lhandPos[1] - lhandPos[4]; // dy
            resPos[2] = lhandPos[2] - lhandPos[5]; // dz
        }

        if (Math.Abs(resPos[0]) > Math.Abs(resPos[1]))
        {
            myEarth.transform.Rotate(0, resPos[0] * 250 * -1, 0, Space.World);
        }
        else
        {
            if (myAxis)
            {
                if (player.transform.position.z < 0)
                {
                    myEarth.transform.Rotate(resPos[1] * 250, 0, 0, Space.World);
                }
                else
                {
                    myEarth.transform.Rotate(resPos[1] * 250 * -1, 0, 0, Space.World);
                }
            }
            else
            {
                if (player.transform.position.x < 0)
                {
                    myEarth.transform.Rotate(0, 0, resPos[1] * 250 * -1, Space.World);
                }
                else
                {
                    myEarth.transform.Rotate(0, 0, resPos[1] * 250, Space.World);
                }
            }
        }
    }
}