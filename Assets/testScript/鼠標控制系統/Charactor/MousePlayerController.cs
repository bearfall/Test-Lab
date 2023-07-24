using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace bearfall
{
    public class MousePlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();



        }
        private void Start()
        {
            MouseManager.Instance.OnMouseClicked += MoveToTarget;
        }
        public void MoveToTarget(Vector3 target)
        {
            agent.destination = target;
        }
    }
}
