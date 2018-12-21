using UnityEngine;

/* Alex Pantuck
 * Abstract base class for items
 * Both equippment items and consumable items are "items"
 */

public abstract class Item : Saveable
{
    const string HAS_ITEM_TAG = "_HasItem";
    const string ITEM_QUANTITY_TAG = "_NumItem";

    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private Sprite icon;
    protected static GameObject droppedItemPrefab;
    protected bool hasItem;
    protected int numCollected;

    public string ItemName
    {
        get { return itemName; }
    }
    public string ItemDescription
    {
        get { return itemDescription; }
    }
    public bool HasItem
    {
        get { return hasItem; }
    }
    public int NumCollected
    {
        get { return numCollected; }
    }

    // Item collection event
    public delegate void ItemCollected(Item i);
    public static event ItemCollected ItemCollectedHandler;

    public override void Awake()
    {
        base.Awake();
        if (droppedItemPrefab == null) droppedItemPrefab = Inventory.DroppedItemPrefab;
        numCollected = PlayerPrefs.GetInt(saveName + ITEM_QUANTITY_TAG, numCollected);
        int collected = PlayerPrefs.GetInt(saveName + HAS_ITEM_TAG);
        hasItem = (collected == TRUE) ? true : false;
        if (hasItem) Destroy(gameObject);
    }

    public virtual void Collect(int quantity)
    {
        numCollected += quantity;
        hasItem = true;
        PlayerPrefs.SetInt(saveName + HAS_ITEM_TAG, TRUE);
        PlayerPrefs.SetInt(saveName + ITEM_QUANTITY_TAG, numCollected);
        if (ItemCollectedHandler != null) ItemCollectedHandler(this);
        Destroy(gameObject); 
    }

    // Dont discard more than you have (dont go negative)
    public virtual void Discard(int num)
    {
        numCollected = (num > numCollected) ? 0 : (numCollected - num);
    }

    public virtual void Drop(int num)
    {
    }

    // Operator overrides

    public static bool operator ==(Item a, Item b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Item a, Item b)
    {
        return !(a.Equals(b));
    }

    public override bool Equals(object other)
    {
        if (other == null) return false;
        var v = (Item)other;
        return string.Equals(itemName, v.itemName);
    }

    public override int GetHashCode()
    {
        int hash = new { ItemName, ItemDescription}.GetHashCode();
        return Mathf.Abs(hash);
    }
}