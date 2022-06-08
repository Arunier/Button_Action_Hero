using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTESys : MonoBehaviour
{
    public Transform displayBoxPannel;
    public GameObject DisplayBoxParentParentPrefab;
    GameObject[] DisplayBox = new GameObject[8]; //눌러야하는 키를 표시
    public GameObject Passbox;
    public GameObject DamageEffectPanel;
    public int QTEGen;
    public int keyDownCount;
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

    KeyCode needToDown;
    // Start is called before the first frame update
    void Start()
    {
        Being(Duration);
        isQTEClear = false;
    }

    //QTE 시스템
    KeyCode[] keyList = new KeyCode[8];
    int count = 0;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("count = " + count);  
        Debug.Log(keyList[count]);
        if (gameManager.isGameover == true) return;
        if(WaitingForKey == 0)
        {
            keyDownCount = Random.Range(1, 9);

            for(int i = 0; i < keyDownCount; i++)
            {

                QTEGen = Random.Range(1, 5); //1부터 4까지 난수 생성
                DisplayBox[i] = Instantiate(DisplayBoxParentParentPrefab, transform.position, Quaternion.identity, displayBoxPannel);
                if(QTEGen == 1)
                {
                    WaitingForKey = 1;
                    DisplayBox[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "W";
                    needToDown = KeyCode.W;
                }
                if(QTEGen == 2)
                {
                    WaitingForKey = 1;
                    DisplayBox[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
                    needToDown = KeyCode.A;
                }
                if(QTEGen == 3)
                {
                    WaitingForKey = 1;
                    DisplayBox[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "S";
                    needToDown = KeyCode.S;
                }
                if(QTEGen == 4)
                {
                    WaitingForKey = 1;
                    DisplayBox[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "D";
                    needToDown = KeyCode.D;
                }

                keyList[i] = needToDown;
            }
        }
        #region comment
        //키 입력받는 부분
        //if(QTEGen == 1)
        //{
        //    if(Input.anyKeyDown)
        //    {
        //        if(Input.GetButtonDown("WKey"))
        //        {
        //            CorrectKey = 1;
        //            StartCoroutine(KeyPressing());
        //        }
        //        else
        //        {
        //            CorrectKey = 2;
        //            StartCoroutine(KeyPressing());
        //        }
        //    }
        //}
        //if (QTEGen == 2)
        //{
        //    if (Input.anyKeyDown)
        //    {
        //        if (Input.GetButtonDown("AKey"))
        //        {
        //            CorrectKey = 1;
        //            StartCoroutine(KeyPressing());
        //        }
        //        else
        //        {
        //            CorrectKey = 2;
        //            StartCoroutine(KeyPressing());
        //        }
        //    }
        //}
        //if (QTEGen == 3)
        //{
        //    if (Input.anyKeyDown)
        //    {
        //        if (Input.GetButtonDown("SKey"))
        //        {
        //            CorrectKey = 1;
        //            StartCoroutine(KeyPressing());
        //        }
        //        else
        //        {
        //            CorrectKey = 2;
        //            StartCoroutine(KeyPressing());
        //        }
        //    }
        //}
        //if (QTEGen == 4)
        //{
        //    if (Input.anyKeyDown)
        //    {
        //        if (Input.GetButtonDown("DKey"))
        //        {
        //            CorrectKey = 1;
        //            StartCoroutine(KeyPressing());
        //        }
        //        else
        //        {
        //            CorrectKey = 2;
        //            StartCoroutine(KeyPressing());
        //        }
        //    }
        //}
        #endregion

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(keyList[count]))//정확한 키를 입력했을때
            {
                CorrectKey = 1;
                DisplayBox[count].transform.GetChild(0).GetComponent<Image>().color = Color.black;
                SoundManager.instance.PlayKeyDownSound();
                StartCoroutine(KeyPressing());
            }
            else
            {
                CorrectKey = 2;
                SoundManager.instance.WrongKeyDownSound();
                StartCoroutine(KeyPressing());
            }
        }


    }


    IEnumerator KeyPressing()
    {

        QTEGen = 5;
        if(CorrectKey == 1) //입력한 키가 맞을때
        {
            if(count < keyDownCount - 1)
            {
                count++;
            }
            else if(count == keyDownCount - 1)
            {
                count = 0;
                Passbox.GetComponent<Text>().text = "CLEAR!";
                yield return new WaitForSeconds(.5f);
                CorrectKey = 0;
                DefeatedMonster++;
                Passbox.GetComponent<Text>().text = "";
                for(int i = 0; i < 8; i++)
                {
                    if (DisplayBox[i] != null)
                    {
                        Destroy(DisplayBox[i]);
                        DisplayBox[i] = null;
                      
                    }
                }
                yield return new WaitForSeconds(.3f);
                WaitingForKey = 0;
                enemyRandraw.Draw();
                GameManager.instance.score += keyDownCount * 10;
            }
        }
        if (CorrectKey == 2)// 입력한 키가 틀릴때
        {
            count = 0;
            Passbox.GetComponent<Text>().text = "FAIL!";
            healthController.PlayerHealth -= 1; //플레이어의 체력 1 감소
            //Debug.Log("틀린판정");
            DamageEffectPanel.SetActive(true);
            EnemyRandraw.instance.EnemyImage.transform.localScale = new Vector3 (10,10,1);
            yield return new WaitForSeconds(.5f);
            CorrectKey = 0;
            Passbox.GetComponent<Text>().text = "";
            for (int i = 0; i < 8; i++)
            {
                if (DisplayBox[i] != null)
                {
                    Destroy(DisplayBox[i]);
                    DisplayBox[i] = null;
                }
            }
            GameManager.instance.score -= 100;
            DamageEffectPanel.SetActive(false);
            EnemyRandraw.instance.EnemyImage.transform.localScale = new Vector3(5, 5, 1);
            yield return new WaitForSeconds(.3f);
            WaitingForKey = 0;
        }
    }

    //타이머
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
