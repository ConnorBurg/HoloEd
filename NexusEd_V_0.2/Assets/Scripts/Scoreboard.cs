using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public GameObject display;
    public Material incorrectMaterial, correctMaterial, defaultMaterial;
    // Start is called before the first frame update
    void Start()
    {
        GameObject cubeRef = display.transform.GetChild(0).gameObject;
        MeshRenderer renderer = cubeRef.GetComponent<MeshRenderer>(); 
        
    }

    /* Update is called once per frame
    void Update()
    {
        
    }
*/
    // Called externally to set the display
    public void tutorialPrompt(int remGreen, int remGray)
    {
        this.GetComponentInChildren<TMP_Text>().text = "Add " + remGreen + " Green cells.\nAdd " + remGray + " Gray cells.";
    }

    public void setDisplay(int score)
    {
        this.GetComponentInChildren<TMP_Text>().text = "Score: " + score;
    }

    public void correctAnswer(){

        StartCoroutine(SwapMaterialsAfterDelay(1.0f, correctMaterial, 2));
    }
    public void incorrectAnswer(){

        StartCoroutine(SwapMaterialsAfterDelay(1.0f, incorrectMaterial, 1));
    }

    public void winner() {

            StartCoroutine(SwapMaterialsAfterDelay(5.0f, correctMaterial, 0));
    }


    private IEnumerator SwapMaterialsAfterDelay(float delay, Material material, int flag)
    {
     
        if(flag == 0){
            this.GetComponentInChildren<TMP_Text>().text = "Engine Restarting... You Win! Difficulty Increased!";
            yield return new WaitForSeconds(delay);
            setDisplay(0);
            
        }else{
            GameObject cubeRef = display.transform.GetChild(0).gameObject;
            Renderer renderer = cubeRef.GetComponent<MeshRenderer>();
            renderer.material = material;
            yield return new WaitForSeconds(delay);
            // Return to normal state
            renderer.material = defaultMaterial;
        }
        
        
    }

    
    
}
