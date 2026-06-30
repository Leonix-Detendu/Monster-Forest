using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI BestScore;

    public void LaunchGame()
    {
        StartCoroutine(LaunchGameRoutine());
    }

    public void SaveName(string enteredName)
    {
        DataPersistence.instanceData.currentPlayerName = enteredName;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        DataPersistence.instanceData.SaveGame();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Awake()
    {
        if (BestScore != null )
        {
            if (DataPersistence.instanceData.highestScore == 0)
            {
                BestScore.text = "The Best Score will be displayed here";
            }
            else
            {
                BestScore.text = "Best Score : " + DataPersistence.instanceData.highestScore + " by " + DataPersistence.instanceData.playerNameWithHs;
            }  
        }
    }

    IEnumerator LaunchGameRoutine()
    {
        yield return new WaitForSeconds(0.4f);

        SceneManager.LoadScene(1);
    }
}
