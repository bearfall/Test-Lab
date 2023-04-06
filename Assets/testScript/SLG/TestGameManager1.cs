using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;

public class TestGameManager1 : MonoBehaviour
{
	private TestMapManager testMapManager;
	private TestCharactersManager testCharactersManager;
	private TestGUIManager testGuiManager; // GUIマネージャ
	private Path path;
	private EnemyPath enemyPath;
	private List<TestMapBlock> reachableBlocks;
	private List<TestMapBlock> attackableBlocks;
	private int charaStartPos_X; // 選択キャラクターの移動前の位置(X方向)
	private int charaStartPos_Z; // 選択キャラクターの移動前の位置(Z方向)
	private TestCharacter selectingChara; // 選中的角色（如果沒有選中則為 false）
	private TestCharacter selectingEnemy;
	private TestCharacter testCharacter;

	




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
		attackableBlocks = new List<TestMapBlock>();

		testGuiManager = GetComponent<TestGUIManager>();
		nowPhase = Phase.MyTurn_Start; // 開始時の進行モード
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) // (←UIへのタップを検出する) )
		{// タップが行われた
		 // バトル結果表示ウィンドウが出ている時の処理
			/*
			   if (testGuiManager.testBattleWindowUI.gameObject.activeInHierarchy)
			   {
				   // バトル結果表示ウィンドウを閉じる
				   testGuiManager.testBattleWindowUI.HideWindow();
				   // 進行モードを進める(デバッグ用)
				   ChangePhase(Phase.MyTurn_Start);
				   return;
			   }
			*/

			// タップ先にあるブロックを取得して選択処理を開始する
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
				targetBlock.SetSelectionMode(TestMapBlock.Highlight.Select);
				// 選択した位置に居るキャラクターのデータを取得
				var charaData =
					testCharactersManager.GetCharacterDataByPos(targetBlock.xPos, targetBlock.zPos);
//				print(charaData.name);
				if (charaData != null && !charaData.isEnemy && charaData.hasActed == false)
				{// キャラクターが存在する
				 // 選択中のキャラクター情報に記憶
					selectingChara = charaData;
					testGuiManager.ShowStatusWindow(selectingChara);

					testCharacter = selectingChara.GetComponent<TestCharacter>();

					path = selectingChara.GetComponent<Path>();
					//path.Startpath();
					reachableBlocks = path.Startpath();



					/*
                    foreach (var item in reachableBlocks)
                    {
						print(item.name);
                    }
					*/
					// 進行モードを進める：移動先選択中
					ChangePhase(Phase.MyTurn_Moving);
				}
				else
				{// キャラクターが存在しない
				 // 選択中のキャラクター情報を初期化する
					ClearSelectingChara();
				}
				break;

			// 自分のターン：移動先選択中
			case Phase.MyTurn_Moving:
				if (reachableBlocks.Contains(targetBlock))
				{
					//print(targetBlock.name);
					//print(targetBlock.xPos);
					//print(targetBlock.zPos);
					selectingChara.MovePosition(targetBlock.xPos, targetBlock.zPos);





					reachableBlocks.Clear();
					testMapManager.AllselectionModeClear();
					testGuiManager.HideStatusWindow();
					// 指定秒数経過後に処理を実行する(DoTween)
					DOVirtual.DelayedCall(
						0.5f, // 遅延時間(秒)
						() =>
						{// 遅延実行する内容
							testGuiManager.ShowCommandButtons();
							ChangePhase(Phase.MyTurn_Command);
						}
					);
				}
				break;


			// 自分のターン：移動後のコマンド選択中
			case Phase.MyTurn_Command:
				// キャラクター攻撃処理
				// (攻撃可能ブロックを選択した場合に攻撃処理を呼び出す)
				if (attackableBlocks.Contains(targetBlock))
				{// 攻撃可能ブロックをタップした時
				 // 攻撃可能な場所リストを初期化する
					attackableBlocks.Clear();
					// 全ブロックの選択状態を解除
					testMapManager.AllselectionModeClear();
					foreach (TestMapBlock mapBlock in attackableBlocks)
						mapBlock.SetSelectionMode(TestMapBlock.Highlight.Off);

					// 攻撃対象の位置に居るキャラクターのデータを取得
					var targetChara =
						testCharactersManager.GetCharacterDataByPos(targetBlock.xPos, targetBlock.zPos);
					if (targetChara != null)
					{// 攻撃対象のキャラクターが存在する
					 // キャラクター攻撃処理
						CharaAttack(selectingChara, targetChara);
						testCharacter.hasActed = true;


						// 進行モードを進める(行動結果表示へ)
						ChangePhase(Phase.MyTurn_Result);
						return;
					}


					else
					{// 攻撃対象が存在しない
					 // 進行モードを進める(敵のターンへ)
						testCharacter.hasActed = true;
						CheckIsAllActive();
//						ChangePhase(Phase.EnemyTurn_Start);
					}
				}
				break;


		}



	}
	/// <summary>
	/// 選択中のキャラクター情報を初期化する
	/// </summary>
	private void ClearSelectingChara()
	{
		// 選択中のキャラクターを初期化する
		selectingChara = null;
		// キャラクターのステータスUIを非表示にする
		testGuiManager.HideStatusWindow();
	}





	private void ChangePhase(Phase newPhase)
	{
		nowPhase = newPhase;
		// 特定のモードに切り替わったタイミングで行う処理
		switch (nowPhase)
		{
			// 自分のターン：開始時
			case Phase.MyTurn_Start:
				// 自分のターン開始時のロゴを表示
				testGuiManager.ShowLogo_PlayerTurn();
				break;
			// 敵のターン：開始時
			case Phase.EnemyTurn_Start:
				// 敵のターン開始時のロゴを表示
				testGuiManager.ShowLogo_EnemyTurn();



				print(gameObject.name + "我要執行下面的EnemyCommand");

				StartCoroutine( EnemyCommand() );
				//				}
				//			);
				break;
		}
	}







	public void AttackCommand()
	{
		// コマンドボタンを非表示にする
		testGuiManager.HideCommandButtons();

		// 攻撃可能な場所リストを取得する
		attackableBlocks = testMapManager.SearchAttackableBlocks(selectingChara.xPos, selectingChara.zPos);
		print(selectingChara.xPos);
		print(selectingChara.zPos);

		// 攻撃可能な場所リストを表示する
		foreach (TestMapBlock mapBlock in attackableBlocks)
			mapBlock.SetSelectionMode(TestMapBlock.Highlight.Attackable);
		// (ここに攻撃範囲取得処理)
	}
	/// <summary>
	/// 待機コマンドボタン処理
	/// </summary>
	public void StandbyCommand()
	{
		// コマンドボタンを非表示にする
		testCharacter.hasActed = true;
		testCharacter.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.1f, 0.1f, 1);
		testGuiManager.HideCommandButtons();
		// 進行モードを進める(敵のターンへ)
		CheckIsAllActive();
	}

	/// <summary>
	/// キャラクターが他のキャラクターに攻撃する処理
	/// </summary>
	/// <param name="attackChara">攻撃側キャラデータ</param>
	/// <param name="defenseChara">防御側キャラデータ</param>
	private void CharaAttack(TestCharacter attackChara, TestCharacter defenseChara)
	{
		// ダメージ計算処理
		int damageValue; // ダメージ量
		int attackPoint = attackChara.atk; // 攻撃側の攻撃力
		int defencePoint = defenseChara.def; // 防御側の防御力
											 // ダメージ＝攻撃力－防御力で計算
		damageValue = attackPoint - defencePoint;
		// ダメージ量が0以下なら0にする
		if (damageValue < 0)
			damageValue = 0;


		// キャラクター攻撃アニメーション
		attackChara.AttackAnimation(defenseChara);


		// バトル結果表示ウィンドウの表示設定
		// (HPの変更前に行う)
		testGuiManager.testBattleWindowUI.ShowWindow(defenseChara, damageValue);

		// ダメージ量分防御側のHPを減少
		defenseChara.nowHP -= damageValue;
		// HPが0～最大値の範囲に収まるよう補正
		defenseChara.nowHP = Mathf.Clamp(defenseChara.nowHP, 0, defenseChara.maxHP);

		// HP0になったキャラクターを削除する
		if (defenseChara.nowHP == 0)
			testCharactersManager.DeleteCharaData(defenseChara);

		// ターン切り替え処理(遅延実行)
		DOVirtual.DelayedCall(
			2.0f, // 遅延時間(秒)
			() =>
			{// 遅延実行する内容
			 // ウィンドウを非表示化
				testGuiManager.testBattleWindowUI.HideWindow();
				testGuiManager.HideStatusWindow();
				// ターンを切り替える
				if (nowPhase == Phase.MyTurn_Result)
				{ // 敵のターンへ

					testCharacter.hasActed = true;
					testCharacter.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.1f, 0.1f, 1);
					CheckIsAllActive();
						//ChangePhase(Phase.EnemyTurn_Start);

				}
				else if (nowPhase == Phase.EnemyTurn_Result)
					// 自分のターンへ
					ChangePhase(Phase.EnemyTurn_Start);
			}
		);

	}

	/// (敵のターン開始時に呼出)
	/// 敵キャラクターのうちいずれか一体を行動させてターンを終了する
	/// </summary>
	/// 

	private IEnumerator EnemyCommand()
	{

		int randId;

		// 生存中の敵キャラクターのリストを作成する
		var enemyCharas = new List<TestCharacter>(); // 敵キャラクターリスト
		foreach (TestCharacter charaData in testCharactersManager.testCharacters)
		{// 全生存キャラクターから敵フラグの立っているキャラクターをリストに追加
			if (charaData.isEnemy &&  charaData.hasActed == false)
			{
				enemyCharas.Add(charaData);

				print(charaData.name);

			}
		}

        for (int i = 0; i < enemyCharas.Count; i++)
        {
			print("執行第" + i + "次運算回合");
			// 攻撃可能なキャラクター・位置の組み合わせの内１つをランダムに取得
			var actionPlan = TargetFinder1.GetActionPlan(testMapManager, testCharactersManager, enemyCharas[i]);
			// 組み合わせのデータが存在すれば攻撃開始
			if (actionPlan != null)
			{
				enemyCharas[i].EnemyMovePosition(actionPlan.toMoveBlock.xPos, actionPlan.toMoveBlock.zPos);

				enemyCharas[i].hasActed = true;
				print(enemyCharas[i] + "行動過了");

				DOVirtual.DelayedCall(
					2.5f, // 遅延時間(秒)
					() =>
					{// 遅延実行する内容

					CharaAttack(enemyCharas[i], actionPlan.toAttackChara);

					}
					);

				CheckIsAllEnemyActive();

				yield return new WaitForSeconds(3f);
				
			}
            else if(actionPlan == null)
            {

				//				enemyCharas[i].GetComponent<EnemyController>().delete();




				reachableBlocks = enemyCharas[i].GetComponent<EnemyPath>().results;
				print(reachableBlocks.Count);

				if (reachableBlocks.Count > 0)
				{
					randId = Random.Range(0, reachableBlocks.Count);
					TestMapBlock targetBlock = reachableBlocks[randId];

					print(targetBlock.transform.position);


					
					enemyCharas[i].EnemyMovePosition(targetBlock.xPos, targetBlock.zPos);
					enemyCharas[i].hasActed = true;
					print(enemyCharas[i] + "行動過了");


					CheckIsAllEnemyActive();


					yield return new WaitForSeconds(3f);
				}
			}
		}




		

	}


	public void CheckIsAllActive()
    {
		bool allActed = true;
		foreach (var character in testCharactersManager.testCharacters)
        {
			if (!character.isEnemy)
			{


				testCharacter = character.GetComponent<TestCharacter>();
				if (!testCharacter.hasActed)
				{
					allActed = false;
					ChangePhase(Phase.MyTurn_Start);
					break;
				}
			}
		}
		if (allActed)
		{
			foreach (var character in testCharactersManager.testCharacters)
			{
				if (!character.isEnemy)
				{
					testCharacter = character.GetComponent<TestCharacter>();
					testCharacter.hasActed = false;
					testCharacter.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);

				}

			}

			ChangePhase(Phase.EnemyTurn_Start);
		}

		

	}



	
	public void CheckIsAllEnemyActive()
	{
		bool allEnemyActed = true;
		foreach (var character in testCharactersManager.testCharacters)
		{
			if (character.isEnemy)
			{


				testCharacter = character.GetComponent<TestCharacter>();
				if (!testCharacter.hasActed)
				{
					allEnemyActed = false;
					break;
				}
			}
		}
		if (allEnemyActed)
		{
			foreach (var character in testCharactersManager.testCharacters)
			{
				if (character.isEnemy)
				{
					testCharacter = character.GetComponent<TestCharacter>();
					testCharacter.hasActed = false;
					ChangePhase(Phase.MyTurn_Start);
					print("Phase.MyTurn_Start");
				}
			}
		}



	}
	




}