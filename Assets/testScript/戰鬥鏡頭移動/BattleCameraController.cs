using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraController : MonoBehaviour
{
    public Transform player1; // 雙方角色1
    public Transform player2; // 雙方角色2
    public float smoothSpeed = 5f; // 鏡頭移動的平滑速度
    public float minDistance = 5f; // 鏡頭與角色的最小距離
    public bool needToMoveCamera = false;
    private Transform target; // 目標位置
    public float additionalRotationY = 90f;

    private void Start()
    {
        // 初始化目標位置
        //InitializeCameraTarget();
        print(transform.forward);
    }

    private void Update()
    {
        
        // 如果需要移動鏡頭，則執行移動
        if (needToMoveCamera)
        {
            
            MoveCameraToTarget();
        }
    }

    // 初始化目標位置的方法
    private void InitializeCameraTarget()
    {
        // 計算兩個角色之間的中心點作為目標位置
        Vector3 center = (player1.position + player2.position) / 2f;
        target = new GameObject().transform;
        target.position = center;
    }

    // 移動鏡頭到目標位置的方法
    private void MoveCameraToTarget()
    {
        InitializeCameraTarget();
        // 計算目標位置
        Vector3 center = (player1.position + player2.position) / 2f;
        float distance = Vector3.Distance(player1.position, player2.position) / 2f;
        distance = Mathf.Max(distance, minDistance);
        target.position = center - transform.forward * distance;
       // print(target.position);
        // 平滑移動鏡頭到目標位置
        transform.position = Vector3.Lerp(transform.position, target.position, smoothSpeed * Time.deltaTime);


        
        
        // 計算攝影機要面向的方向
        Vector3 lookDirection = player1.position - player2.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        //print(targetRotation);
        targetRotation *= Quaternion.Euler(0f, additionalRotationY, 0f);
        // 平滑旋轉攝影機到目標方向
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation  , smoothSpeed * Time.deltaTime);
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = 0;
        transform.eulerAngles = currentRotation;


    }
    // 在需要的時候呼叫此方法，例如在特定事件觸發時
        public void StartCameraMovement(Transform Player1, Transform Player2)
    {
        player1 = Player1;
        player2 = Player2;
        needToMoveCamera = true;
    }

    // 在不需要移動鏡頭時，可以呼叫此方法停止移動
    public void StopCameraMovement()
    {
        needToMoveCamera = false;
    }
}
