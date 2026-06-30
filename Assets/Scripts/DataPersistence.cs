using TMPro;
using UnityEditor.Overlays;
using UnityEngine;
using System.IO;

public class DataPersistence : MonoBehaviour
{
    [HideInInspector] public static DataPersistence instanceData { get; private set; }

    [HideInInspector] public string currentPlayerName;

    [HideInInspector] public int highestScore;
    [HideInInspector] public string playerNameWithHs;

    private string savePath;

    private void Awake()
    {
        if (instanceData == null)
        {
            instanceData = this;
            savePath = Path.Combine(Application.persistentDataPath, "savefile.json");
            LoadGame();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.highestScore = highestScore;
        data.playerNameWithHs = playerNameWithHs;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highestScore = data.highestScore;
            playerNameWithHs = data.playerNameWithHs;
        }
    }
}
