using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ChooseGame : MonoBehaviour
{
    XRRayInteractor leftRay, rightRay;
    public GameObject rightHand, leftHand;
    public GameObject[] gameButtons = new GameObject[4];
    BoxCollider[] colliders = new BoxCollider[4];
    public GameObject[] pressed = new GameObject[4];
    public GameObject[] unPressed = new GameObject[4];
    public GameObject backButton, mainMenu;
    BoxCollider backCollider;
    public InputActionReference a_Button = null;
    GameObject backPressed, backUnPressed;



    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        leftRay = leftHand.GetComponent<XRRayInteractor>();
        rightRay = rightHand.GetComponent<XRRayInteractor>();
        backCollider = backButton.GetComponent<BoxCollider>();
        backPressed = backButton.transform.GetChild(1).gameObject;
        backUnPressed = backButton.transform.GetChild(0).gameObject;

        Debug.LogWarning("Before For Loop");
        // Get the box collider component from their parent gameobject
        for (int i = 0; i < gameButtons.Length; i++)
        {
            gameButtons[i] = this.transform.GetChild(i).gameObject;
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
            activityManagement(rightRay, leftRay, colliders[i], unPressed[i], pressed[i]);
        }

        activityManagement(rightRay, leftRay, backCollider, backUnPressed, backPressed);


        if (backPressed.activeSelf && a_Button.action.triggered)
        {
            this.transform.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
        }
    }

    void activityManagement(XRRayInteractor thisRay, XRRayInteractor thisOtherRay, BoxCollider thisBox, GameObject bNP, GameObject bP)
    {
        // Get the ray origin and direction from the XRRayInteractor
        Vector3 rayOrigin = thisRay.transform.position;
        Vector3 rayDirection = thisRay.transform.forward;

        Vector3 ray2Origin = thisOtherRay.transform.position;
        Vector3 ray2Direction = thisOtherRay.transform.forward;

        // Create a ray based on the origin and direction
        Ray ray1 = new Ray(rayOrigin, rayDirection);
        Ray ray2 = new Ray(ray2Origin, ray2Direction);


        // Perform the intersection check
        bool isIntersecting1 = thisBox.Raycast(ray1, out RaycastHit hitInfo, float.MaxValue);
        bool isIntersecting2 = thisBox.Raycast(ray2, out RaycastHit hitInfo2, float.MaxValue);



        bNP.SetActive(!(isIntersecting1 || isIntersecting2));
        bP.SetActive(isIntersecting1 || isIntersecting2);
    }

}

