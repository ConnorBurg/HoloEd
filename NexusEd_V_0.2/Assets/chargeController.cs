using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargeController : MonoBehaviour
{
    ChargeManager scriptRef;
    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the ChargeManager script
        GameObject batonRef = GameObject.Find("Baton");
        scriptRef = batonRef.GetComponent<ChargeManager>();
        // Set the rotation of the charge
        this.transform.eulerAngles = new Vector3(0f, -90f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the charge forward
        this.transform.Translate(Vector3.forward * Time.deltaTime * 5);
        if(this.transform.position.x < 0f){ // If the charge is off the screen call for it to be removed
            scriptRef.removeCharge(this.gameObject);
        }
        
    }


}
