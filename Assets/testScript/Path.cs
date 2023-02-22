using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;


//=================================================================
//	(1)用於計算最大可行走範圍,並於點選完成後,將部份與"移動"有關的參數回歸初值
//	(2)點選右鍵可以取消"移動"
//=================================================================

public class Path : MonoBehaviour
{

	public GameObject chessBox; //這個用來放棋盤格(ChessBox)
	public GameObject chessBoard; //這個用來放棋盤基底(BigChessBoard)
	public Button moveButton;
	private TestMapBlock testMapBlock;
	public List<TestMapBlock> results = new List<TestMapBlock>();


	public static Vector3 Position; //用來記錄下一步的位置
									//public static Vector3 PlayerPosition;	//玩家位置

	private bool check = true;  //用來判斷某一格是不是已經走過,若走過則為false
	private bool monsterCheck = true;   //用來判斷下一格是否有怪物
	public static bool camera = false;  //用來控制攝影機拉近拉遠,false拉近,true拉遠. 點選"移動"前是拉近的,故此處初值為false
	public static bool button = true;   //用來控制Button出現的時機,true出現,false消失
	public static bool cancel = false;  //用來取消任何戰鬥指令,在點選任意Button後,需將其變成true才能發揮作用

	public int m; //m = 可移動數
	public static int CanMove;  //記錄可移動數,第一輪需要
	public static int index = 0;    //存入陣列用的引數
	public int blockIndex;

    public static object Combine(string dataPath, string v)
    {
        throw new NotImplementedException();
    }

    public static int Count = 0;    //取出陣例內容用的引數

	private int i = 0; //迴圈計數用

	public static List<int> mCount = new List<int>();   //用來記錄每次移動過後,剩餘的m值
	public static List<Vector3> ppp = new List<Vector3>();  //用來記錄每次移動過後的新位置


	//--------------------------------------------------------------------------------------------------

	void Start()
	{
		//PlayerPosition = this.transform.position;	//用PlayerPosition儲存角色目前的位置
		CanMove = m;    //把CanMove設定成最大移動數

		var results = new List<TestMapBlock>();
	}

	//--------------------------------------------------------------------------------------------------

	void Update()
	{
		if (ClickPosition.delete == true)   //點完格子之後
			m = CanMove;    //把 m 值回歸最大值

		if (Input.GetMouseButton(1) && cancel == true)  //點選右鍵,取消"移動"
		{
			cancel = false; //按一次後就變成false,防止重複點擊造成錯誤
			camera = false; //拉近攝影機
			ClickPosition.delete = true;    //將delete設為true,藉此刪除棋盤
			//.SetActive(false);    //隱藏大棋盤
			Reset();    //重置部份參數,回歸點選"移動"前的初值

		}


		//moveButton.onClick.AddListener(moveButtonActive);



	}

	//-----------------------------------------------------------------------------------------------





	void moveButtonActive()
	{
		if (button == true)
		{
			camera = true;  //將鏡頭拉遠
			moveButton.gameObject.SetActive(false); //讓"移動"Button消失,防止重複點擊
			button = false;
			ClickPosition.delete = false;   //delete為false時,棋盤格才能被顯示(克隆)
			//chessBoard.SetActive(true); //顯示大棋盤
			Startpath(); //開始計算可行走範圍
		}
	}
	//----------------------------------------------------------------------------------------------

	public List<TestMapBlock> Startpath()
	{
		

		for (i = 0; i < 500; i++)   //500最多可以算到16格
		{

			//第一輪,從Player位置出發,依序走上下左右
			if (i == 0) //第一輪
			{
				//把起始位置存入陣列
				mCount.Insert(index, m);
				ppp.Insert(index, this.transform.position);
				index++;


				//向上走
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((this.transform.position.z + 1) == MonsterPosition.monsterPosition[j].z) && (this.transform.position.x == MonsterPosition.monsterPosition[j].x))   //如果接下來要移動的那格有怪物
						monsterCheck = false;   //就把monsterCheck變成false
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)    //**新的迴圈,用來判斷隊友
				{
					if (((this.transform.position.z + 1) == PartnerPosition.partnerPosition[j].z) && (this.transform.position.x == PartnerPosition.partnerPosition[j].x))   //如果接下來要移動的那格有怪物
						monsterCheck = false;   //就把monsterCheck變成false
				}
				*/

				if (monsterCheck == true)   //如果是ture,表示接下來要移動的那格沒有怪物
				{
					Position = this.transform.position + new Vector3(0, 0, 1);  //那就走,然後由Position記錄新位置
					PathCount();    //記錄新位置,以及新位置的剩餘 m(行動力) 值
					m = CanMove;    //把 m 回歸最大值

				}

				monsterCheck = true;    //探索完一個方向就回歸初值


				//向下走
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((this.transform.position.z - 1) == MonsterPosition.monsterPosition[j].z) && (this.transform.position.x == MonsterPosition.monsterPosition[j].x))   //如果接下來要移動的那格有怪物
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**新的迴圈,用來判斷隊友
				{
					if (((this.transform.position.z - 1) == PartnerPosition.partnerPosition[j].z) && (this.transform.position.x == PartnerPosition.partnerPosition[j].x))   //如果接下來要移動的那格有怪物
						monsterCheck = false;
				}
				*/

