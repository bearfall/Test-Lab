using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace bearfall
{


    public class EnemySpawnBase : MonoBehaviour
    {
        [SerializeField, Header("敵人欲置物")]
        private GameObject prefabBullet;
        [SerializeField, Header("敵人生成點")]
        private Transform pointSpawn;
        [SerializeField, Header("角色父物件")]
        private Transform charactorParent;

        public int enemyAmount = 1;

        //生成子彈
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
