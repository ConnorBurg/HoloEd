using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


public class FractionManager : MonoBehaviour
{
    public GameObject emptyBin, fullBin, fullCell, emptyCell, leftHand, rightHand;
    public InputActionReference a_Button = null, lGrip = null, rGrip = null;
    XRRayInteractor leftRay, rightRay;
    GameObject currCell = null;

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

        // if currcell is not being grabbed, set it to null
        if(currCell != null && currCell.GetComponent<XRGrabInteractable>().isSelected == false){
            currCell = null;
        }// end grab reset

        // Hit object comparisons
        // Right Hand Spawns
        if(rightHitObject.tag == "FullBin" && rGrip.action.triggered && currCell == null){
            // Spawn a new fullCell at the point where the raycast hits the fullbin
            currCell = Instantiate(fullCell, rightInfo.point, Quaternion.identity);  
        }else if(rightHitObject.tag == "EmptyBin" && rGrip.action.triggered && currCell == null){
            // Spawn a new emptyCell at the point where the raycast hits the emptybin
            currCell = Instantiate(emptyCell, rightInfo.point, Quaternion.identity);
        }
    
        // Left Hand Spawns
        if(leftHitObject.tag == "FullBin" && lGrip.action.triggered && currCell == null){
            // Spawn a new fullCell at the point where the raycast hits the fullbin
            currCell = Instantiate(fullCell, leftInfo.point, Quaternion.identity);
        }else if(leftHitObject.tag == "EmptyBin" && lGrip.action.triggered && currCell == null){
            // Spawn a new emptyCell at the point where the raycast hits the emptybin
            currCell = Instantiate(emptyCell, leftInfo.point, Quaternion.identity);
        }
    }
}
