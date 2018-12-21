using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    public static GameObject DroppedItemPrefab;
    private static Dictionary<int, Item> items;
    private static Dictionary<int, Item> collectedItems;

    private void Awake()
    {
        Item.ItemCollectedHandler += OnItemCollected;
        items = new Dictionary<int, Item>();
        Item[] temp = Resources.LoadAll<Item>("Items");
        foreach (Item i in temp)
        {
            items.Add(i.GetHashCode(), i);
            Item i2 = Instantiate(i, transform);
            if (i2.HasItem) i2.Collect(0);
            else Destroy(i2.gameObject);
        }

        DroppedItemPrefab = droppedItemPrefab;
    }

    public static Item Get_Item(Item item)
    {
        Item foundItem;
        if (items.TryGetValue(item.GetHashCode(), out foundItem)) return foundItem;
        else
        {
            Debug.Log("Inventory failed to find item " + item.name);
            return null;
        }
    }

    public static Item Get_At(int hashCode)
    {
        Item foundItem;
        if (items.TryGetValue(hashCode, out foundItem)) return foundItem;
        else
        {
            Debug.Log("Inventory failed to find item with hashCode " + hashCode.ToString());
            return null;
        }
    }

    void OnItemCollected(Item i)
    {
        if (!collectedItems.ContainsKey(i.GetHashCode())) collectedItems.Add(i.GetHashCode(), i);
    }

}
