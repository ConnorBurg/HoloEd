using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FractionDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Default
        this.GetComponentInChildren<TMP_Text>().text = "0";
    }

    /* Update is called once per frame
    void Update()
    {
        
    }
*/
    // Called externally to set the display
    public void setDisplay(int[] fraction)
    {
        this.GetComponentInChildren<TMP_Text>().text = fraction[0].ToString() + "/" + fraction[1].ToString();
    }
}
