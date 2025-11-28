using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;


public class CoinSpawnerUI : MonoBehaviour
{
    public RectTransform coinPrefab;
    public RectTransform spawnPoint; // сундук/кошелек
    public RectTransform targetPoint; // куда летит
    public TextMeshProUGUI textCoinsAmount;

    public int coinsAmount;
    public int coinsCountPrefabs = 10;

    public void OnEnable()
    {
        Debug.Log("Сработал скрипт CoinSpawnerUI");
        textCoinsAmount.transform.DOScale(0f, 0f);
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        int numberToSpawnText = coinsCountPrefabs / 4 * 3;
        Debug.Log("numberToSpawnText: " + numberToSpawnText);

        for (int i = 0; i < coinsCountPrefabs; i++)
        {
            RectTransform coin = Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity,
                spawnPoint.transform);
            Vector2 randomOffset = new Vector2(Random.Range(-50, 50), Random.Range(80, 150));

            coin.DOScale(0f, 0);
            yield return new WaitForSeconds(0.1f);
            coin.DOScale(1f, 0.15f);

            if (i == numberToSpawnText)
            {
                Sequence seq = DOTween.Sequence();

                textCoinsAmount.text = $"{coinsAmount}";
                seq.Append(textCoinsAmount.transform.DOScale(1f, 1f))
                    .Append(textCoinsAmount.transform.DOScale(0f, 0.5f))
                    .OnComplete(() => gameObject.SetActive(false));
            }

            coin.DOAnchorPos(coin.anchoredPosition + randomOffset, Random.Range(0.2f, 0.4f))
                .SetEase(Ease.OutQuad)
                .SetDelay(0.1f)
                .OnComplete(() => { coin.DOScale(0f, 0.2f).OnComplete(() => Destroy(coin.gameObject)); });
        }
    }
}