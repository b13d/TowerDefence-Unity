using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] private Material mDisable;
    [SerializeField] private Material mFocus;
    [SerializeField] CanvasSidebar canvasSidebar;
    public bool hasTower;
    
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        canvasSidebar = FindObjectOfType<CanvasSidebar>();
    }

    public void SetTower(GameObject tower)
    {
        Debug.Log("SetTower func");
        
        hasTower = true;
        Instantiate(tower, transform.position, Quaternion.identity);
        Destroy(gameObject); // возможно не нужно так делать для будущих башен, после одной этой установленной
        
        Debug.Log("hasTower: " + hasTower);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            TowerInfo towerInfo = other.gameObject.GetComponent<TowerDisabled>().GetTowerInfo();

            canvasSidebar.TextTooltip.text = $"Название башни: {towerInfo.towerName}\n Цена башни: {towerInfo.towerCost}";
            Debug.Log(towerInfo.towerCost);
            Debug.Log(towerInfo.towerName);
            meshRenderer.material = mFocus;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            canvasSidebar.TooltipOff();
            meshRenderer.material = mDisable;
        }
    }
}
