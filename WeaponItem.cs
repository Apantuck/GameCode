using UnityEngine;

/* Alex Pantuck
 * A weaponitem is an equippable weapon
 * that instantiates a weapon prefab at
 * a given Transform when equipped.
 * 
 * Assumes that there's 4 distinct layers:
 * Player, Enemy, Player_Weapon, and Enemy_Weapon.
 * The idea being that enemy weapons dont hit other
 * enemies, and players cant hit other players
 * (not that there are any others).
 */

public class WeaponItem : EquippableItem
{
    const string WEAPON_OBJECT_TAG = "Weapon";
    const string WPN_TAG = "_WpnID";
    public static string WpnTag
    {
        get { return WPN_TAG;  }
    }

    public override GameObject Equip(Transform parent)
    {
        GameObject instance = base.Equip(parent);
        instance.tag = WEAPON_OBJECT_TAG;
        string wpnLayer = LayerMask.LayerToName(instance.layer) + "_" + WEAPON_OBJECT_TAG;
        int layer = LayerMask.NameToLayer(wpnLayer);
        if (layer != -1) instance.layer = layer;

        Weapon wpn = instance.GetComponent<Weapon>();
        if (wpn == null)
        {
            Debug.Log("WeaponItem " + gameObject.name + " doesn't have a prefab with a Weapon component");
            return null;
        }

        if (wpn.weaponAnimations != null)
            parent.root.GetComponent<Player>().AnimController = wpn.weaponAnimations;

        string saveName = parent.root.GetComponent<Killable>().SaveName;
        PlayerPrefs.SetInt(saveName + WPN_TAG, GetHashCode());

        return instance;
    }

    public override void Unequip(Transform parent)
    {
        base.Unequip(parent);
    }

    public override void Collect(int num)
    {
        base.Collect(1);
    }

    public static bool operator ==(WeaponItem e, WeaponItem f)
    {
        Item a = e, b = f;
        return a.Equals(b);
    }

    public static bool operator !=(WeaponItem e, WeaponItem f)
    {
        Item a = e, b = f;
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
