using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRandraw : MonoBehaviour
{

    public static EnemyRandraw instance;

    public Image EnemyImage;

    public Sprite Image1;
    public Sprite Image2;
    public Sprite Image3;
    public Sprite Image4;
    public Sprite Image5;
    public Sprite Image6;
    public Sprite Image7;
    public Sprite Image8;
    public Sprite Image9;
    public Sprite Image10;

    public static int RandomInt;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        Draw();
    }
    void Update()
    {
    }

    public void Draw()
    {
        RandomInt = Random.Range(1, 10);
        if(RandomInt == 1)
        {
            EnemyImage.sprite = Image1;
        }
        else if (RandomInt == 2)
        {
            EnemyImage.sprite = Image2;
        }
        else if (RandomInt == 3)
        {
            EnemyImage.sprite = Image3;
        }
        else if (RandomInt == 4)
        {
            EnemyImage.sprite = Image4;
        }
        else if (RandomInt == 5)
        {
            EnemyImage.sprite = Image5;
        }
        else if (RandomInt == 6)
        {
            EnemyImage.sprite = Image6;
        }
        else if (RandomInt == 7)
        {
            EnemyImage.sprite = Image7;
        }
        else if (RandomInt == 8)
        {
            EnemyImage.sprite = Image8;
        }
        else if (RandomInt == 9)
        {
            EnemyImage.sprite = Image9;
        }
        else if (RandomInt == 10)
        {
            EnemyImage.sprite = Image10;
        }
    }
}
