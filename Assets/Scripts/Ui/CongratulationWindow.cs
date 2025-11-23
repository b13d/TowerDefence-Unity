using System;
using UnityEngine;

namespace Ui
{
    public enum EnumTypeWindow
    {
        Win,
        Lose,
    }
    
    public class CongratulationWindow : MonoBehaviour
    {
        public EnumTypeWindow typeWindow;
        
        void Start()
        {
            if (typeWindow == EnumTypeWindow.Win)
            {
                StartCoroutine(AudioSettings.instance.CongratulationMusic(4));
            }
            else
            {
                StartCoroutine(AudioSettings.instance.CongratulationMusic(7));
            }
        }

        private void OnDisable()
        {
            AudioSettings.instance.Reset();
        }
    }
}
