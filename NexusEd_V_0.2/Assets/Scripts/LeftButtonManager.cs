using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButtonManager : MonoBehaviour
{
    public GameObject tutorial;
    TutorialManager tutorialManager;
    // Start is called before the first frame update
    void Start()
    {
        // Get Script references
        tutorialManager = tutorial.GetComponent<TutorialManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When the button is pressed, set the mode to 1
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Baton") {
            tutorialManager.setMode(1);
        }
    }
}
