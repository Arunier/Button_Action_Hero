using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{
    public enum SceneNum
    {
        MainMenu,
        FightScene
    }


    public void GoNext()
    {
        SceneManager.LoadScene((int)SceneNum.FightScene);
    }
}
