using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAdd : MonoBehaviour
{
    public GameObject ManagerObject;
    GameManager gM_Ref;
    // Start is called before the first frame update
    void Start()
    {
        gM_Ref = ManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "XR Controller Left" || other.gameObject.name == "XR Controller Right")
        {
            gM_Ref.newFullCell();
        }
    }
}