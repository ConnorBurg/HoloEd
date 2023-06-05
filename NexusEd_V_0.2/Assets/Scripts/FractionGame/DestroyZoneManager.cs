using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZoneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //if the object is a fraction
        if (other.gameObject.tag == "fullCell" || other.gameObject.tag == "emptyCell")
        {
            Destroy(other.gameObject);
        }
    }
}
