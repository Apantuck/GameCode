using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public GameObject bloodPrefab;
    private BodyPartManager manager;
    
    // for CriticalBodyPart implementation
    protected int dmgMod = 1;

    private void Awake()
    {
        manager = GetComponentInParent<BodyPartManager>();

        if (manager == null)
        {
            Debug.Log(transform.root.name + " has a bodypart but not BodyPartManager. Disabling BodyPart...");
            this.enabled = false;
        }
    }

    // When a weapon enters the collider of this body part, contact the body part manager. If the manager hasn't
    // already been contacted by another bodypart for this swing, increment the weapons swing count.
    private void OnCollisionEnter(Collision collision)
    {
        Weapon wpn = collision.gameObject.GetComponent<Weapon>();
        if (wpn == null) return;
        if (manager.Bodypart_Hit(wpn.SwingNum, wpn.damage * dmgMod)) wpn.Add_Swing();

        // Spawn blood prefab at collision point
        if (bloodPrefab != null)
        {
            Vector3 spawnPoint = collision.contacts[0].point;
            Quaternion dir = Quaternion.LookRotation(collision.contacts[0].normal, transform.up);
            Instantiate(bloodPrefab, spawnPoint, dir, transform);
        }
    }
}
