using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    public bool isTransparent = false;
    public Material originMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UndoMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = originMaterial;



    }


}
