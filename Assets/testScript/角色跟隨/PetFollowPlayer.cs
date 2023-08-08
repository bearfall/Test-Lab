using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollowPlayer : MonoBehaviour
{
    public Transform player; // ���a�� Transform
    public float followDistance = 3.0f; // �d�����H�Z��

    private List<Vector3> playerPath = new List<Vector3>();
    private int currentPathIndex = 0;


    private void Update()
    {
        playerPath.Add(player.position);

        // �p�G���|�C�����I�ƶW�L�O�����Z���A�����̦����I
        if (playerPath.Count > Mathf.RoundToInt(followDistance))
        {
            playerPath.RemoveAt(0);
        }

        // ��s�d����m
        if (currentPathIndex < playerPath.Count)
        {
            Vector3 targetPosition = playerPath[currentPathIndex] + (player.forward * followDistance);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 10);

            // �ˬd�O�_����ؼЦ�m�A�M�Ჾ�ʨ�U�@�Ӹ��|�I
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }
    }
}
