using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI casinoBalanceText;
    [SerializeField] private TextMeshProUGUI informationText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button startButton;

    private int casinoBalance;

    private bool isBuySkin;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("CasinoCoins")) casinoBalance = 1000;
        else casinoBalance = PlayerPrefs.GetInt("CasinoCoins");

        if (!PlayerPrefs.HasKey("PixelSkin")) isBuySkin = false;
        else isBuySkin = true;
    }

    private void Start()
    {
        casinoBalanceText.text = "" + casinoBalance;

        if (isBuySkin == true) 
        {
            informationText.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
        }
        else
        {
            informationText.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(false);
        }
    }

    public void PlaStandartSkinMachine()
    {
        SceneManager.LoadScene("Machine 1");
    }

    public void PlayNewMachine()
    {
        SceneManager.LoadScene("Machine 2");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BuyNewMachine()
    {
        if(casinoBalance > 500)
        {
            casinoBalance -= 500;
            isBuySkin = true;
            PlayerPrefs.SetInt("CasinoCoins", casinoBalance);
            PlayerPrefs.SetInt("PixelSkin", 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("GameClose");
    }
}