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
    public GameObject _timeText;
    public Text timeText;
    public Text recordText;

    private float surviveTime; //생존 시간
    private bool isGameover; //게임오버 상태
    [SerializeField] public HealthController healthController;


    // Start is called before the first frame update
    void Start()
    {
        surviveTime = 0;
        isGameover = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isGameover)
        {
            surviveTime += Time.deltaTime;
            timeText.text = "Time : " + surviveTime.ToString("F2");
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
        _timeText.SetActive(false);


        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if(surviveTime > bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        recordText.text = "Best Time : " + bestTime.ToString("F2");
    }
}
