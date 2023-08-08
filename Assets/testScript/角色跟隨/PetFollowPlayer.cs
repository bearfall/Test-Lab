using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollowPlayer : MonoBehaviour
{
    public Transform player; // 玩家的 Transform
    public float followDistance = 3.0f; // 寵物跟隨距離

    private List<Vector3> playerPath = new List<Vector3>();
    private int currentPathIndex = 0;


    private void Update()
    {
        playerPath.Add(player.position);

        // 如果路徑列表中的點數超過保持的距離，移除最早的點
        if (playerPath.Count > Mathf.RoundToInt(followDistance))
        {
            playerPath.RemoveAt(0);
        }

        // 更新寵物位置
        if (currentPathIndex < playerPath.Count)
        {
            Vector3 targetPosition = playerPath[currentPathIndex] + (player.forward * followDistance);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 10);

            // 檢查是否接近目標位置，然後移動到下一個路徑點
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }
    }
}
