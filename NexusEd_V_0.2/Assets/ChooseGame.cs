using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChooseGame : MonoBehaviour
{
    XRRayInteractor leftRay, rightRay;
    public GameObject rightHand, leftHand;
    public GameObject[] gameButtons = new GameObject[4];
    BoxCollider[] colliders = new BoxCollider[4];
    public GameObject[] pressed = new GameObject[4];
    public GameObject[] unPressed = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        leftRay = leftHand.GetComponent<XRRayInteractor>();
        rightRay = rightHand.GetComponent<XRRayInteractor>();

        // Get the box collider component from their parent gameobject
        for(int i = 0; i < gameButtons.Length; i++)
        {
            colliders[i] = gameButtons[i].GetComponent<BoxCollider>();

            unPressed[i] = gameButtons[i].transform.GetChild(0).gameObject;
            pressed[i] = gameButtons[i].transform.GetChild(1).gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < gameButtons.Length; i++)
        {
            activityManagement(rightRay, colliders[i], unPressed[i], pressed[i]);
            activityManagement(leftRay, colliders[i], unPressed[i], pressed[i]);

        }

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
