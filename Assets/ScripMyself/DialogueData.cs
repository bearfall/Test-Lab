using UnityEngine;
namespace bearfall
{
    /// <summary>
    /// 對話資料
    /// </summary>
    [CreateAssetMenu(menuName = "bearfall/Dialogue Data", fileName = "New Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        [Header("對話者名稱")]
        public string dialogueName;
        [Header("對話者名稱"), TextArea(2, 10)]
        public string[] dialogueContents;
    }
}