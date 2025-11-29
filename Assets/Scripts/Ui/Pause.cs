using System;
using System.Collections;
using TMPro;
using UnityEngine;


namespace Ui
{
    public class PauseData
    {
        public bool IsPaused;
        public float TimeScale = 1.0f;
    }

    public class Pause : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textTitlePause;

        public float currentSpeedGame;
        public PauseData PauseData = new PauseData();
        public event Action<bool> OnPauseStateChanged;

        
        private void OnEnable()
        {
            OnPauseStateChanged += UpdateUi;
        }
        
        private void OnDisable()
        {
            OnPauseStateChanged -= UpdateUi;
        }


        public bool Paused
        {
            get => PauseData.IsPaused;
        }

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

        public void PauseGame()
        {
            if (!PauseData.IsPaused)
            {
                PauseData.TimeScale = Time.timeScale;
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = PauseData.TimeScale;
            }

            PauseData.IsPaused = !PauseData.IsPaused;
            OnPauseStateChanged?.Invoke(PauseData.IsPaused);
        }

        public void PauseGame(bool value, float timeScale)
        {
            PauseData.TimeScale = timeScale;
            Time.timeScale = timeScale;

            PauseData.IsPaused = value;
            OnPauseStateChanged?.Invoke(PauseData.IsPaused);
        }

        void UpdateUi(bool isPaused)
        {
            textTitlePause.gameObject.SetActive(isPaused);
            Debug.Log("Time.Scale = " + isPaused);
        }
    }
}