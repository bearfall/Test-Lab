using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPath : MonoBehaviour
{
    
    public GameObject chessBox;
    public Transform playerTransform;
    public List<Vector3> AttakeVector3 = new List<Vector3>();
    public static int AttakeIndex = 0;
    public  Vector3 AttakePosition;
    public Transform player;

    public Button attakeButton;



    public void attakePath()
    {
        AttakePosition = this.transform.position + new Vector3(0, 0, -1);
        AttakePathCount();
        AttakePosition = this.transform.position + new Vector3(-1, 0, 0);
        AttakePathCount();
        AttakePosition = this.transform.position + new Vector3(1, 0, 0);
        AttakePathCount();
        AttakePosition = this.transform.position + new Vector3(0, 0, 1);
        AttakePathCount();
    }

    void AttakePathCount()
    {
        Instantiate(chessBox, new Vector3(AttakePosition.x, 0.01f, AttakePosition.z), chessBox.transform.rotation, player);
        AttakeVector3.Insert(AttakeIndex, AttakePosition);
        
        AttakeIndex++;
    }
}
