using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events; 

public class ScavengerManager : MonoBehaviour
{
    private List<string> collectedItemNames = new List<string>();
    private List<ItemData> allItemsAtStart = new List<ItemData>();

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

    void Start()
    {
        allItemsAtStart = new List<ItemData>(itemsToFind);
        UpdateUI();
    }

    public void CollectItem(ItemData collectedItem)
    {
        if (itemsToFind.Contains(collectedItem))
        {
            itemsToFind.Remove(collectedItem);
            collectedItemNames.Add(collectedItem.itemName);
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

    public List<string> GetCollectedItemNames()
    {
        return new List<string>(collectedItemNames);
    }

    public int GetItemsFound()
    {
        return itemsFound;
    }

    public void LoadCollectedItems(List<string> loadedCollectedItems)
    {
        collectedItemNames = new List<string>(loadedCollectedItems);
        itemsFound = collectedItemNames.Count;

        itemsToFind = new List<ItemData>(allItemsAtStart);

        foreach (string collectedName in collectedItemNames)
        {
            itemsToFind.RemoveAll(item => item.itemName == collectedName);
        }

        PickupItem[] sceneItems = FindObjectsOfType<PickupItem>();

        foreach (PickupItem item in sceneItems)
        {
            if (item.itemData != null && collectedItemNames.Contains(item.itemData.itemName))
            {
                Destroy(item.gameObject);
            }
        }

        if (itemsToFind.Count == 0 && allItemsAtStart.Count > 0)
        {
            if (listText != null)
            {
                listText.text = winText;
            }

            if (TimerManager.Instance != null)
            {
                TimerManager.Instance.StopTimer();
            }

            OnAllItemsFound.Invoke();
        }
        else
        {
            UpdateUI();
        }
    }
}