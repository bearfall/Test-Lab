using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace bearfall
{


    public class EnemySpawnBase : MonoBehaviour
    {
        [SerializeField, Header("�ĤH���m��")]
        private GameObject prefabBullet;
        [SerializeField, Header("�ĤH�ͦ��I")]
        private Transform pointSpawn;
        [SerializeField, Header("���������")]
        private Transform charactorParent;

        public int enemyAmount = 1;

        //�ͦ��l�u
        public void SpawnEnemy()
        {
            Instantiate(prefabBullet, pointSpawn.position, pointSpawn.rotation, charactorParent);
            if (enemyAmount > 0)
            {
                enemyAmount--;
            }
            

        }
    }
}
