using UnityEngine;

public abstract class EquippableItem : Item
{
    public GameObject prefab;

    public delegate void ItemChanged();
    public static event ItemChanged EquipmentChangedHandler;
    public override void Awake()
    {
        base.Awake();
        if (prefab == null)
        {
            Debug.Log(gameObject.name + " is an equippable item with no prefab. Disabling...");
            this.enabled = false;
        }
    }

    public virtual GameObject Equip(Transform parent)
    {
        Unequip(parent);
        GameObject instance = Instantiate(prefab, parent);
        if (EquipmentChangedHandler != null) EquipmentChangedHandler();
        return instance;
    }

    public virtual void Unequip(Transform parent)
    {
        GameObject go = parent.GetChild(0).gameObject;
        if (go == null) return;
        Destroy(go);
        if (EquipmentChangedHandler != null) EquipmentChangedHandler();
    }

    public override void Collect(int num)
    {
        base.Collect(1);
    }

    public static bool operator ==(EquippableItem c, EquippableItem d)
    {
        Item a = c, b = d;
        return a.Equals(b);
    }

    public static bool operator !=(EquippableItem c, EquippableItem d)
    {
        Item a = c, b = d;
        return !a.Equals(b);
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
