using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameInfo : MonoBehaviour
{
    public TextMeshProUGUI killsTMP;
    public TextMeshProUGUI playerHealthTMP;
    public TextMeshProUGUI playerScoreTMP;
    private int enemyKills = 0;
    private float healthPercent = 100f;

    void Start()
    {
        killsTMP.text = "Kills: " + enemyKills;
        playerHealthTMP.text = "Health: 100%";
        // playerScoreTMP.text = "Your Score: 0";
    }

    void LateUpdate()
    {
        killsTMP.text = "Kills: " + enemyKills;
        updateHealthText();
    }
    public void incrementKill()
    {
        this.enemyKills++;
    }

    public void updatehealth(float healthPercent)
    {
        this.healthPercent = healthPercent;
    }

    private void updateHealthText()
    {
        playerHealthTMP.text = "Health: " + this.healthPercent + "%";
        if (this.healthPercent >= 70)
        {
            playerHealthTMP.color = Color.green;
        }
        else if (this.healthPercent > 30)
        {
            playerHealthTMP.color = Color.yellow;
        }
        else
        {
            playerHealthTMP.color = Color.red;
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;

    }

    public void gameOver()
    {
        Transform gameOverPanelTransform = transform.Find("GameOverPanel");
        gameOverPanelTransform.gameObject.SetActive(true);
        playerScoreTMP.text = "Your Score: " + this.enemyKills;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    public void quit()
    {
        Application.Quit();
    }
}
