using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;


public class CoinSpawnerUI : MonoBehaviour
{
    public static event Action<int> CoinSpawned;

    public RectTransform coinPrefab;
    public RectTransform spawnPoint; // сундук/кошелек
    public RectTransform targetPoint; // куда летит
    public TextMeshProUGUI textCoinsAmount;

    public int coinsAmount;
    public int coinsCountPrefabs = 10;

    public void OnEnable()
    {
        Debug.Log("Сработал скрипт CoinSpawnerUI");
        textCoinsAmount.transform.localScale = Vector3.zero;
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        textCoinsAmount.transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        CoinSpawned?.Invoke(coinsAmount);
        textCoinsAmount.text = $"{coinsAmount}";

        Sequence seq = DOTween.Sequence();
        seq.Append(textCoinsAmount.transform.DOScale(1f, 1.5f))
            .Append(textCoinsAmount.transform.DOScale(0f, 0.85f))
            .OnComplete(() => gameObject.SetActive(false));

        for (int i = 0; i < coinsCountPrefabs; i++)
        {
            RectTransform coin = Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity,
                spawnPoint.transform);
            Vector2 randomOffset = new Vector2(Random.Range(-50, 50), Random.Range(80, 150));




            coin.DOScale(0f, 0);
            yield return new WaitForSeconds(0.1f);
            coin.DOScale(1f, 0.15f);

            coin.DOAnchorPos(coin.anchoredPosition + randomOffset, Random.Range(0.2f, 0.4f))
                .SetEase(Ease.OutQuad)
                .SetDelay(0.1f)
                .OnComplete(() => { coin.DOScale(0f, 0.2f).OnComplete(() => Destroy(coin.gameObject)); });
        }
    }
}