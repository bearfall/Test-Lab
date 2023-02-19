using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkNpc : MonoBehaviour
{
    private string nameTarget = "lin-je-chi";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
       if (other.name.Contains(nameTarget))
        {
            print("¹ï¸Ü");
        }
    }
}
