using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private int startValue = 3;
    [SerializeField] private int endValue = 0;
    [SerializeField] private float stepDuration = 1f;
    [SerializeField] private bool punchScale = true;
    [SerializeField] private Vector3 punchScaleStrength = new Vector3(0.3f, 0.3f, 0f);
    [SerializeField] private int punchVibrato = 4;
    [SerializeField] private float punchElasticity = 0.5f;

    public event Action OnCountdownFinished;

    private Sequence _sequence;
    private Vector3 _originalScale;

    public void StartCountdown(Action playFn)
    {
        if (countdownText != null) 
        {
            _originalScale = countdownText.transform.localScale;
            
            _sequence = DOTween.Sequence();
            _sequence.SetUpdate(true);
            _sequence.AppendInterval(0.25f);

            for (int i = 3; i > 0; i--)
            {
                int value = i;
                
                _sequence.AppendCallback(() => countdownText.text = value.ToString());
                _sequence.Append(countdownText.transform.DOScale(5f, 0.5f));
                _sequence.AppendInterval(0.25f);
                _sequence.Append(countdownText.transform.DOScale(1f, 0.5f));
            }

            _sequence.OnComplete(() =>
            {
                playFn?.Invoke();
            });
            
            // Игра началась
        }
    }
    
    private void OnDestroy()
    {
        _sequence?.Kill();
    }


}
