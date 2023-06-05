using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


public class FractionManager : MonoBehaviour
{
    public GameObject emptyBin, fullBin,fullCell, emptyCell, leftHand, rightHand;
    public InputActionReference a_Button = null, lGrip = null, rGrip = null;
    XRRayInteractor leftRay, rightRay;

    GameObject rightHitObject, leftHitObject;
    // Start is called before the first frame update
    void Start()
    {
        // Get the XR Ray Interactor component on the left hand and right hand
        leftRay = leftHand.GetComponent<XRRayInteractor>();
        rightRay = rightHand.GetComponent<XRRayInteractor>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the raycast from the left hand or right hand is hitting an object
        if (rightRay.TryGetCurrent3DRaycastHit(out RaycastHit rightInfo)){
            rightHitObject = rightInfo.collider.gameObject;
        }

        if(leftRay.TryGetCurrent3DRaycastHit(out RaycastHit leftInfo)){
            leftHitObject = leftInfo.collider.gameObject;
        }// end get hit object

        // Hit object compares
        if(rightHitObject == fullCell){
            

        }else if(rightHitObject == emptyCell){


        }

        if(leftHitObject == fullCell){
        }
        else if(leftHitObject == emptyCell){
        }
    }
}
