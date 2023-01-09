using UnityEngine;

namespace KUMO
{
    /// <summary>
    /// 對話資料
    /// </summary>

    [CreateAssetMenu(menuName ="KUMO/Dialogue Data" , fileName = "New Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        [Header("對話者名稱")]
        public string dialoguename;
        [Header("對話者內容"),TextArea(1,10)]
        public string[] dialogueContents;
    }
}