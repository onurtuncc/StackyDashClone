using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int score=0;
    bool isAlreadyStarted = false;
    private int dashesUnder=0;
    public float endMultiply = 0f;
    public int endScore = 0;
    public int levelGold = 0;
    public GameObject endPanel;
    public GameObject inGamePanel;
    public GameObject startPanel;
    public Text scoreText;
    public Text levelText;
    public Text bestText;
    public Text startGoldText;
    public Text endScoreText;
    public Text endMultiplyText;
    public Text levelCompletedText;
    public Text totalGoldText;
    public Text levelGoldText;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.text = score.ToString();
            
        }
    }
   
    public int DashesUnder
    {
        get
        {
            return dashesUnder;
        }
        set
        {
            dashesUnder = value;
        }
    }


    private void Start()
    {
       
        levelText.text = "Level "+(SceneManager.GetActiveScene().buildIndex+1).ToString();
        bestText.text = "Best: " + PlayerPrefs.GetInt("highScore", 0).ToString();
        startGoldText.text = PlayerPrefs.GetInt("TotalGold", 0).ToString();
    }
    public void Multiplier()
    {
        
        int newScore = (int)(score * endMultiply);
        endScore = newScore;
        levelGold = endScore / 10;
        PlayerPrefs.SetInt("TotalGold", PlayerPrefs.GetInt("TotalGold") + levelGold);
        EndPhase();
        
    }
    private void Update()
    {
       
        if (InputManager.Instance.swipeDelta!=Vector2.zero && !isAlreadyStarted)
        {
            isAlreadyStarted = true;
            GamePhase();
        }
        
    }
    public void GamePhase()
    {
        startPanel.SetActive(false);
        inGamePanel.SetActive(true);
        
    }
    public void EndPhase()
    {
        if (PlayerPrefs.GetInt("highScore") < endScore)
        {
            PlayerPrefs.SetInt("highScore", endScore);
        }
        levelText.enabled = false;
        inGamePanel.SetActive(false);
        endPanel.SetActive(true);
        endScoreText.text = endScore.ToString();
        endMultiplyText.text = endMultiply.ToString("n1")+"x";
        levelCompletedText.text = "Level "+(SceneManager.GetActiveScene().buildIndex+1).ToString()+" Completed";
        totalGoldText.text = PlayerPrefs.GetInt("TotalGold").ToString();
        levelGoldText.text = levelGold.ToString();
    }
    public void ToNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("atLevel", sceneIndex + 2);
        SceneManager.LoadScene(sceneIndex + 1);
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    

}
