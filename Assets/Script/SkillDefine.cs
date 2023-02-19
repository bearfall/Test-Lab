using System.Collections;
using System.Collections.Generic;

public static class SkillDefine
{
	// 特技の種類を定義
	public enum Skill
	{
		_None, // 無技能
		Critical,// 致命一擊
		DefBreak,// 盾破
		Heal, // 治癒
		FireBall, // 火球

	}



	// 將特殊技能定義與字典中的每個數據相關聯
	// 特技名
	public static Dictionary<Skill, string> dic_SkillName = new Dictionary<Skill, string>()
	{
		{Skill._None, "スキル無し"},
		{Skill.Critical, "会心の一撃"},
		{Skill.DefBreak, "シールド破壊"},
		{Skill.Heal, "ヒール"},
		{Skill.FireBall, "ファイアボール"},
	};
	// 表示する説明文
	public static Dictionary<Skill, string> dic_SkillInfo = new Dictionary<Skill, string>()
	{
		{Skill._None, "----"},
		{Skill.Critical, "雙倍傷害攻擊\n（僅一次）"},
		{Skill.DefBreak, "將敵人的防禦設置為 0\n（傷害為 0）"},
		{Skill.Heal, "回复盟友HP"},
		{Skill.FireBall, "你可以攻擊任何位置的敵人\n（傷害減半）"},
	};


}