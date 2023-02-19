using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
	private TestMapManager testMapManager;
	private TestCharactersManager testCharactersManager;
	private Path path;
	private List<TestMapBlock> reachableBlocks;
	private int charaStartPos_X; // 選択キャラクターの移動前の位置(X方向)
	private int charaStartPos_Z; // 選択キャラクターの移動前の位置(Z方向)
	private TestCharacter selectingChara; // 選中的角色（如果沒有選中則為 false）






	private enum Phase
	{
		MyTurn_Start,       // 我的回合：開始時
		MyTurn_Moving,      // 我的回合：移動先選択中
		MyTurn_Command,     // 我的回合：移動後のコマンド選択中
		MyTurn_Targeting,   // 我的回合：攻撃の対象を選択中
		MyTurn_Result,      // 我的回合：行動結果表示中
		EnemyTurn_Start,    // 敵方的回合：開始時
		EnemyTurn_Result    // 敵方的回合：行動結果表示中
	}
	private Phase nowPhase; // 現在の進行モード
							// Start is called before the first frame update
	void Start()
    {
		testMapManager = GetComponent<TestMapManager>();
		testCharactersManager = GetComponent<TestCharactersManager>();
		reachableBlocks = new List<TestMapBlock>();

		nowPhase = Phase.MyTurn_Start; // 開始時の進行モード
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0) )
				 // (←檢測對 UI 的點擊)
		{// 在非 UI 部分進行了點擊
		 // 在點擊目的地獲取塊並開始選擇過程
			GetMapBlockByTapPos();
		}
	}
	private void GetMapBlockByTapPos()
	{
		GameObject targetObject = null; // タップ対象のオブジェクト

		// 從相機向點擊的方向投射光線
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit))
		{// Rayに当たる位置に存在するオブジェクトを取得(対象にColliderが付いている必要がある)
			targetObject = hit.collider.gameObject;
		}

		// 対象オブジェクト(マップブロック)が存在する場合の処理
		if (targetObject != null)
		{
			// ブロック選択時処理
			SelectBlock(targetObject.GetComponent<TestMapBlock>());
			print("hi");
		}
	}

	private void SelectBlock(TestMapBlock targetBlock)
	{
		// 現在の進行モードごとに異なる処理を開始する
		switch (nowPhase)
		{
			// 自分のターン：開始時
			case Phase.MyTurn_Start:
				// 取消選擇所有塊
				testMapManager.AllselectionModeClear();
				// 將塊顯示為選中
				targetBlock.SetSelectionMode(true);
		// 選択した位置に居るキャラクターのデータを取得
		var charaData =
			testCharactersManager.GetCharacterDataByPos(targetBlock.xPos, targetBlock.zPos);
				if (charaData != null)
				{// キャラクターが存在する
				 // 選択中のキャラクター情報に記憶
					selectingChara = charaData;

					charaStartPos_X = selectingChara.xPos;
					charaStartPos_Z = selectingChara.zPos;

					path = selectingChara.GetComponent<Path>();
					path.Startpath();
					reachableBlocks = path.Startpath();
                    foreach (var item in reachableBlocks)
                    {
						print(item.name);
                    }
					// 進行モードを進める：移動先選択中
					ChangePhase(Phase.MyTurn_Moving);
				}
				break;

			// 自分のターン：移動先選択中
			case Phase.MyTurn_Moving:
				if (reachableBlocks.Contains(targetBlock))
				{
					print(targetBlock.name);
					print(targetBlock.xPos);
					print(targetBlock.zPos);
					selectingChara.MovePosition(targetBlock.xPos, targetBlock.zPos);
					Path.index = 0; //存入ppp[]用的索引值歸0(初值)
					Path.Count = 0; //取出ppp[]用的索引值歸0(初值)

					Path.ppp.Clear();   //清空儲存行走範圍的陣列
					Path.mCount.Clear();    //清空儲存 m 值的陣列
					reachableBlocks.Clear();
					testMapManager.AllselectionModeClear();
					ChangePhase(Phase.MyTurn_Start);
				}
					break;
		}

	}
	private void ChangePhase(Phase newPhase)
	{
		// モード変更を保存
		nowPhase = newPhase;
	}
}
