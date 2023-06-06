using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class ShowDiceNumber : MonoBehaviour
{

    public VisualEffect VFXGraph;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoShowDiceNumber()
    {
        VFXGraph.Play();

    }
}
