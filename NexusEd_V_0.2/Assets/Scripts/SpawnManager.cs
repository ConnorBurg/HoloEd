using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int streak = 0, score = 0;

    private int[] globalFraction = new int[2];

    public GameObject base4, base6, base8, base12, base3, base5, globalFracRef;
    public GameObject[] fractionTypes;

    public Material def, sel;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tmpFrac = Instantiate(fractionTypes[Random.Range(0, fractionTypes.Length)], new Vector3((this.transform.position.x + Random.Range(-2, 2)), this.transform.position.y, this.transform.position.z), this.transform.rotation);

        for(int i = 0; i < 1; i++){
            tmpFrac.transform.GetChild(i).GetComponent<MeshRenderer>().material = sel;
        }

        // Get the initial fraction to be displayed
        globalFraction = fractionValueGen();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int[] fractionValueGen(){
        
        int[] denOps = {4, 6, 8, 12};
        int[] numOps = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
        int[] tmpFrac = new int[2];

        tmpFrac[1] = denOps[Random.Range(0,3)]; // denominator
        tmpFrac[0] = numOps[Random.Range(0, tmpFrac[1] - 1)]; // numerator

        return tmpFrac; 
    }

    public class fraction{

        GameObject fractionFab;
        int[] value = new int[2];
        // constructor
        public fraction(GameObject prefab, int[] value){
            fractionFab = prefab;
            this.value = value;
        }


        bool compareToGlobal(int[] gValue){

            if(gValue[0] == value[0] && gValue[1] == value[1]){
                return true;
            }

            return false;
        }






    }
}
