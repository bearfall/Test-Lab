using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace bearfall
{


    public class EnemySpawnBase : MonoBehaviour
    {
        
        [SerializeField, Header("�ĤH�ͦ��I")]
        private Transform pointSpawn;
        [SerializeField, Header("���������")]
        private Transform charactorParent;

        public List<GameObject> enemyList = new List<GameObject>();
        public bool allEnemyReady = false;
        //public int enemyAmount = 4;

        //�ͦ��l�u
        public IEnumerator SpawnEnemy()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                Instantiate(enemyList[i], pointSpawn.position, pointSpawn.rotation, charactorParent);
                yield return new WaitForSeconds(0.3f);
            }
            allEnemyReady = true;
        }
    }
}
