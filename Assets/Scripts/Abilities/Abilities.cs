using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    const int MaxCountdownMeteor = 10;
    [SerializeField] int countdownMeteor;
    [SerializeField] private Button buttonMeteor;
    [SerializeField] private Slider sliderMeteor;

    public int Countdown => countdownMeteor;

    private void Start()
    {
        sliderMeteor.value = countdownMeteor;
        buttonMeteor.interactable = false;
        StartCoroutine(CountDownMeteor());
    }

    IEnumerator CountDownMeteor()
    {
        while (countdownMeteor > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownMeteor--;
            sliderMeteor.value = countdownMeteor;
        }

        if (countdownMeteor == 0)
        {
            buttonMeteor.interactable = true;
        }
    }

    public void MeteorUsed()
    {
        countdownMeteor = MaxCountdownMeteor;
        sliderMeteor.value = MaxCountdownMeteor;
        buttonMeteor.interactable = false;
        StartCoroutine(CountDownMeteor());
    }
}
