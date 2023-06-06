using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FractionDisplay : MonoBehaviour
{

    
    public Material incorrectMaterial, correctMaterial, defaultMaterial;
    public GameObject fractionDisplay;
    // Start is called before the first frame update
    void Start()
    {
        // Default
        GameObject cubeRef = fractionDisplay.transform.GetChild(0).gameObject;
        
        MeshRenderer renderer = cubeRef.GetComponent<MeshRenderer>();
    }

    /* Update is called once per frame
    void Update()
    {
        
    }
*/
    // Called externally to set the display
    public void setDisplay(int[] fraction)
    {
        // Update the current fraction
        this.GetComponentInChildren<TMP_Text>().text = fraction[0].ToString() + "/" + fraction[1].ToString();
    }

     public void correctAnswer(){

        StartCoroutine(SwapMaterialsAfterDelay(1.0f, correctMaterial));
    }
    public void incorrectAnswer(){

        StartCoroutine(SwapMaterialsAfterDelay(1.0f, incorrectMaterial));
    }

      private IEnumerator SwapMaterialsAfterDelay(float delay, Material material)
    {
        GameObject cubeRef = fractionDisplay.transform.GetChild(0).gameObject;
        MeshRenderer renderer = cubeRef.GetComponent<MeshRenderer>();
        renderer.material = material;
        yield return new WaitForSeconds(delay);

        // Return to normal state
        renderer.material = defaultMaterial;
    
        
        
    }


}
