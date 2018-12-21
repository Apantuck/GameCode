using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : Saveable
{
    const string POS_X_TAG = "_PosX", POS_Y_TAG = "_PosY", POS_Z_TAG = "_PosZ";
    const string ROT_W_TAG = "_RotW", ROT_X_TAG = "_RotX", ROT_Y_TAG = "_RotY", ROT_Z_TAG = "_RotZ";

    [SerializeField] private GameObject player;
    [SerializeField] Transform defualtSpawnPoint;
    private static GameObject playerInstance;

    // replace these when proper scene/spawn manager is made
    private static Transform curSpawnPoint;
    public static Transform CurSpawnPoint { get; set; }

    public delegate void PlayerSpawned(GameObject player);
    public static event PlayerSpawned OnPlayerSpawnedHandler;

    public override void Awake()
    {
        base.Awake();
        SaveManager.OnAutoSaveHandler += RecordPlayerPosition;
    }

    private void OnLevelWasLoaded(int level)
    {
        Player player = this.player.GetComponent<Player>();
        saveName = player.SaveName;
        int playerDied = PlayerPrefs.GetInt(saveName + Player.DiedTag, FALSE);
        if (playerDied == TRUE) returnToSpawnPoint();
        else returnToLastPos();
    }

    // Where the current spawnpoint is
    private void returnToSpawnPoint()
    {
        playerInstance = Spawnpoint.curSpawnPoint.Respawn(player);
        if (OnPlayerSpawnedHandler != null) OnPlayerSpawnedHandler(playerInstance);
        PlayerPrefs.SetInt(saveName + Player.DiedTag, FALSE);
    }

    // Where the player was at the last game save
    // (Or at the defualt spawn if this is the start of the game)
    private void returnToLastPos()
    {
        Vector3 defualtPos = defualtSpawnPoint.position;
        float x = PlayerPrefs.GetFloat(saveName + POS_X_TAG, defualtPos.x);
        float y = PlayerPrefs.GetFloat(saveName + POS_Y_TAG, defualtPos.x);
        float z = PlayerPrefs.GetFloat(saveName + POS_Z_TAG, defualtPos.x);
        Vector3 pos = new Vector3(x, y, z);

        Quaternion defualtRos = defualtSpawnPoint.rotation;
        float rw = PlayerPrefs.GetFloat(saveName + ROT_W_TAG, defualtRos.w);
        float rx = PlayerPrefs.GetFloat(saveName + ROT_X_TAG, defualtRos.x);
        float ry = PlayerPrefs.GetFloat(saveName + ROT_Y_TAG, defualtRos.y);
        float rz = PlayerPrefs.GetFloat(saveName + ROT_Z_TAG, defualtRos.z);
        Quaternion rot = new Quaternion(rx, ry, rz, rw);

        playerInstance = Instantiate(player, pos, rot);
        if (OnPlayerSpawnedHandler != null) OnPlayerSpawnedHandler(playerInstance);
    }

    // Write current player positiona and rotation to file
    private void RecordPlayerPosition()
    {
        Vector3 pos = playerInstance.transform.position;
        PlayerPrefs.SetFloat(saveName + POS_X_TAG, pos.x);
        PlayerPrefs.SetFloat(saveName + POS_Y_TAG, pos.y);
        PlayerPrefs.SetFloat(saveName + POS_Z_TAG, pos.z);

        Quaternion rot = playerInstance.transform.rotation;
        PlayerPrefs.SetFloat(saveName + ROT_X_TAG, rot.x);
        PlayerPrefs.SetFloat(saveName + ROT_Y_TAG, rot.y);
        PlayerPrefs.SetFloat(saveName + ROT_Z_TAG, rot.z);
        PlayerPrefs.SetFloat(saveName + ROT_W_TAG, rot.w);
    }
}