				if (monsterCheck == true)
				{
					Position = this.transform.position + new Vector3(0, 0, -1);
					PathCount();
					m = CanMove;

				}

				monsterCheck = true;


				//向左走
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((this.transform.position.x - 1) == MonsterPosition.monsterPosition[j].x) && (this.transform.position.z == MonsterPosition.monsterPosition[j].z))   //如果接下來要移動的那格有怪物
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**新的迴圈,用來判斷隊友
				{
					if (((this.transform.position.x - 1) == PartnerPosition.partnerPosition[j].x) && (this.transform.position.z == PartnerPosition.partnerPosition[j].z))   //如果接下來要移動的那格有怪物
						monsterCheck = false;
				}
				*/

				if (monsterCheck == true)
				{
					Position = this.transform.position + new Vector3(-1, 0, 0);
					PathCount();
					m = CanMove;

				}

				monsterCheck = true;


				//向右走
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((this.transform.position.x + 1) == MonsterPosition.monsterPosition[j].x) && (this.transform.position.z == MonsterPosition.monsterPosition[j].z))   //如果接下來要移動的那格有怪物
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**新的迴圈,用來判斷隊友
				{
					if (((this.transform.position.x + 1) == PartnerPosition.partnerPosition[j].x) && (this.transform.position.z == PartnerPosition.partnerPosition[j].z))   //如果接下來要移動的那格有怪物
						monsterCheck = false;
				}
				*/

				if (monsterCheck == true)
				{
					Position = this.transform.position + new Vector3(1, 0, 0);
					PathCount();
					m = CanMove;    // 最後一個可以不需要,因為第二輪開始的m,是從剛才陣列裡抓取的

				}

				monsterCheck = true;
				Count++;    //每走完一輪就+1,用以達到"更換出發點"的效果

			}


			//第2~n輪,換新出發點,上下左右
			if (i != 0) //第2~n輪
			{

				//向上走
				//取出陣列目前最前端的值,將曾經走過的某一點作為新出發點,並判斷下一步的位置是否有怪物. 另外,如果新出發點的m值<=0,表示不能再走
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((ppp[Count].z + 1) == MonsterPosition.monsterPosition[j].z) && (ppp[Count].x == MonsterPosition.monsterPosition[j].x) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**新的迴圈,用來判斷隊友
				{
					if (((ppp[Count].z + 1) == PartnerPosition.partnerPosition[j].z) && (ppp[Count].x == PartnerPosition.partnerPosition[j].x) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				*/
				if (monsterCheck == true)
				{
					Position = ppp[Count] + new Vector3(0, 0, 1);   //從新出發點往上走一格,然後由Position記錄移動後的位置
					pppCheck(); //檢查現在試探的這格,之前是否走過
				}

				if (check == true && monsterCheck == true && (mCount[Count] > 0))   //如果check = true,表示接下來這步還沒走過. monsterCheck = true,表示接下來這格沒怪物
					PathCount();    //記錄新位置,以及新位置的剩餘 m(行動力) 值

				check = true;   //把check回歸初值
				monsterCheck = true;    //把monsterCheck回歸初值



				//向下走
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{

					if (((ppp[Count].z - 1) == MonsterPosition.monsterPosition[j].z) && (ppp[Count].x == MonsterPosition.monsterPosition[j].x) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**新的迴圈,用來判斷隊友
				{

					if (((ppp[Count].z - 1) == PartnerPosition.partnerPosition[j].z) && (ppp[Count].x == PartnerPosition.partnerPosition[j].x) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				*/
				if (monsterCheck == true)
				{
					Position = ppp[Count] + new Vector3(0, 0, -1);
					pppCheck();
				}

				if (check == true && monsterCheck == true && (mCount[Count] > 0))
					PathCount();

				check = true;
				monsterCheck = true;


				//向左走
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((ppp[Count].x - 1) == MonsterPosition.monsterPosition[j].x) && (ppp[Count].z == MonsterPosition.monsterPosition[j].z) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**新的迴圈,用來判斷隊友
				{
					if (((ppp[Count].x - 1) == PartnerPosition.partnerPosition[j].x) && (ppp[Count].z == PartnerPosition.partnerPosition[j].z) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				*/
				if (monsterCheck == true)
				{
					Position = ppp[Count] + new Vector3(-1, 0, 0);
					pppCheck();
				}

				if (check == true && monsterCheck == true && (mCount[Count] > 0))
					PathCount();

				check = true;
				monsterCheck = true;


				//向右走
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((ppp[Count].x + 1) == MonsterPosition.monsterPosition[j].x) && (ppp[Count].z == MonsterPosition.monsterPosition[j].z) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**新的迴圈,用來判斷隊友
				{
					if (((ppp[Count].x + 1) == PartnerPosition.partnerPosition[j].x) && (ppp[Count].z == PartnerPosition.partnerPosition[j].z) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				*/
				if (monsterCheck == true)
				{
					Position = ppp[Count] + new Vector3(1, 0, 0);
					pppCheck();
				}

				if (check == true && monsterCheck == true && (mCount[Count] > 0))
					PathCount();

				check = true;
				monsterCheck = true;


				//第一輪結束時,Count=1. 第二輪剛開始,會從第1個位置開始取出陣列值,意即ppp[1],mCount[1]
				//第二輪結束時,把Count+1,下一輪才會取出陣列的第2個位置,意即ppp[2],mCount[2]
				//用以達到"更換新出發點"的效果
				Count++;

			}

			//index為存入陣列用的索引值,若取出陣列用的索引值(Count)>=index,說明陣列已經搜索完畢
			if (Count >= index)
				break;  //跳出迴圈

		}

		cancel = true;  //這時候按"右鍵"才有取消行動的功能
		return results;
	}


	//-------------------------------------------------------------------------------------

	public void pppCheck() //用來檢查某一格是否已經走過
	{
		for (int k = 0; k < index; k++) //跑遍ppp陣列裡所有的值
		{
			if (Position == ppp[k]) //如果接下來要走的一步,在陣列裡已經有了(表示曾經走過)
			{
				check = false;  //把check變成false
				break;  //跳出迴圈
			}
		}
	}

	//--------------------------------------------------------------------------------------

	public void PathCount()    //用來記錄、計算每一格的座標位置，以及每一格的 m(剩餘行動力) 值
	{
		//在新位置上,克隆一個棋盤格
		//Position.x(新位置的X座標),Position.z(新位置的Z座標)
		//Instantiate(chessBox, new Vector3(Position.x, 0, Position.z), chessBox.transform.rotation);
		 Vector3 hi = new Vector3(Position.x, 0, Position.z);

        foreach (var gameObject in TestMapManager.testMapBlocks)
        {
            if (gameObject.transform.position == hi)
            {
				gameObject.transform.GetChild(0).gameObject.SetActive(true);
				gameObject.SetSelectionMode(TestMapBlock.Highlight.Reachable);
				print(gameObject.transform.GetChild(0).gameObject);
				results.Add(gameObject);
			}
		}
	  
		m = mCount[Count] - 1;  //行動數-1(mCount[Count]是移動前的m值,因為ppp[Count]是移動前的位置)
		mCount.Insert(index, m);    //把這次移動過後剩餘的m值,存入陣列
		ppp.Insert(index, Position);    //把這次移動過後的新位置,存入陣列
		index++;    //把儲存用的引數+1
	}

	//---------------------------------------------------------------------------------------

	void Reset()
	{
		moveButton.gameObject.SetActive(true);
		button = true;//將"移動"Button顯示出來
		index = 0;  //存入ppp[]用的索引值歸0(初值)
		Count = 0;  //取出ppp[]用的索引值歸0(初值)
		ppp.Clear();    //清空儲存行走範圍的陣列
		mCount.Clear(); //清空儲存 m 值的陣列
	}

	//---------------------------------------------------------------------------------------

}



