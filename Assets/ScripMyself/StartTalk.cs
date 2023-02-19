using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace bearfall
{


    public class StartTalk : MonoBehaviour
    {
        [SerializeField, Header("第一段對話資料")]
        private DialogueData dataDialogue;
        [SerializeField, Header("對話結束後的事件")]
        private UnityEvent onDialogueFinish;
        [SerializeField, Header("啟動道具")]
        private GameObject propActive;
        [SerializeField, Header("啟動後的對話資料")]
        private DialogueData dataDialogueActive;
        
        private Button btnReplay;
        [SerializeField, Header("啟動後對話結束後的事件")]
        private UnityEvent onDialogueFinishAfterActive;
        private DialogueSystem dialogueSystem;

        private void Awake()
        {
            dialogueSystem = GameObject.Find("畫布對話系統").GetComponent<DialogueSystem>();
            btnReplay = GetComponent<Button>();
            btnReplay.onClick.AddListener(Talk);
        }




        private void Talk()
        {
            if (propActive == null || propActive.activeInHierarchy)
            {
                dialogueSystem.StartDialogue(dataDialogue, onDialogueFinish);
            }
            else
            {
                dialogueSystem.StartDialogue(dataDialogueActive, onDialogueFinishAfterActive);
            }
        }
    }
} 