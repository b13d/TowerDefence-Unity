using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] private Material mDisable;
    [SerializeField] private Material mFocus;
    public bool hasTower;
    
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
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
            meshRenderer.material = mFocus;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            meshRenderer.material = mDisable;
        }
    }
}
