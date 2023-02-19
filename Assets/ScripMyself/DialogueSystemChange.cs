using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace bearfall
{
    /// <summary>
    /// 對話系統
    /// </summary>

    public class DialogueSystemChange : MonoBehaviour
    {
        #region 資料區域
        [SerializeField, Header("對話間隔"), Range(0, 0.5f)]
        private float dialogueIntervalTime = 0.1f;
        [SerializeField, Header("開頭對話")]
        private DialogueData dialogueOpening;
        [SerializeField, Header("對話按鍵")]
        private KeyCode dialogueKey = KeyCode.Space;

        private WaitForSeconds dialogueInterval => new WaitForSeconds(dialogueIntervalTime);


        private CanvasGroup groupDialogue;
        private TextMeshProUGUI textName;
        private TextMeshProUGUI textContent;
        private GameObject goTriangle;
        private PlayerInput playerInput;
        private UnityEvent onDialogueFinish;
        #endregion




        #region 事件
        private void Awake()
        {
            groupDialogue = GameObject.Find("畫布對話系統").GetComponent<CanvasGroup>();
            textName = GameObject.Find("對話者名稱").GetComponent<TextMeshProUGUI>();
            textContent = GameObject.Find("對話內容").GetComponent<TextMeshProUGUI>();
            
            

            playerInput = GameObject.Find("玩家1").GetComponent<PlayerInput>();

            
        } 
        #endregion
        /// <summary>
        /// 開始對話
        /// </summary>
        /// <param name="data">要執行的對話資料</param>
        /// <param name="_onDialogueFinish">對話結束的事件，可以為空值</param>
        public void StartDialogueChange( UnityEvent _onDialogueFinish = null)
        {
            playerInput.enabled = false;
            
            
            onDialogueFinish = _onDialogueFinish;
            onDialogueFinish?.Invoke();
        }
        /// <summary>
        /// 淡入淡出群組物件
        /// </summary>
        private IEnumerator FadeGroup(bool fadeIn = true)
        {

            float increase = fadeIn ? +0.1f : -0.1f;
            for (int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += increase;
                yield return new WaitForSeconds(0.04f);
            }
        }
        #region 打字效果
        private IEnumerator TypeEffect(DialogueData data)
        {
            textName.text = data.dialogueName;

            for (int j = 0; j < data.dialogueContents.Length; j++)
            {
                textContent.text = "";
                goTriangle.SetActive(false);

                string dialogue = data.dialogueContents[j];

                for (int i = 0; i < dialogue.Length; i++)
                {
                    textContent.text += dialogue[i];
                    yield return dialogueInterval;
                }

                goTriangle.SetActive(true);

                //如果 玩家 還沒按下 指定按鍵 就等待
                while (!Input.GetKeyDown(dialogueKey))
                {
                    yield return null;
                }
            }

            StartCoroutine(FadeGroup(false));

            playerInput.enabled = true;             //開啟玩家輸入元件
            onDialogueFinish?.Invoke();               //對話結束事件.呼叫();
        }
        #endregion
    }
}