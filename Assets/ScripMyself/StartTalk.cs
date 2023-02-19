using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace bearfall
{


    public class StartTalk : MonoBehaviour
    {
        [SerializeField, Header("�Ĥ@�q��ܸ��")]
        private DialogueData dataDialogue;
        [SerializeField, Header("��ܵ����᪺�ƥ�")]
        private UnityEvent onDialogueFinish;
        [SerializeField, Header("�ҰʹD��")]
        private GameObject propActive;
        [SerializeField, Header("�Ұʫ᪺��ܸ��")]
        private DialogueData dataDialogueActive;
        
        private Button btnReplay;
        [SerializeField, Header("�Ұʫ��ܵ����᪺�ƥ�")]
        private UnityEvent onDialogueFinishAfterActive;
        private DialogueSystem dialogueSystem;

        private void Awake()
        {
            dialogueSystem = GameObject.Find("�e����ܨt��").GetComponent<DialogueSystem>();
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