using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class MainMenuManager : MonoBehaviour

{

    XRRayInteractor leftRay, rightRay;
    BoxCollider startBox, optionsBox, levelBox;
    public GameObject greenNotPressed, greenPressed, levNotPressed, levPressed, opNotPressed, opPressed, startButton, optionsButton, levelButton, rightHand, leftHand, gameSelectMenu;
    UnityEvent startPressed, optionsPressed, lvlSelPressed;
    public InputActionReference a_Button = null;

    // Start is called before the first frame update
    void Start()
    {
        startPressed = new UnityEvent();
        optionsPressed = new UnityEvent();
        lvlSelPressed = new UnityEvent();
        leftRay = leftHand.GetComponent<XRRayInteractor>();
        rightRay = rightHand.GetComponent<XRRayInteractor>();

        // Get the box collider component from their parent gameobject
        startBox = startButton.GetComponent<BoxCollider>();
        optionsBox = optionsButton.GetComponent<BoxCollider>();
        levelBox = levelButton.GetComponent<BoxCollider>();

        //Add event listeners
        startPressed.AddListener(gameStart);
        optionsPressed.AddListener(optionsMenuTime);
        lvlSelPressed.AddListener(lvlSelMenuTime);
    }

    // Update is called once per frame
    void Update()
    {

        activityManagement(rightRay, leftRay, startBox, greenNotPressed, greenPressed);
        activityManagement(rightRay, leftRay, optionsBox, opNotPressed, opPressed);        
        activityManagement(rightRay, leftRay, levelBox, levNotPressed, levPressed);

        if(greenPressed.activeSelf && a_Button.action.triggered)
        {
            startPressed.Invoke();
        }
        if(opPressed.activeSelf && a_Button.action.triggered)
        {
            optionsPressed.Invoke();
        }
        if (levPressed.activeSelf && a_Button.action.triggered)
        {
            lvlSelPressed.Invoke();
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

    void gameStart()
    {
        //Debug.Log("GAME START PRESSED ME BOYO");
     
    }

    void optionsMenuTime()
    {
        //Debug.Log("SPONGEBOB, YE PRESSED DE OPTIONS BUTTON");

    }

    void lvlSelMenuTime()
    {
        //Debug.Log("YOU PRESSED LEVEL SELECT KRABS!!!");
        gameSelectMenu.SetActive(true);
        this.transform.gameObject.SetActive(false);
    }
}
