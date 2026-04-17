using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [Header("Item Details")]
    public ItemData itemData;

    [Header("Proximity Glow Settings")]
    public float proximityRange = 5f;
    public float pulseSpeed = 5f;
    public Transform playerTransform;

    private Renderer itemRenderer;
    private Material itemMaterial;
    private bool isPlayerNearby = false;

    void Start()
    {
        itemRenderer = GetComponent<Renderer>();
        if (itemRenderer != null)
        {
            itemMaterial = itemRenderer.material;
            itemMaterial.SetFloat("_OutlineWidth", 0f);
        }
    }

    void Update()
    {
        if (playerTransform == null || itemMaterial == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= proximityRange)
        {
            // Sadece menzile ilk girdiðimizde konsola yazdýr
            if (!isPlayerNearby)
            {
                Debug.Log("<color=yellow>Eþyaya yaklaþýldý! Parlama kodu çalýþýyor.</color>");
                isPlayerNearby = true;
            }

            // Yanýp sönme matematiði
            float lerpValue = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;

            // DÝKKAT: HasProperty kontrolünü sildik (Unity 6 bazen bunu bug'a sokuyor)
            // Çarpaný da 2f yaptýk ki ýþýk iyice patlasýn, gözden kaçmasýn
            itemMaterial.SetFloat("_OutlineWidth", lerpValue * 2f);
        }
        else if (isPlayerNearby)
        {
            Debug.Log("<color=red>Eþyadan uzaklaþýldý. Parlama durdu.</color>");
            isPlayerNearby = false;
            itemMaterial.SetFloat("_OutlineWidth", 0f);
        }
    }

    public void OnFocus() { }
    public void OnLoseFocus() { }

    public void Interact()
    {
        if (itemData != null)
        {
            ScavengerManager.Instance.CollectItem(itemData);
            if (TimerManager.Instance != null && itemData.timeBonus > 0)
                TimerManager.Instance.AddTime(itemData.timeBonus);
        }
        Destroy(gameObject);
    }
}