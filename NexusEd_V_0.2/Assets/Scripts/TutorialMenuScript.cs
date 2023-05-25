using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialMenuScript : MonoBehaviour
{

    XRRayInteractor leftRay, rightRay;
    BoxCollider playBox, tutBox;
    public GameObject redPressed, redNotPressed, greenPressed, greenNotPressed, playButton, tutButton, rightHand, leftHand;

    // Start is called before the first frame update
    void Start()
    {
        leftRay = leftHand.GetComponent<XRRayInteractor>();
        rightRay = rightHand.GetComponent<XRRayInteractor>();
        // Get the box collider component from their parent gameobject
        playBox = playButton.GetComponent<BoxCollider>();
        tutBox = tutButton.GetComponent<BoxCollider>();

    }
    

    // Update is called once per frame
    void Update()
    {

        activityManagement(rightRay, playBox, greenNotPressed, greenPressed);
        activityManagement(leftRay, playBox, greenNotPressed, greenPressed);
        activityManagement(rightRay, tutBox, redNotPressed, redPressed);
        activityManagement(leftRay, tutBox, redNotPressed, redPressed);



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
