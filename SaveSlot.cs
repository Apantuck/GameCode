using UnityEngine;

public class SaveSlot : Saveable
{
    public const string P_NAME_TAG = "_PlayerName",
        P_TIME_TAG = "_PlayerPlaytime",
        P_LEVEL_TAG = "_PlayerLevel";

    private string playerName;
    public string PlayerName { get; set; }
    private int playerLevel;
    public int PlayerLevel { get; set; }
    private float playTime;
    public float PlayTime { get; set; }

    public override void Awake()
    {
        base.Awake();
        playerName = PlayerPrefs.GetString(saveName + P_NAME_TAG, "Player");
        playerLevel = PlayerPrefs.GetInt(saveName + P_LEVEL_TAG, 1);
        playTime = PlayerPrefs.GetFloat(saveName + P_TIME_TAG, 0);
    }
}
