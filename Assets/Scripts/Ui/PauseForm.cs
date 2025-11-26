using System;
using System.Collections;
using TMPro;
using UnityEngine;


namespace Ui
{
    public class PauseForm : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textTitlePause;

        public float currentSpeedGame;
        public bool isPaused;

        private void Awake()
        {
            StartCoroutine(TimeScaleDebugCoroutine());
        }

        private IEnumerator TimeScaleDebugCoroutine()
        {
            while (true)
            {
                DebugTimeScale();
                yield return new WaitForSeconds(1f);
            }
        }
        
        public void DebugTimeScale()
        {
            // Debug.Log("Time Scale: " + Time.timeScale);
        }
        
        void UpdateUi()
        {
            textTitlePause.gameObject.SetActive(isPaused);
        }
        
        public void Pause()
        {
            if (!isPaused)
            {
                currentSpeedGame = Time.timeScale;
            }
            
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : currentSpeedGame;            
            UpdateUi();
        }
    }
}