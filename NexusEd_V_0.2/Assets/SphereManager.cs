using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public Material holoGreen;
    public Material emissive;

    public GameObject mySphere;

    private bool colStatus = false;
    List<string> collidingNations = new List<string>();

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
        //Export the thing we're colliding with to the list
        collidingNations.Add(other.gameObject.tag);
        //Light functionality if it's the earth we're colliding with for UI
        if (other.tag == "EarthCollider")
        {
            colStatus = true;
            mySphere.GetComponent<MeshRenderer>().material = emissive;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Get rid of the game object we're no longer colliding with
        collidingNations.Remove(other.gameObject.tag);
        //Light functionality if it's the earth we're colliding with for UI
        if(other.tag == "EarthCollider")
        {
            colStatus = false;
            mySphere.GetComponent<MeshRenderer>().material = holoGreen;
        }
    }

    public bool GetColStatus()
    {
        return colStatus;
    }

    public List<string> getCurrentNations()
    {
        return collidingNations;
    }
}
