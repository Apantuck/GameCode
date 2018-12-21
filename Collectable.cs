using UnityEngine;
using UnityEngine.UI;

/* Alex Pantuck
 * A "collectable" is an item you find in the game world, and can
 * collect to bring it into your inventory. When the item is
 * collected, the gameobject is destroyed
 */

public class Collectable : MonoBehaviour, IInteractable
{
    public Item item;
    public string itemCollectionPrompt = "Collect";
    private bool canInteract = false;
	
    void Awake()
    {
        itemCollectionPrompt += " " + item.ItemName + "?";
    }
	
	void Update () {
		
	}

    public void Interact()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canInteract = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canInteract = false;
    }

    void OnGUI()
    {

    }

}
