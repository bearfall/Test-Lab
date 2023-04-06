using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColar : MonoBehaviour
{

    public SpriteRenderer sp;


    // Start is called before the first frame update
    void Start()
    {

        sp = GetComponent<SpriteRenderer>();
        sp.color = new Color(0.1f, 0.1f, 0.1f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
