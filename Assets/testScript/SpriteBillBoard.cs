using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillBoard : MonoBehaviour
{
    public bool isBillBoard = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBillBoard)
        {


            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
    }
}
