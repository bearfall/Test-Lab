using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointFollowV3 : MonoBehaviour
{
    [Header("掛上路徑父物件")]
    public UnityStandardAssets.Utility.WaypointCircuit circuit;
    [Header("追擊目標索引")]
    public int currentWP = 0;

    [Header("移動速度"), Range(0, 10)]
    public float speed = 1.0f;

    [Header("保持距離"), Range(0, 3)]
    public float closePoint = 0.5f;

    [Header("旋轉速度"), Range(0, 5)]
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
        //如果場景沒有任何 waypoint 則回傳 0 (不動作)
        if (circuit.Waypoints.Length == 0) return;

        //面像與目標的相關設定
        Vector3 lookAtGoal = new Vector3(circuit.Waypoints[currentWP].transform.position.x,
                                        this.transform.position.y,
                                        circuit.Waypoints[currentWP].transform.position.z);

        Vector3 direction = lookAtGoal - this.transform.position;
        Vector3 directionVertical = lookAtGoal - circuit.Waypoints[currentWP].transform.position;
        Debug.DrawRay(this.transform.position, direction, Color.green);
        Debug.DrawRay(circuit.Waypoints[currentWP].transform.position, directionVertical, Color.yellow);

        //使用 Slerp 方法，使移動物件，旋轉朝向目標物
        
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);
        
        // 追擊目標時的 waypoint 陣列索引號變更&歸零(重複)
        if (direction.magnitude < closePoint)
        {
            currentWP++;
            if (currentWP >= circuit.Waypoints.Length)
            {
                currentWP = 0;
            }
        }

        this.transform.Translate(0, 0, speed * Time.deltaTime); //物件向前移動方式
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


