using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject eCell, fCell, fractionZone;
    GameObject tmpF, tmpE;
    public InputActionReference rGrip = null, lGrip = null, b_button = null;
    List<GameObject> cells = new List<GameObject>();
    List<GameObject> fullCells = new List<GameObject>(), emptyCells = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start(){    
    }

    // Update is called once per frame
    void Update(){
        
        // if the right grip is pressed, instantiate a new cell, add it to the fullcells list, and then add it to the fraction zone
        if(rGrip.action.triggered && cells.Count < 11){
            tmpF = newFullCell(); // We have our new cell that's spawned in the fraction zone.
            cells.Add(tmpF); // Add the cell to the cells list.
            fullCells.Add(tmpF); // Add the cell to the full cells list.
            tmpF.transform.position = fractionZone.transform.position;
            groupCells();
        }// end of spawn full cell

        if(lGrip.action.triggered && cells.Count < 11){
            tmpE = newEmptyCell();
            cells.Add(tmpE);
            emptyCells.Add(tmpE);
            tmpE.transform.position = fractionZone.transform.position;
            groupCells();
        }// end of spawn empty cell

        if(b_button.action.triggered){
            //clear the cells
            for(int i = 0; i < cells.Count; i++){
                Destroy(cells[i]);
            }
            // clear the lists
            fullCells.Clear();
            emptyCells.Clear();
            cells.Clear();
        }// end of clear
    }// end of update

    private GameObject newFullCell(){

        // instantiate cell at the fraction zone
        GameObject tmp = Instantiate(fCell, fractionZone.transform.position, fractionZone.transform.rotation);
        //GameObject tmp = Instantiate(fCell, res.point, hand.transform.rotation);
        return tmp;
    }
      private GameObject newEmptyCell(){
        // instantiate cell at the fraction zone
        GameObject tmp = Instantiate(eCell, fractionZone.transform.position, fractionZone.transform.rotation);
        //GameObject tmp = Instantiate(fCell, res.point, hand.transform.rotation);
        return tmp;
        }

       private void groupCells(){
       //Organize the cells so they are all visible
        for(int i = 0; i < cells.Count; i++){
            if(i % 2 == 0)
                cells[i].transform.position = new Vector3(fractionZone.transform.position.x - (.25f * i), fractionZone.transform.position.y, fractionZone.transform.position.z);
            
            else
                cells[i].transform.position = new Vector3(fractionZone.transform.position.x + (.25f * i), fractionZone.transform.position.y, fractionZone.transform.position.z);
        }
    }
}


