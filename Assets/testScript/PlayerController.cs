using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//=================================================================
//	用於平滑移動角色,並於移動完成後,將部份與"移動"有關的參數回歸初值
//=================================================================

public class PlayerController : MonoBehaviour
{


	public int MoveSpeed;   //用來調整移動速度,數值越大越快
	//public Button moveButton;
	//private CharacterStats characterStats;
	//private GameObject target;
	//private bool _canBeAttack;
	


	//aaa[targetChess - 2] = 起始位置的下一個位置,因為aaa[targetChess]是空的,aaa[targetChess-1]是起始位置
	private int i = 2;


    //------------------------------------------------------------------------------------
    private void Awake()
    {
		//characterStats = GetComponent<CharacterStats>();
		
    }

    private void Start()
    {
		//characterStats.MaxHealth = 30;
    }


    void Update()
	{
		if (TestCharacter.chose == true)    //按下滑鼠左鍵,而且判定是點擊了ChessBox
		{
			StartCoroutine(move()); //啟動計數器,用於move()
			TestCharacter.chose = false;    //令滑鼠左鍵失效,防止移動中重複點擊,造成計算錯誤

		}

	}
	/*
    private void OnTriggerstay(Collider enemy)
    {
		target = enemy.gameObject;
		print(target.name);
    }
	*/
    //------------------------------------------------------------------------------------

    public IEnumerator move()
	{
		while (true)
		{

			//計算目標點和現在的座標差(這是一個向量)
			Vector3 distance = TestCharacter.aaa[TestCharacter.targetChess - i] - this.transform.position;
			//將座標差換算成長度(純量)
			float len = distance.magnitude;

			distance.Normalize();   //將座標差轉換成單位向量的資料型態(向量)


			//往右的旋轉值
			if (distance.x > 0.1f)
				this.transform.eulerAngles = new Vector3(0, 90, 0);

			//往左的旋轉值
			if (distance.x < -0.1f)
				this.transform.eulerAngles = new Vector3(0, -90, 0);

			//往上的旋轉值
			if (distance.z > 0.9f)
				this.transform.eulerAngles = new Vector3(0, 0, 0);

			//往下的旋轉值
			if (distance.z < -0.9f)
				this.transform.eulerAngles = new Vector3(0, 180, 0);



			//如果目標點與現在的位置,距離低於這一幀的長度
			if (len <= (distance.magnitude * Time.deltaTime * 2))   //distance.magnitude是一個純量
			{
				//把現在位置強制設定成目標位置
				this.transform.position = TestCharacter.aaa[TestCharacter.targetChess - i];
				i++;    //索引值+1
				if (TestCharacter.targetChess - i < 0)  //aaa[-1]不存在
				{
					i = 2;  //將 i 回歸初值
					break;  //跳出迴圈
				}

			}

			//Delay time 單位:秒
			yield return new WaitForSeconds(1/ MoveSpeed);
			//隨時間移動目前座標
			this.transform.position = this.transform.position + (distance * Time.deltaTime * 2); //distance是一個向量
		}


		TestCharacter.delete = false;   //把用於刪除chessbox的bool值,回歸false(初值)

		Path.index = 0; //存入ppp[]用的索引值歸0(初值)
		Path.Count = 0; //取出ppp[]用的索引值歸0(初值)
		TestCharacter.mSave = 0;    //暫存最大 m 值的變數歸0(初值)
		TestCharacter.targetChess = 0;  //存入和取出aaa[]用的索引值歸0(初值)

		Path.ppp.Clear();   //清空儲存行走範圍的陣列
		Path.mCount.Clear();    //清空儲存 m 值的陣列
		TestCharacter.aaa.Clear(); //清空儲存最短行走路徑的陣列

		//moveButton.gameObject.SetActive(true);
		Path.button = true;//將"移動"Button顯示出來

		//TestCharacter.ChessBoard = false; //移動完畢後,將隱藏大棋盤的bool回歸初值(false)


	}

	
	

	//------------------------------------------------------------------------------------

}
