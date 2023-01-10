using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace KUMO

{
    /// <summary>
    /// 對話系統
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        #region 資料區域
        [SerializeField, Header("對話間隔"), Range(0, 0.5f)]
        private float dialogueIntervalTime = 0.1f;
        [SerializeField, Header("開頭對話")]
        private DialogueData dialogueOpening;
        [SerializeField, Header("對話按鍵")]
        private KeyCode dialogueKey = KeyCode.Mouse0;

        private WaitForSeconds dialogueInterval => new WaitForSeconds(dialogueIntervalTime);

        private CanvasGroup groupDialogue;
        private TextMeshProUGUI textName;
        private TextMeshProUGUI textContent;
        private GameObject goCircle;
        #endregion
        private PlayerInput PlayerInput;

        private UnityEvent onDialogueFinish;

        #region 事件區域
        private void Awake()
        {
            groupDialogue = GameObject.Find("畫布對話系統").GetComponent<CanvasGroup>();
            textName = GameObject.Find("對話者名稱").GetComponent<TextMeshProUGUI>();
            textContent = GameObject.Find("對話者內容").GetComponent<TextMeshProUGUI>();
            goCircle = GameObject.Find("對話完成圖示");
            goCircle.SetActive(false);

            PlayerInput = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>();

            StartDialogue(dialogueOpening);
        }
        #endregion

        public void StartDialogue(DialogueData data, UnityEvent _onDialogueFinish = null)
        {
            PlayerInput.enabled = false;
            StartCoroutine(FadeGruop());
            StartCoroutine(TypeEffect(data));
            onDialogueFinish = _onDialogueFinish;
        }

        /// <summary>
        /// 淡入淡出群組物件
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeGruop(bool fadeIn = true)
        {
            ///三元運算子  ?  :
            float increase = fadeIn ? +0.1f : -0.1f;

            for (int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += increase;
                yield return new WaitForSeconds(0.04f);
            }
        }

        private IEnumerator TypeEffect(DialogueData data)
        {
            textName.text = data.dialoguename;

            for (int j = 0; j < data.dialogueContents.Length; j++)
            {
                textContent.text = "";
                goCircle.SetActive(false);

                string dialogue = data.dialogueContents[j];

                for (int i = 0; i < dialogue.Length; i++)
                {
                    textContent.text += dialogue[i];
                    yield return dialogueInterval;
                }

                goCircle.SetActive(true);

                //如果 玩家 還沒按下 指定按鍵 等待
                while (!Input.GetKeyDown(dialogueKey))
                {
                    yield return null;
                }
            }

            StartCoroutine(FadeGruop(false));

            PlayerInput.enabled = true;
            onDialogueFinish?.Invoke();
        }
    }
}