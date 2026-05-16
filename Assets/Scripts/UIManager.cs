using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public void LaunchGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveName(string enteredName)
    {
        DataPersistence.instanceData.currentPlayerName = enteredName;
    }
}
