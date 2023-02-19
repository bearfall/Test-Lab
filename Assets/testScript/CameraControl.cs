using UnityEngine;
using System.Collections;

//=================================================================
//	用於攝影機的拉近拉遠,以及跟隨玩家
//=================================================================


public class CameraControl : MonoBehaviour {


	public GameObject player;  //建立一個GameObject,可放置一個物件
	private Vector3 offset;  //建立一個Vector3的物件

	public float minFov;	//限定Fov的最小值
	public float maxFov;	//限定Fov的最大值
	private float fov = 0f;	//儲存當前的fov


//-------------------------------------------------------------------------------------------

	void Start () {
		
		offset = transform.position;  //令offset = 攜帶此Script的物件之起始座標
		
	}
	
//-------------------------------------------------------------------------------------------

	void LateUpdate () {
		
		transform.position = player.transform.position + offset;  //令攜帶此Script的物件之座標，和Player保持起始距離
		
	}

//-------------------------------------------------------------------------------------------

	void Update()
	{
		if(Path.camera == false)	//選擇"指令(移動..攻擊..)"以前,將攝影機拉近
		{
			for(int i = 0; i < 1000; i++)	//i值亂設的
			{
				fov = Camera.main.fieldOfView;	//令fov = 攝影機當前視野
				fov = fov - 0.2f * Time.deltaTime;	//令視野每秒縮減0.2f
				fov = Mathf.Clamp(fov, minFov, maxFov);	//限制視野的最大最小值
				Camera.main.fieldOfView = fov;	//將變更後的值,傳回給攝影機
			}
		}


		if(Path.camera == true)	//選擇"指令(移動..攻擊..)"之後,將攝影機拉近
		{
			for(int i = 0; i < 1000; i++)	//i值亂設的
			{
				fov = Camera.main.fieldOfView;
				fov = fov + 0.2f * Time.deltaTime;
				fov = Mathf.Clamp(fov, minFov, maxFov);
				Camera.main.fieldOfView = fov;
			}
		}

	}

//-----------------------------------------------------------------------------------------------

}
