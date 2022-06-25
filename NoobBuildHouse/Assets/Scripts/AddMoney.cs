using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

using UnityEngine;
using UnityEngine.UI;
public class AddMoney : MonoBehaviour
{
    public double Money;
    public double Money2;
    public string money;
    public double[] minusMoney;
    public double[] minusMoney2;
    public long[] addMinusMoney;
    public long[] countLevel;
    public int[] countLevel2;
    public Text moneyText;
    public Text[] UpgradeText;
    public Text[] PriceNewLevel;
    public Text[] YlytsitText;
    public Text[] Doxod;
    public GameObject[] Panel;
    public GameObject[] Player;
    public GameObject noMoneyText;  
    public GameObject UpgradeImage;
    bool isOpenUpgradeImage = false;
    public bool[] isUpgrade;
    bool isCheckMoney = false;
    public int[] count;
    public long[] priceBuyNewLayer;
    public long[] priceBuyNewLayer2;
    public string[] priceText;
    string YlytsitBykvaText;

    private void Start()
    {
        isCheckMoney = false;
        StartCoroutine(AddMoneyLevel());
        countLevel[0] = 10;
        for (int i = 0; i <= count.Length - 2; i++)
        {
            count[i] = 1;
            switch (priceBuyNewLayer[i])
            {
                case 1000:
                case 10000:
                case 300000:
                    priceBuyNewLayer2[i] = priceBuyNewLayer[i] / 1000;
                    priceText[i] = "К";
                    break;
                case 3000000:
                case 70000000:
                case 500000000:
                    priceBuyNewLayer2[i] = priceBuyNewLayer[i] / 1000000;
                    priceText[i] = "М";
                    break;
                case 10000000000:
                case 250000000000:
                    priceBuyNewLayer2[i] = priceBuyNewLayer[i] / 1000000000;
                    priceText[i] = "B";
                    break;
            }
           
            PriceNewLevel[i].text = "Открыть: " + priceBuyNewLayer2[i] + priceText[i];
        }
    }

    void Update()
    {
        CheckMoney();
        CheckUpgrade2();        
    }

    public void AddMoneyLevel(int number)
    {
        Money += countLevel[number];
    }

    public void BuyUpgrade(int number)
    {
        if (Money >= addMinusMoney[number])
        {
            Money -= addMinusMoney[number];
        }
    }

    public void BuyNewLayer(int number)
    {
        if (Money >= priceBuyNewLayer[number])
        {
            switch (priceBuyNewLayer[number])
            {
                case 1000: 
                    Panel[0].SetActive(false);
                    Player[0].SetActive(true);
                    countLevel[1] = 50;
                    break;
                case 10000:
                    Panel[1].SetActive(false);
                    Player[1].SetActive(true);
                    countLevel[2] = 250;
                    break;
                case 300000:
                    Panel[2].SetActive(false);
                    Player[2].SetActive(true);
                    countLevel[3] = 15000;
                    Debug.Log("123");

                    break;
                case 3000000:
                    Panel[3].SetActive(false);
                    Player[3].SetActive(true);
                    countLevel[4] = 150000;
                    break;
                case 70000000:
                    Panel[4].SetActive(false);
                    Player[4].SetActive(true);
                    countLevel[5] = 3500000;
                    break;
                case 500000000:
                    Panel[5].SetActive(false);
                    Player[5].SetActive(true);
                    countLevel[6] = 25000000;
                    break;
                case 10000000000:
                    Panel[6].SetActive(false);
                    Player[6].SetActive(true);
                    countLevel[7] = 500000000;
                    break;
                case 250000000000:
                    Panel[7].SetActive(false);
                    Player[7].SetActive(true);
                    countLevel[8] = 12500000000;
                    break;
            }
            Money -= priceBuyNewLayer[number];
        }
        else
        {
            StartCoroutine(OpenNoMoney());
        }
    }

    public void OpenUpgradeImage()
    {
        if (isOpenUpgradeImage == false)
        {
            UpgradeImage.SetActive(true);
            isOpenUpgradeImage = true;
        }
        else
        {
            UpgradeImage.SetActive(false);
            isOpenUpgradeImage = false;
        }
    }

    public void MinusUpgradeMoneyInSek(int number)
    {
        if (Money >= minusMoney[number] && isUpgrade[number] == false)
        {
            if (count[number] <= 5)
            {
                count[number]++;
                countLevel[number] += countLevel2[number];
                Money -= minusMoney[number];
                minusMoney[number] += addMinusMoney[number];
            }
            if (count[number] == 6)
            {
                isUpgrade[number] = true;
                UpgradeText[number].text = "max";
            }
        }      
    }

    void CheckMoney()
    {
        if (Money >= 1000 && Money < 1000000)
        {
            money = "К";
            Money2 = Math.Round((float)(Money / 1000), 2);
            isCheckMoney = true;
        }
        else if (Money > 1000000 && Money < 1000000000)
        {
            money = "М";
            Money2 = Math.Round(((float)(Money / 1000000)), 2);
            isCheckMoney = true;
        }
        else if (Money > 1000000000 /*&& Money < 1000000000000*/)
        {
            money = "B";
            Money2 = Math.Round(((float)(Money / 1000000000)), 2);
            isCheckMoney = true;
        }
        else
        {
            isCheckMoney = false;
            money = "";
        }
        if (isCheckMoney == true)
        {
            moneyText.text = Money2.ToString() + money;
        }
        else
        {
            moneyText.text = Money.ToString() + money;
        }
    }
   
    void CheckUpgrade2()
    {
        for (int i = 0; i < isUpgrade.Length; i++)
        {
            if (minusMoney[i] >= 1000 && minusMoney[i] < 1000000)
            {
                minusMoney2[i] = minusMoney[i] / 1000;
                YlytsitBykvaText = "К";
            }
            if (minusMoney[i] >= 1000000 && minusMoney[i] < 1000000000)
            {
                minusMoney2[i] = minusMoney[i] / 1000000;
                YlytsitBykvaText = "М";
            }
            if (minusMoney[i] >= 1000000000 )
            {
                minusMoney2[i] = minusMoney[i] / 1000000000;
                YlytsitBykvaText = "B";
            }
            if (isUpgrade[i] == false)
            {
                UpgradeText[i].text = count[i] + "/6";
                YlytsitText[i].text = "УЛУЧШИТЬ: " + minusMoney2[i] + YlytsitBykvaText;
                Doxod[i].text = "ДОХОД: " + countLevel[i] + "/УДАР";
            }
            else
            {
                YlytsitText[i].text = "УЛУЧШИТЬ: " + " МАКС";
            }
        }
    }

    IEnumerator AddMoneyLevel()
    {
        while (true)
        {
            for (int i = 0; i < countLevel.Length; i++)
            {
                Money += countLevel[i];
            }
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator OpenNoMoney()
    {
        noMoneyText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        noMoneyText.SetActive(false);
    }
}
