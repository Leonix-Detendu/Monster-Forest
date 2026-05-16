using UnityEngine;
using TMPro;

public class DataPersistence : MonoBehaviour
{
    [HideInInspector] public static DataPersistence instanceData { get; private set; }

    [HideInInspector] public string currentPlayerName;

    [HideInInspector] public int highestScore;
    [HideInInspector] public string playerNameWithHs;

    private void Awake()
    {
        if (instanceData == null)
        {
            instanceData = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
