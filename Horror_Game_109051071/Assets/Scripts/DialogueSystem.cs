using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

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


        private WaitForSeconds dialogueInterval => new WaitForSeconds(dialogueIntervalTime);

        private CanvasGroup groupDialogue;
        private TextMeshProUGUI textName;
        private TextMeshProUGUI textContent;
        private GameObject goCircle;
        #endregion

        #region 事件區域
        private void Awake()
        {
            groupDialogue = GameObject.Find("畫布對話系統").GetComponent<CanvasGroup>();
            textName = GameObject.Find("對話者名稱").GetComponent<TextMeshProUGUI>();
            textContent = GameObject.Find("對話者內容").GetComponent<TextMeshProUGUI>();
            goCircle = GameObject.Find("對話完成圖示");
            goCircle.SetActive(false);

            StartCoroutine(FadeGruop());
            StartCoroutine(TypeEffect());
        }
        #endregion

        /// <summary>
        /// 淡入淡出群組物件
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeGruop()
        {
            for (int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += 0.1f;
                yield return new WaitForSeconds(0.04f);
            }
        }

        private IEnumerator TypeEffect()
        {
            textName.text = dialogueOpening.dialoguename;
            textContent.text = "";

            string dialogue = dialogueOpening.dialogueContents[0];

            for(int i = 0; i< dialogue.Length; i++)
            {
                textContent.text += dialogue[i];
                yield return dialogueInterval;
            }

            goCircle.SetActive(true);
        }
    }
}