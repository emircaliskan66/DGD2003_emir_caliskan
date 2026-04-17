using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scavenger Hunt/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public float timeBonus = 0f; 
}