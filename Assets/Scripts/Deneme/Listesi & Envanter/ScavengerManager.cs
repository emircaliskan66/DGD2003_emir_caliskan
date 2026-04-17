using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events; 

public class ScavengerManager : MonoBehaviour
{
    public static ScavengerManager Instance;

    [Header("List Settings")]
    public List<ItemData> itemsToFind = new List<ItemData>();
    private int itemsFound = 0;

    [Header("UI Elements")]
    public TextMeshProUGUI listText;

    [Header("UI Text Settings")]
    public string titleText = "Items to Find:";
    public string foundText = "Found: ";
    public string winText = "ALL ITEMS FOUND!\nGo to the Exit!";

    [Header("Events")]
    public UnityEvent OnAllItemsFound;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start() { UpdateUI(); }

    public void CollectItem(ItemData collectedItem)
    {
        if (itemsToFind.Contains(collectedItem))
        {
            itemsToFind.Remove(collectedItem);
            itemsFound++;
            UpdateUI();

            if (TimerManager.Instance != null && collectedItem.timeBonus > 0)
            {
                TimerManager.Instance.AddTime(collectedItem.timeBonus);
            }

            if (itemsToFind.Count == 0) 
            {
                if (listText != null) listText.text = winText;
                if (TimerManager.Instance != null) TimerManager.Instance.StopTimer();

                OnAllItemsFound.Invoke();
            }
        }
    }

    void UpdateUI()
    {
        if (listText != null && itemsToFind.Count > 0)
        {
            listText.text = titleText + "\n";
            foreach (var item in itemsToFind)
            {
                string displayName = string.IsNullOrEmpty(item.itemName) ? item.name : item.itemName;
                listText.text += "- " + displayName + "\n";
            }
            listText.text += "\n" + foundText + itemsFound;
        }
    }
}