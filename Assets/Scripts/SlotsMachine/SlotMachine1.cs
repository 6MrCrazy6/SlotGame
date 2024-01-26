using TMPro;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    public Slots[] slots;
    public Combinations[] combinations;

    private int money;
    private int casinoMoney;
    private int price;
    public float timeInterval = 0.025f;
    private int stoppedSlots = 3;
    
    public TextMeshProUGUI moneyText;
    public TMP_InputField priceInput;
    public Camera mainCamera;

    private bool isSpin = false;
    private bool isAuto;
    private bool isPlayer = true;
    private bool isCasino = false;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("PlayerCoins")) money = 1000;
        else money = PlayerPrefs.GetInt("PlayerCoins");

        if (!PlayerPrefs.HasKey("CasinoCoins")) casinoMoney = 1000;
        else casinoMoney = PlayerPrefs.GetInt("CasinoCoins");
    }

    private void Start()
    {
        moneyText.text = "COINS: " + money;
        Debug.Log(PlayerPrefs.GetInt("CasinoCoins"));
        mainCamera.backgroundColor = Color.blue;
    }

    public void Spin()
    {
        int newBet = int.Parse(priceInput.text);
        price = newBet;

        if (price >= 20 && price <= 100)
        {
            if (isPlayer == true && isCasino == false)
            {
                if ((!isSpin && money - price >= 0))
                {
                    ChangeMoney(-price);
                    isSpin = true;
                    foreach (Slots i in slots)
                    {
                        i.StartCoroutine("Spin");
                    }
                }
            }
            else if (isPlayer == false && isCasino == true)
            {
                if (!isSpin && casinoMoney - price >= 0)
                {
                    Debug.Log(price);
                    ChangeCasinoMoney(-price);
                    isSpin = true;
                    foreach (Slots i in slots)
                    {
                        i.StartCoroutine("Spin");
                    }
                }
            }
        }
        else
        {
            Debug.Log("Invalid bid. Enter a number between 20 and 100.");
        }
    }

    public void WaitResults()
    {
        stoppedSlots -= 1;
        if (stoppedSlots <= 0)
        {
            stoppedSlots = 3;
            CheckResults();
        }
    }

    public void CheckResults()
    {
        isSpin = false;
        foreach (Combinations i in combinations)
        {
            Debug.Log(slots[0].gameObject.GetComponent<Slots>().stoppedSlot.ToString());
            Debug.Log(i.FirstValue.ToString());
            if (slots[0].gameObject.GetComponent<Slots>().stoppedSlot.ToString() == i.FirstValue.ToString()
                && slots[1].gameObject.GetComponent<Slots>().stoppedSlot.ToString() == i.SecondValue.ToString()
                && slots[2].gameObject.GetComponent<Slots>().stoppedSlot.ToString() == i.ThirdValue.ToString())
            {
                if(isPlayer == true && isCasino == false)
                {
                    ChangeMoney(i.prize);
                }
                else if(isPlayer == false && isCasino == true)
                {
                    ChangeCasinoMoney(i.prize - price);
                }
            }
        }
        if (isAuto)
        {
            Invoke("Spin", 0.4f);
        }
    }
    private void ChangeMoney(int count)
    {
        money += count;
        moneyText.text = "COINS: " + money;
        PlayerPrefs.SetInt("PlayerCoins", money);
        PlayerPrefs.Save();
    }

    private void ChangeCasinoMoney(int count)
    {
        casinoMoney += count;
        moneyText.text = "COINS: " + casinoMoney;
        PlayerPrefs.SetInt("CasinoCoins", casinoMoney);
        PlayerPrefs.Save();
    } 

    public void ChangePlayerCasino()
    {
        if (isPlayer == true && isCasino == false)
        {
            moneyText.text = "COINS: " + money;
            mainCamera.backgroundColor = Color.black;
            isCasino = true;
            isPlayer = false;
        }
        else if(isPlayer == false && isCasino == true)
        {
            moneyText.text = "COINS: " + casinoMoney;
            mainCamera.backgroundColor = Color.blue;
            isCasino = false;
            isPlayer = true;
        }
    }

    public void SetAuto()
    {
        if (!isAuto)
        {
            timeInterval = timeInterval / 10;
            isAuto = true;
            Spin();
            WaitResults();
        }
        else
        {
            timeInterval = timeInterval * 10;
            isAuto = false;
        }
    }
}
