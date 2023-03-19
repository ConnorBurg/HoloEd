using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject fCellBucket, eCellBucket, eCell, fCell, fractionZone, tmpZone;
    GameObject tmpF, tmpE;
    int tmpI;
    public ActionBasedController rightHand, leftHand;
    public InputDevice targetDeviceR, targetDeviceL;
    public XRRayInteractor rRayInteractor, lRayInteractor; 
    public XRGrabInteractable fCellGrabbable, eCellGrabbable, genericGrab;
    bool rGripBool, lGripBool;
    Cell cellCurr;
    List<Cell> cells = new List<Cell>();
    List<Cell> cellsInZone = new List<Cell>(), cellsTmpZone = new List<Cell>();
    
    // Start is called before the first frame update
    void Start(){
        TryInit();   
    }

    // Update is called once per frame
    void Update(){

        if(!targetDeviceR.isValid || !targetDeviceL.isValid)
            TryInit();
            

        targetDeviceR.TryGetFeatureValue(CommonUsages.gripButton, out rGripBool);
        targetDeviceL.TryGetFeatureValue(CommonUsages.gripButton, out lGripBool);
        
        


        if(rRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit resR)){ // Check if rayCast is hitting anything
            Debug.Log("resR.transform.gameObject = " + resR.transform.gameObject.ToString()); // Debug
            Debug.Log("cells.Count = " + cells.Count.ToString()); // Debug
            
            // If at least one cell exists.
            if(cells.Count != 0){
                cellCurr = cells[findCellRef(getPointedObj(resR))];
                Debug.Log("cellCurr = " + cellCurr.ToString());
            }
            

            if(cells.Count == cellsInZone.Count || cellsTmpZone.Count == 0){// only allow new intance if no extra cells in tmpZone
                if(resR.transform.gameObject.Equals(fCellBucket) && rGripBool){ // Check if pointing at fCellBucket && right grip
                    if(cells.Count == 0 || (!eCellGrabbable.isSelected && !fCellGrabbable.isSelected)){ // Grabbable component checks if nothing is being actively grabbed by user
                        tmpF = newFullCell(rightHand, resR);
                
                    }   
                }else if(resR.transform.gameObject.Equals(eCellBucket) && rGripBool){ // See above - Altered for E Bucket
                    if(cells.Count == 0 || (!eCellGrabbable.isSelected && !fCellGrabbable.isSelected)){
                        tmpE = newEmptyCell(rightHand, resR);
                        
                    }
                    
                }/*else if(findZoneCellRef(resR.transform.gameObject) != -1){
                    Debug.Log("This c   ell is indexed: " + findZoneCellRef(resR.transform.gameObject));
                    if(rGripBool)
                        unGroup(cellsInZone[findZoneCellRef(resR.transform.gameObject)]);
                }*/
            }
        }

        if(lRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit resL)){ // See above - Altered for Left Hand
            if(cells.Count == cellsInZone.Count){
                if(resL.transform.gameObject.Equals(fCellBucket) && lGripBool){
                    if(cells.Count == 0 || (!eCellGrabbable.isSelected && !fCellGrabbable.isSelected)){
                        tmpF = newFullCell(leftHand, resL);
                    }

                }else if(resL.transform.gameObject.Equals(eCellBucket) && lGripBool){
                    if(cells.Count == 0 || (!eCellGrabbable.isSelected && !fCellGrabbable.isSelected)){
                        tmpE = newEmptyCell(leftHand, resL);
                    } 
                }
            } 
        }


        /*
        if(cellsTmpZone.Count != 0){
            //cellCurr = cellsTmpZone[0];
            if(cells.Count != 0 && zoneCheck(cellCurr)){
                group(cellCurr); // Group the cell 
                Debug.Log("Dropped In Zone"); // Debug
            }   
        }*/
    }

    private GameObject getPointedObj(RaycastHit resR)
    {
        GameObject currObj = resR.transform.gameObject;
        Cell _cell;
        
        if(findCellRef(currObj) != -1){ // If the pointed at cell exisits in the current world. It will, just need to avoid issues when pointing at other things. 
            _cell = cells[findCellRef(currObj)];
            if(_cell.getValue() == 0){
                eCellGrabbable = _cell.getCell().GetComponent<XRGrabInteractable>();
            }else{
                fCellGrabbable = _cell.getCell().GetComponent<XRGrabInteractable>();
            }
        }

        return currObj; 
    }

    private int findZoneCellRef(GameObject cellRef)
    {
        for(int i = 0; i < cellsInZone.Count; i++){
            if(cellsInZone[i].getCell().Equals(cellRef))
                return i;

        }
        return -1;
    }

    private int findCellRef(GameObject cellRef)
    {
        for(int i = 0; i < cellsInZone.Count; i++){
            if(cells[i].getCell().Equals(cellRef))
                return i;

        }
        return -1;
    }

    private bool zoneCheck(Cell cell){
       

        if(!cell.getCell().GetComponent<XRGrabInteractable>().isSelected)
            if(isInZone(cell))
                if(!rGripBool && !lGripBool)
                    if(cell.getStatus() == 0)
                        return true;

        return false;
    }

    private bool isInZone(Cell cell){
        GameObject _cell = cell.getCell();
        float xUpper = fractionZone.transform.position.x + 2f;
        float xLower = fractionZone.transform.position.x - 2f;

        float zUpper = fractionZone.transform.position.z + 2.5f;
        float zLower = fractionZone.transform.position.z - 2.5f;
      
        if(xUpper > _cell.transform.position.x && xLower < _cell.transform.position.x)
            if(zUpper > _cell.transform.position.z && zLower < _cell.transform.position.z)
                return true;
             
        return false;
    }

    private void group(Cell cell){    
        GameObject _cell = cell.getCell();

        cellsInZone.Add(cell);
        cellsInZone[cellsInZone.Count - 1].getCell().GetComponent<XRGrabInteractable>().enabled = false;
        cell.setStatus(1);
        cellsTmpZone.Remove(cell);

        // Make added cell child of fractionZone
        _cell.transform.parent = fractionZone.transform;
        for(int i = 0; i < cellsInZone.Count; i++){
            cellsInZone[i].getCell().transform.position = new Vector3(fractionZone.transform.position.x + (.25f * i), fractionZone.transform.position.y, fractionZone.transform.position.z);
            cellsInZone[i].getCell().transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        
        
        Debug.Log("Cell added! New Size: " + cellsInZone.Count);
    }

    private void unGroup(Cell cell){// Passed cell will be the one pointed at

            Debug.Log("Removing: " + cell.ToString());

            if(cell.getValue() == 1)
                tmpF = cell.getCell();
            else
                tmpE = cell.getCell();

            cell.getCell().transform.parent = tmpZone.transform;
            cell.getCell().GetComponent<XRGrabInteractable>().enabled = true;
            cellsInZone.Remove(cell);
            cellsTmpZone.Add(cell);
            cell.setStatus(0);
            //redraw();
            Debug.Log("Cell Removed! New Size: " + cellsInZone.Count);
        
    }

    private void redraw(){
        for(int i = 0; i < cellsInZone.Count; i++){
            cellsInZone[i].getCell().transform.position = new Vector3(fractionZone.transform.position.x + (.125f * i), fractionZone.transform.position.y, fractionZone.transform.position.z);
        }
    }

    public void clearZone(){
        for(int i = 0; i < cellsInZone.Count; i++){
            unGroup(cellsInZone[i]);
        }
    }

    private void TryInit(){

        List<InputDevice> devices = new List<InputDevice>();
        //InputDeviceCharacteristics ControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, devices);

        if(devices.Count > 0){
            //Debug.Log("Number of Devices: " + devices.Count);
            targetDeviceL = devices[0];
            targetDeviceR = devices[1];
            //Debug.Log("targetDeviceL: " + devices[0].ToString() + " targetDeviceR: " + devices[1].ToString());
        }
    }

    private int[] getFraction(){ 
        int sum = 0; 
        int[] fractionOut = new int[2];
        fractionOut[1] = cellsInZone.Count;
    
        for(int i = 0; i < cellsInZone.Count; i++)
            sum += cellsInZone[i].getValue();

        fractionOut[0] = sum;

        return fractionOut;
    }

    private GameObject newFullCell(ActionBasedController hand, RaycastHit res){

             // Grabbable component checks if nothing is being actively grabbed by user
            GameObject tmp = Instantiate(fCell, res.point, hand.transform.rotation);
            cells.Add(new Cell(tmp, 1));
            cellsTmpZone.Add(cells[cells.Count - 1]);
            fCellGrabbable = tmp.GetComponent<XRGrabInteractable>();
            tmp.transform.parent = tmpZone.transform;
            Debug.Log("New Spawn. Global Cells: " + cells.Count);

            return tmp;
        
    }
    private GameObject newEmptyCell(ActionBasedController hand, RaycastHit res){
        
        GameObject tmp = Instantiate(eCell, res.point, hand.transform.rotation);
        cells.Add(new Cell(tmp, 1));
        cellsTmpZone.Add(cells[cells.Count - 1]);
        fCellGrabbable = tmp.GetComponent<XRGrabInteractable>();
        tmp.transform.parent = tmpZone.transform;
        Debug.Log("New Spawn. Global Cells: " + cells.Count);

        return tmp;
    }  
}// end GameManager

public class Cell{
    GameObject _cell;
    int _value, _status;

    public Cell(GameObject cell, int value){
        _cell = cell;
        _value = value;
        _status = 0; // Not in zone   
    }

    public GameObject getCell(){
        return _cell;
    }

    public int getValue(){
        return _value;
    }

    public void setValue(int newVal){
        _value = newVal;
    }

    public int getStatus(){
        return _status;
    }

    public void setStatus(int status){
        _status = status;
    }
} // end cell



