using UnityEngine;
using UnityEngine.Events;
namespace bearfall
{
    /// <summary>
    /// 互動系統:偵測玩家是否進入並且執行互動行為
    /// </summary>

    public class InteractableSystemChange : MonoBehaviour
    {
        [SerializeField, Header("第一段對話資料")]
        private DialogueData dataDialogue;
        [SerializeField, Header("對話結束後的事件")]
        private UnityEvent onDialogueFinish;
        [SerializeField, Header("啟動道具")]
        private GameObject propActive;
        [SerializeField, Header("啟動後的對話資料")]
        private DialogueData dataDialogueActive;
        private string nameTarget = "玩家1";

        [SerializeField, Header("啟動後對話結束後的事件")]
        private UnityEvent onDialogueFinishAfterActive;

        [SerializeField, Header("離開")]
        private UnityEvent Leave;

        private DialogueSystemChange dialogueSystemChange;

        private void Awake()
        {
            dialogueSystemChange = GameObject.Find("畫布對話系統").GetComponent<DialogueSystemChange>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name.Contains(nameTarget))
            {
                print(other.name);

                //如果 不需要啟動道具 或者 啟動道具是顯示的 就執行 第一段對話
                if (propActive == null || propActive.activeInHierarchy)
                {
                    dialogueSystemChange.StartDialogueChange(onDialogueFinish);
                }
                else
                {
                    dialogueSystemChange.StartDialogueChange(onDialogueFinishAfterActive);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name.Contains(nameTarget))
            {
                dialogueSystemChange.StartDialogueChange(Leave);
            }
        }

        public void HiddenObject()
        {
            gameObject.SetActive(false);
        }
    }
}