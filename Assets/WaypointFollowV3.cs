using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointFollowV3 : MonoBehaviour
{
    [Header("���W���|������")]
    public UnityStandardAssets.Utility.WaypointCircuit circuit;
    [Header("�l���ؼЯ���")]
    public int currentWP = 0;

    [Header("���ʳt��"), Range(0, 10)]
    public float speed = 1.0f;

    [Header("�O���Z��"), Range(0, 3)]
    public float closePoint = 0.5f;

    [Header("����t��"), Range(0, 5)]
    public float rotSpeed = 5;

    public Animator enemyAnim;

    public WaypointFollowV3 animPrograms;

    void Start()
    {
        enemyAnim = GetComponent<Animator>();

        animPrograms = GetComponent<WaypointFollowV3>();
    }


    void Update()
    {
        //�p�G�����S������ waypoint �h�^�� 0 (���ʧ@)
        if (circuit.Waypoints.Length == 0) return;

        //�����P�ؼЪ������]�w
        Vector3 lookAtGoal = new Vector3(circuit.Waypoints[currentWP].transform.position.x,
                                        this.transform.position.y,
                                        circuit.Waypoints[currentWP].transform.position.z);

        Vector3 direction = lookAtGoal - this.transform.position;
        Vector3 directionVertical = lookAtGoal - circuit.Waypoints[currentWP].transform.position;
        Debug.DrawRay(this.transform.position, direction, Color.green);
        Debug.DrawRay(circuit.Waypoints[currentWP].transform.position, directionVertical, Color.yellow);

        //�ϥ� Slerp ��k�A�ϲ��ʪ���A����¦V�ؼЪ�
        
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);
        
        // �l���ؼЮɪ� waypoint �}�C���޸��ܧ�&�k�s(����)
        if (direction.magnitude < closePoint)
        {
            currentWP++;
            if (currentWP >= circuit.Waypoints.Length)
            {
                currentWP = 0;
            }
        }

        this.transform.Translate(0, 0, speed * Time.deltaTime); //����V�e���ʤ覡
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Hit"))
        {
            enemyAnim.SetTrigger("Hit");
        }


        /*
        else if (collider.CompareTag("speed+"))
        {
            animPrograms.speed = 10;
        }
        
        */
    }




}


