using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public GameObject healthCanvas;
    public GameObject qt_Button;
    public GameObject countdownBar;
    public GameObject _killText;
    public Text KillText;
    public Text recordText;

    private bool isGameover; //게임오버 상태

    public QTESys qTESys;
    [SerializeField] public HealthController healthController;


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
        qt_Button.SetActive(false);
        countdownBar.SetActive(false);
        _killText.SetActive(false);


        int bestKill = PlayerPrefs.GetInt("BestKill");

        if(qTESys.DefeatedMonster > bestKill)
        {
            bestKill = qTESys.DefeatedMonster;
            PlayerPrefs.SetFloat("BestKill", bestKill);
        }

        recordText.text = "Best Kill : " + bestKill;
    }
}
