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
            SetOutline(0); 
        }
    }

    void Update()
    {
        if (playerTransform == null || itemMaterial == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= proximityRange)
        {
            isPlayerNearby = true;
            HandlePulsingEffect();
        }
        else if (isPlayerNearby)
        {
            isPlayerNearby = false;
            SetOutline(0);
        }
    }

    void HandlePulsingEffect()
    {
        float lerpValue = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
        SetOutline(lerpValue);
    }

    void SetOutline(float intensity)
    {
        if (itemMaterial.HasProperty("_OutlineWidth"))
        {
            itemMaterial.SetFloat("_OutlineWidth", intensity * 0.5f);
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
            {
                TimerManager.Instance.AddTime(itemData.timeBonus);
            }
        }

        Destroy(gameObject);
    }
}