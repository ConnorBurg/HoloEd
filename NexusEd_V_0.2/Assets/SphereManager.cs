using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public Material holoGreen;
    public Material emissive;

    public GameObject mySphere;

    public GameObject myEarth;
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
        if(other.tag == "EarthCollider")
        {
            mySphere.GetComponent<MeshRenderer>().material = emissive;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "EarthCollider")
        {
            mySphere.GetComponent<MeshRenderer>().material = holoGreen;
        }
    }
}
