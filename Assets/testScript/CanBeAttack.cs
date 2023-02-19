using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanBeAttack : MonoBehaviour
{
    public  bool canBeAttack;

    private void Update()
    {
        
    }

    
    
    private void OnTriggerStay(Collider floor)
    {
        if (floor.name.Contains("AttakeChessBox"))
        {
            canBeAttack = true;
        }
        else
        {
            canBeAttack = false;
        }
    }
}
