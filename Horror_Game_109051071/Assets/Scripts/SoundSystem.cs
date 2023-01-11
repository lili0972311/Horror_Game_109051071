using OpenCover.Framework.Model;
using UnityEngine;


namespace KUMO
{
    /// <summary>
    /// 音效系統
    /// </summary>
    /// 要求元件:在第一次套用此腳本時會添加裡面指定的元件
    [RequireComponent(typeof(AudioSource))]
    public class SoundSystem : MonoBehaviour
    {
        private AudioSource aud;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();
        }

        ///<Summary>
        ///播放音效
        ///</Summary>
        ///<param name="sound">要播放的音效</param>
        public void PlaySound(AudioClip sound)
        {
            //音效來源.播放一次音效(音效);
            aud.PlayOneShot(sound);
        }


    }
}