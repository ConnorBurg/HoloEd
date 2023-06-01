using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutCharge : MonoBehaviour
{
    public GameObject charge, tutorial;

    TutorialManager tutManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Called when the parent gameobject is enabled
    public void onStart(){
        tutManager = tutorial.GetComponent<TutorialManager>();
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Called when this object collides with another object
    void OnTriggerEnter(Collider other) {
        // Check if the object that collided with this object is the baton
        if(other.gameObject.transform.parent.name == "Baton"){
            // If so, call the hitCharge function in the TutorialManager script
            tutManager.hitCharge();
            this.gameObject.SetActive(false);

        }        
    }
}
