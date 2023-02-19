using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CharacterStats characterStats;


    private void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
    }
}
