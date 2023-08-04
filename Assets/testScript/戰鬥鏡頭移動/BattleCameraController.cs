using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraController : MonoBehaviour
{
    public Transform player1; // ���訤��1
    public Transform player2; // ���訤��2
    public float smoothSpeed = 5f; // ���Y���ʪ����Ƴt��
    public float minDistance = 5f; // ���Y�P���⪺�̤p�Z��
    public bool needToMoveCamera = false;
    private Transform target; // �ؼЦ�m
    public float additionalRotationY = 90f;

    private void Start()
    {
        // ��l�ƥؼЦ�m
        InitializeCameraTarget();
        print(transform.forward);
    }

    private void Update()
    {
        // �p�G�ݭn�������Y�A�h���沾��
        if (needToMoveCamera)
        {
            MoveCameraToTarget();
        }
    }

    // ��l�ƥؼЦ�m����k
    private void InitializeCameraTarget()
    {
        // �p���Ө��⤧���������I�@���ؼЦ�m
        Vector3 center = (player1.position + player2.position) / 2f;
        target = new GameObject().transform;
        target.position = center;
    }

    // �������Y��ؼЦ�m����k
    private void MoveCameraToTarget()
    {
        // �p��ؼЦ�m
        Vector3 center = (player1.position + player2.position) / 2f;
        float distance = Vector3.Distance(player1.position, player2.position) / 2f;
        distance = Mathf.Max(distance, minDistance);
        target.position = center - transform.forward * distance;
       // print(target.position);
        // ���Ʋ������Y��ؼЦ�m
        transform.position = Vector3.Lerp(transform.position, target.position, smoothSpeed * Time.deltaTime);


        
        
        // �p����v���n���V����V
        Vector3 lookDirection = player1.position - player2.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        //print(targetRotation);
        targetRotation *= Quaternion.Euler(0f, additionalRotationY, 0f);
        // ���Ʊ�����v����ؼФ�V
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation  , smoothSpeed * Time.deltaTime);
        
       
}
    // �b�ݭn���ɭԩI�s����k�A�Ҧp�b�S�w�ƥ�Ĳ�o��
        public void StartCameraMovement()
    {
        needToMoveCamera = true;
    }

    // �b���ݭn�������Y�ɡA�i�H�I�s����k�����
    public void StopCameraMovement()
    {
        needToMoveCamera = false;
    }
}
