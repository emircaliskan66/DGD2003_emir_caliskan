using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [Header("Item Details")]
    public ItemData itemData; // Eþyanýn kimlik kartý (ScriptableObject)

    public void OnFocus()
    {
        // Görsel parlama efektini buraya ekleyebilirsin
    }

    public void OnLoseFocus()
    {
        // Parlama efektini kapatma
    }

    public void Interact()
    {
        if (itemData != null)
        {
            // ScavengerManager'a bu eþyayý topladýðýmýzý haber ver
            ScavengerManager.Instance.CollectItem(itemData);
            Debug.Log(itemData.itemName + " toplandi!");
        }
        else
        {
            Debug.LogWarning("Bu objede ItemData eksik!");
        }

        // Eþyayý sahneden yok et
        Destroy(gameObject);
    }
}