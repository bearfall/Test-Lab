using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTransparent : MonoBehaviour
{
    // public Material stoneMaterial; // 半透明材质
    //public MeshRenderer stoneMesh;
    private BuildingManager buildingManager;
    private void Start()
    {
        buildingManager = GameObject.Find("Manager").GetComponent<BuildingManager>();
    }


    private void Update()
    {
        // 从角色位置发射射线
        Ray ray = new Ray(transform.position, Camera.main.transform.position - transform.position);
        //print(Camera.main.transform.position);
        RaycastHit hitInfo;
        RaycastHit[] hits;
        // 检测射线与所有碰撞物体的交点
        // 迭代每个碰撞到的物体

        if (Physics.Raycast(ray, out hitInfo)) 
        { 
                // 检查碰撞到的物体是否是建筑物
            if (hitInfo.collider.gameObject.CompareTag("Building"))
            {
                hits = Physics.RaycastAll(ray,LayerMask.NameToLayer("Building"));

                buildingManager.ClearBuilding();
                Debug.DrawLine(ray.origin, hitInfo.point);
                print("被擋住了");


                foreach (var hit in hits)
                {


                    // 获取建筑物的渲染器组件
                    Material stoneMaterial; // 半透明材质
                    MeshRenderer stoneMeshRender;



                    stoneMeshRender = hit.collider.gameObject.GetComponent<MeshRenderer>();
                    stoneMaterial = stoneMeshRender.material;

                    Material newMaterial = new Material(stoneMaterial);
                    stoneMeshRender.material = newMaterial;


                    newMaterial.SetFloat("_Alpha", 0.5f);

                  //  bool isTransparent = hit.collider.gameObject.GetComponent<BuildingInfo>().isTransparent;
                   // isTransparent = true;
                }
            }
            else
            {
                buildingManager.ClearBuilding();
            }
        }

        
        
        
    }
}

