using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    
    public GameObject gameoverText;
    public GameObject healthCanvas;
    public GameObject countdownBar;
    public GameObject _killText;
    public Text scoreText;
    public Text KillText;
    public Text recordText;
    private int _score = 0;
    public int score { 
        get { return _score; }
        set { _score = value;
            scoreText.text = "Score : " + _score.ToString();    
        }
    }
    public bool isGameover; //게임오버 상태

    public QTESys qTESys;
    [SerializeField] public HealthController healthController;

    public void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        isGameover = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isGameover)
        {
            KillText.text = "Kill : " + qTESys.DefeatedMonster;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        if (healthController.PlayerHealth == 0)
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);
        healthCanvas.SetActive(false);
        //qt_Button.SetActive(false);
        countdownBar.SetActive(false);
        _killText.SetActive(false);


        int bestKill = PlayerPrefs.GetInt("BestKill");
        int bestScore = PlayerPrefs.GetInt("BestScore");

        if(qTESys.DefeatedMonster > bestKill)
        {
            bestKill = qTESys.DefeatedMonster;
            PlayerPrefs.SetInt("BestKill", bestKill);
        }

        if(_score > bestScore)
        {
            bestScore = _score;
            PlayerPrefs.SetInt("BEstScore", bestScore);
        }
        recordText.text = "Best Kill : " + bestKill + "\n Best Score : " + bestScore;
        
    }
}
