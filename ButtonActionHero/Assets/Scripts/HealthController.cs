using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public int PlayerHealth;

    [SerializeField] private Image[] hearts;

    public void UpdateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < PlayerHealth)
            {
                hearts[i].color = Color.white;
            }
            else
            {
                hearts[i].color = Color.black;
            }
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        UpdateHealth();
    }

    private void Update()
    {
        UpdateHealth();
    }
}
