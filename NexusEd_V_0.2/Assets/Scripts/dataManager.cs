using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataManager : MonoBehaviour
{
    public int gamesCompleted = 0;
    public static dataManager myInstance;
    public Vector3 spawnLocation;
    public Quaternion spawnRotation;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if(myInstance == null)
        {
            myInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
