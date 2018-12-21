using System.Collections;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // seconds between autosaves
    [SerializeField] private int autoSaveInterval = 600;
    [SerializeField] private int maxSaveSlots = 6;
    private static int saveSlot = 0;
    public static int SaveSlot { get; set; }

    public int MaxSaveSlots
    {
        get { return maxSaveSlots; }
    }

    public delegate void OnAutoSave();
    public static OnAutoSave OnAutoSaveHandler;

    private void Start()
    {
        // Save conditions
        EquippableItem.EquipmentChangedHandler += Save_Game;
        Item.ItemCollectedHandler += Save_Game;
        //ConsumableItem.ItemUsedHandler += Save_Game;
        //Boss.BossBeganHandler += Save_Game
        Player.ExpChangedHandler += Save_Game;
        //Menu.MenuOpenedHandler += Save_Game;
        //Settings.SettingsChangedHandler += Save_Game;
        //Dialogue.DialogueFinishedHandler += Save_Game;
        //Interactable.InteractionHandler += Save_Game;
    }

    private void Update()
    {
        AutoSave();
    }



    // save slot, object name, and the square magnitude of its position upon spawning
    public static string getSaveName(GameObject obj)
    {
        string name = saveSlot.ToString() + "_" + obj.name + "_" + 
            obj.transform.position.x.ToString() +
            obj.transform.position.y.ToString() +
            obj.transform.position.z.ToString();
        return name;
    }

    // Because player doesn't spawn in the same location everytime and thee's only one,
    // it has this unique ID
    public static string getPlayerSaveName()
    {
        string name = saveSlot.ToString() + "_Player";
        return name;
    }

    // Save Conditions

    IEnumerator AutoSave()
    {
        yield return new WaitForSeconds(autoSaveInterval);
        Save();
    }

    private void Save_Game(object o)
    {
        Save();
    }

    private void Save_Game()
    {
        Save();
    }

    private void OnLevelWasLoaded(int level)
    {
        Save();
    }

    private void Save()
    {
        if (OnAutoSaveHandler != null) OnAutoSaveHandler();
        PlayerPrefs.Save();
    }
}
