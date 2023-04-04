using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject eCell, fCell, fractionZone;
    GameObject tmpF, tmpE;
    public InputActionReference a_Button = null, lGrip = null, b_button = null, x_Button = null;
    List<GameObject> cells = new List<GameObject>(), fullCells = new List<GameObject>(), emptyCells = new List<GameObject>();
    int[] tmpFrac = new int[2];
    int score = 0;
    bool canLeave = false;
    public GameObject fractionDisplay;
    FractionDisplay displayRef; 
    public GameObject scoreboardDisplay;
    Scoreboard scorebordRef;
    public GameObject tutorialDisplay;
    bool tutActive = true;
    public BoxCollider doorTrigger;

    // Start is called before the first frame update
    void Start(){
        displayRef = fractionDisplay.GetComponent<FractionDisplay>();
        scorebordRef = scoreboardDisplay.GetComponent<Scoreboard>();

        fractionValueGen();

    }

    // Update is called once per frame
    void Update(){

        if (x_Button.action.triggered) {
            tutActive = !tutActive;
            tutorialDisplay.SetActive(tutActive);
        }

        if (a_Button.action.triggered) {
            checkFraction();
        }
        if(b_button.action.triggered){
            clearFraction();
        }// end of clear
    }// end of update

    public void newFullCell(){
        // instantiate cell at the fraction zone
        GameObject tmp = Instantiate(fCell, fractionZone.transform.position, fractionZone.transform.rotation);
        fullCells.Add(tmp); // Add the cell to the full cells list.
        tmp.transform.position = fractionZone.transform.position;
        groupCells();
    }

    public void newEmptyCell(){
        // instantiate cell at the fraction zone
        GameObject tmp = Instantiate(eCell, fractionZone.transform.position, fractionZone.transform.rotation); // Instantiate the new cell
        emptyCells.Add(tmp); // Add the cell to the empty cell list
        tmp.transform.position = fractionZone.transform.position; // set transform
        groupCells(); // Regroup the cells
    }

    public void removeFullCell()
    {
        if (fullCells.Count > 0){// Check for elements
            GameObject cellToRemove = fullCells[fullCells.Count - 1];
            fullCells.RemoveAt(fullCells.Count - 1); // Remove the last element
            Destroy(cellToRemove);
            groupCells(); // Regroup the cells
        }
    }

    public void removeEmptyCell() {
        if (emptyCells.Count > 0) {// Check for elements 
            GameObject cellToRemove = emptyCells[emptyCells.Count - 1];
            emptyCells.RemoveAt(emptyCells.Count - 1); // Remove last element
            Destroy(cellToRemove);
            groupCells(); // Regroup the cells
        }
    }

    private void groupCells() {
        cells.Clear();

        for (int i = 0; i < emptyCells.Count; i++) {
            cells.Add(emptyCells[i]);     
        }

        for (int i = 0; i < fullCells.Count; i++) {
            cells.Add(fullCells[i]);
        }

        if (cells.Count > 0) { 
        //Organize the cells so they are all visible
            for(int i = 0; i < cells.Count; i++){
                if(i % 2 == 0)
                    cells[i].transform.position = new Vector3(fractionZone.transform.position.x - (.25f * i), fractionZone.transform.position.y, fractionZone.transform.position.z);
                else
                    cells[i].transform.position = new Vector3(fractionZone.transform.position.x + (.25f * i), fractionZone.transform.position.y, fractionZone.transform.position.z);
            }
        }
    }

    public void checkRefs() {
        Debug.Log("Refs Called");
    }

    void fractionValueGen(){

        int[] denOps = { 4, 6, 8, 12 };
        int[] numOps = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        
        tmpFrac[1] = denOps[Random.Range(0, 3)]; // denominator
        tmpFrac[0] = numOps[Random.Range(0, tmpFrac[1] - 1)]; // numerator

        displayRef.setDisplay(tmpFrac);
    }

    void checkFraction() {
        int numerator = fullCells.Count;
        int denom = fullCells.Count + emptyCells.Count;

        Debug.Log("NUM: " + numerator + ", " + "DEN: " + denom);


        if ((numerator == tmpFrac[0]) && (denom == tmpFrac[1]))
        { // Correct!
            clearFraction();
            score++;
            scorebordRef.setDisplay(score);
            if (score == 5)
            {
                scorebordRef.winner();
                canLeave = true;
                doorTrigger.enabled = true;
            }
            else
            {
                fractionValueGen();
            }
        }
        else {
            fractionValueGen();
            clearFraction();
        }
    }

    void clearFraction() {

        //clear the cells
        for (int i = 0; i < cells.Count; i++)
        {
            GameObject cellToDestory = cells[i];
            Destroy(cellToDestory);
        }

        // clear the lists
        fullCells.Clear();
        emptyCells.Clear();
        cells.Clear();


    }
}


