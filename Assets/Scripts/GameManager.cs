using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 2f;
    public GameObject completeLevelUI;
    public GameObject pauseScreen;

    public bool paused = false;

    private bool gameHasEnded = false;

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }
    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    private void setPause(bool desiredPause)
    {
        if (desiredPause)
        {
            paused = true;
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            Time.timeScale = 1;
        }
    }
    public void togglePause()
    {
        setPause(!paused);
        pauseScreen.SetActive(paused);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
