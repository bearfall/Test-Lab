using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace bearfall
{
    public class MousePlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        private TestGameManager1 testGameManager1;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            testGameManager1 = GameObject.Find("Manager").GetComponent<TestGameManager1>();


        }
        private void Start()
        {
            MouseManager.Instance.OnMouseClicked += MoveToTarget;
        }
        public void MoveToTarget(Vector3Int target)
        {

            agent.destination = target;
        }

        private void Update()
        {
            if (agent.velocity.magnitude == 0)
            {
                // ����b���ʤ�
                GetComponent<Collider>().enabled = true;
            }
            else
            {
                // ���⥼�b����
                GetComponent<Collider>().enabled = false;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BattleArea"))
            {
                this.GetComponent<NavMeshAgent>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
                testGameManager1.currentArea = TestGameManager1.AreaType.TurnBasedCombat;
                

                print("�i�J�԰��ϰ�");
            }
        }
        
    }
}
