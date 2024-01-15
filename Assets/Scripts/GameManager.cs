using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    float money = 0f;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        moneyText.text = $"Money: {money}";
    }
    public void SellProduct(float value)
    {
        money += value;
        moneyText.text = $"Money: {money}";

    }

    public void ReStock(float value)
    {
        money -= value;
        moneyText.text = $"Money: {money}";
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
