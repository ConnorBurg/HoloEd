using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MainMenuManager : MonoBehaviour

{

    XRRayInteractor leftRay, rightRay;
    BoxCollider startBox, optionsBox, levelBox;
    public GameObject greenNotPressed, greenPressed, levNotPressed, levPressed, opNotPressed, opPressed, startButton, optionsButton, levelButton, rightHand, leftHand;
    // Start is called before the first frame update
    void Start()
    {
        leftRay = leftHand.GetComponent<XRRayInteractor>();
        rightRay = rightHand.GetComponent<XRRayInteractor>();

        // Get the box collider component from their parent gameobject
        startBox = startButton.GetComponent<BoxCollider>();
        optionsBox = optionsButton.GetComponent<BoxCollider>();
        levelBox = levelButton.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        activityManagement(rightRay, startBox, greenNotPressed, greenPressed);
        activityManagement(leftRay, startBox, greenNotPressed, greenPressed);


        activityManagement(rightRay, optionsBox, opNotPressed, opPressed);
        activityManagement(leftRay, optionsBox, opNotPressed, opPressed);
        
        activityManagement(rightRay, levelBox, levNotPressed, levPressed);
        activityManagement(leftRay, levelBox, levNotPressed, levPressed);

    }

    void activityManagement(XRRayInteractor thisRay, BoxCollider thisBox, GameObject bNP, GameObject bP)
    {
        // Get the ray origin and direction from the XRRayInteractor
        Vector3 rayOrigin = thisRay.transform.position;
        Vector3 rayDirection = thisRay.transform.forward;

        // Create a ray based on the origin and direction
        Ray ray = new Ray(rayOrigin, rayDirection);

        // Perform the intersection check
        bool isIntersecting = thisBox.Raycast(ray, out RaycastHit hitInfo, float.MaxValue);


        bNP.SetActive(!isIntersecting);
        bP.SetActive(isIntersecting);
    }
}
