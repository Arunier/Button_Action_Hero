using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTESys : MonoBehaviour
{
    public GameObject DisplayBox; //눌러야하는 키를 표시
    public GameObject Passbox; 
    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int DefeatedMonster;
    public int Score;
    public bool isQTEClear;
    public int Duration;
    public float remainingDuration;

    public EnemyRandraw enemyRandraw;
    public GameManager gameManager;
    [SerializeField] public HealthController healthController;
    [SerializeField] private Image uiFill;

    // Start is called before the first frame update
    void Start()
    {
        Being(Duration);
        isQTEClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(WaitingForKey == 0)
        {
            QTEGen = Random.Range(1, 5); //1부터 4까지 난수 생성
            //Being(Duration);

            if(QTEGen == 1)
            {
                WaitingForKey = 1;
                DisplayBox.GetComponent<Text>().text = "W";
            }
            if(QTEGen == 2)
            {
                WaitingForKey = 1;
                DisplayBox.GetComponent<Text>().text = "A";
            }
            if(QTEGen == 3)
            {
                WaitingForKey = 1;
                DisplayBox.GetComponent<Text>().text = "S";
            }
            if(QTEGen == 4)
            {
                WaitingForKey = 1;
                DisplayBox.GetComponent<Text>().text = "D";
            }
        }

        //키 입력받는 부분
        if(QTEGen == 1)
        {
            if(Input.anyKeyDown)
            {
                if(Input.GetButtonDown("WKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("AKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 3)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("SKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 4)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("DKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

        
    }


    IEnumerator KeyPressing()
    {
        QTEGen = 5;
        if(CorrectKey == 1) //입력한 키가 맞을때
        {
            Passbox.GetComponent<Text>().text = "CLEAR!";
            yield return new WaitForSeconds(.5f);
            CorrectKey = 0;
            DefeatedMonster++;
            Passbox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(.3f);
            WaitingForKey = 0;
            enemyRandraw.Draw();
        }
        if (CorrectKey == 2)// 입력한 키가 틀릴때
        {
            Passbox.GetComponent<Text>().text = "FAIL!";
            healthController.PlayerHealth -= 1; //플레이어의 체력 1 감소
            //Debug.Log("틀린판정");
            yield return new WaitForSeconds(.5f);
            CorrectKey = 0;
            Passbox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(.3f);
            WaitingForKey = 0;
        }
    }

    //IEnumerator CountDown() //시간안에 키를 입력하지 못했을때
    //{
    //    Debug.Log("카운트다운 시작");
    //    yield return new WaitForSeconds(3.5f);
    //    if(CountingDown == 1)
    //    {
    //        QTEGen = 5;
    //        CountingDown = 2;
    //        Passbox.GetComponent<Text>().text = "FAIL!";
    //        healthController.PlayerHealth -= 1; //플레이어의 체력 1 감소
    //        Debug.Log("시간초과");
    //        yield return new WaitForSeconds(1.0f);
    //        CorrectKey = 0;
    //        Passbox.GetComponent<Text>().text = "";
    //        DisplayBox.GetComponent<Text>().text = "";
    //        yield return new WaitForSeconds(1.0f);
    //        WaitingForKey = 0;
    //        CountingDown = 1;
    //    }
    //}

    private void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer() //거의 62초정도 됨
    {
        while(remainingDuration >= 0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0,Duration, remainingDuration);
            remainingDuration -= 0.02f;
            yield return new WaitForSeconds(0.04f);

        }

        remainingDuration = 0;

        if(remainingDuration == 0)
        {
            gameManager.EndGame();
        }

    }

}
