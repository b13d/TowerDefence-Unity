using System;
using System.Collections;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] private Material mDisable;
    [SerializeField] private Material mFocus;
    [SerializeField] CanvasSidebar canvasSidebar;
    [SerializeField] private GameObject zoneTower;
    public bool hasTower;


    private void Awake()
    {
        zoneTower.SetActive(false);
    }

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        canvasSidebar = FindObjectOfType<CanvasSidebar>();
    }

    public void SetTower(GameObject tower)
    {
        Debug.Log("SetTower func");

        hasTower = true;
        Instantiate(tower, transform.position, transform.localRotation);
        Destroy(gameObject); // возможно не нужно так делать для будущих башен, после одной этой установленной

        Debug.Log("hasTower: " + hasTower);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            if (!zoneTower.activeSelf)
            {
                zoneTower.SetActive(true);
            }
            
            other.transform.localRotation = transform.localRotation;
            StopAllCoroutines();
            StartCoroutine(TriggerTower(other.gameObject));
        }
    }

    IEnumerator TriggerTower(GameObject other)
    {
        Debug.Log("Башня в триггере");

        TowerInfo towerInfo = other.GetComponent<TowerDisabled>().GetTowerInfo();

        canvasSidebar.TextTooltip.text =
            $"Название башни: {towerInfo.towerName}\n Цена башни: {towerInfo.towerCost}";
        Debug.Log(towerInfo.towerCost);
        Debug.Log(towerInfo.towerName);
        meshRenderer.material = mFocus;

        yield return new WaitForSeconds(0.1f);
        zoneTower.SetActive(false);
        canvasSidebar.TooltipOff();
        meshRenderer.material = mDisable;
    }
}