using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Staticでクラスを定義する
public static class TargetFinder
{
	// 行動プランクラス
	// (行動する敵キャラクター、移動先の位置、攻撃相手のキャラクターの３データを１まとめに扱う)
	public class ActionPlan
	{
		
		public TestCharacter charaData; // 行動中的敵人角色
		public TestMapBlock toMoveBlock; // 目標位置
		public TestCharacter toAttackChara; // 攻擊者角色
	}

	/// <summary>
	/// 攻撃可能な行動プランを全て検索し、その内の１つをランダムに返す処理
	/// </summary>
	/// <param name="mapManager">在場景中引用 MapManager</param>
	/// <param name="charactersManager">在場景中引用 CharactersManager</param>
	/// <param name="enemyCharas">敵人角色列表</param>
	/// <returns></returns>
	public static ActionPlan GetRandomActionPlan(TestMapManager mapManager, TestCharactersManager charactersManager, List<TestCharacter> enemyCharas)
	{

		// 全行動プラン(攻撃可能な相手が見つかる度に追加される)
		var actionPlans = new List<ActionPlan>();
		// 移動範囲リスト
		var reachableBlocks = new List<TestMapBlock>();
		// 攻撃範囲リスト
		var attackableBlocks = new List<TestMapBlock>();

		// 全行動プラン検索処理
		foreach (TestCharacter enemyData in enemyCharas)
		{
			// 移動可能な場所リストを取得する
			var selectingEnemy = enemyData;
			var enemyPath = selectingEnemy.GetComponent<EnemyPath>();
			enemyPath.StartEnemypath();
			reachableBlocks = enemyPath.StartEnemypath();
			// それぞれの移動可能な場所ごとの処理
			foreach (TestMapBlock block in reachableBlocks)
			{
				// 攻撃可能な場所リストを取得する
				attackableBlocks = mapManager.EnemySearchAttackableBlocks(block.xPos, block.zPos);
				// それぞれの攻撃可能な場所ごとの処理
				foreach (TestMapBlock attackBlock in attackableBlocks)
				{
					// 攻撃できる相手キャラクター(プレイヤー側のキャラクター)を探す
					TestCharacter targetChara =
						charactersManager.GetCharacterDataByPos(attackBlock.xPos, attackBlock.zPos);
					if (targetChara != null &&
						!targetChara.isEnemy)
					{// 相手キャラクターが存在する
					 // 行動プランを新規作成する
						var newPlan = new ActionPlan();
						newPlan.charaData = enemyData;
						newPlan.toMoveBlock = block;
						newPlan.toAttackChara = targetChara;

						// 全行動プランリストに追加
						actionPlans.Add(newPlan);
					}
				}
			}
		}

		// 検索終了後、行動プランが１つでもあるならその内の１つをランダムに返す
		if (actionPlans.Count > 0)
			return actionPlans[Random.Range(0, actionPlans.Count)];
		else // 行動プランが無いならnullを返す
			return null;
	}
}