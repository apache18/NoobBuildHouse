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
    public double[] countLevel;
    public int[] countLevel2;
    public double[] countDoxod;
    public Text moneyText;
    public Text PlusProcentText;
    public Text PlusProcentText2;
    public double PlusProcent = 10;
    public double PlusProcent2;
    public int PricePlusProcent = 1000;
    public float AutomatizationDoxodCount;
    public long AutomatizationDoxodPrice;
    public long AutomatizationDoxodPrice2;
    string PlusProcentBykva;
    public Text[] UpgradeText;
    public Text[] PriceNewLevel;
    public Text[] YlytsitText;
    public Text[] Doxod;
    public Text[] LevelText;
    public Text AutomatizationDoxodText;
    public Text AutomatizationDoxodPriceText;
    public Text timer;
    public GameObject[] Panel;
    public GameObject[] Player;
    public GameObject noMoneyText;  
    public GameObject UpgradeImage;
    bool isOpenUpgradeImage = false;
    public bool[] isUpgrade;
    public bool[] checkBuyLevel;
    bool isCheckMoney = false;
    public bool isCheckAutomatization = false;
    bool isCheckPricePlus;
    public bool checkTimer = false;
    public int[] count;
    public long[] priceBuyNewLayer;
    public long[] priceBuyNewLayer2;
    public string[] priceText;
    string YlytsitBykvaText;
    string DoxodText;
    string AutomatizationDoxodBykva = "";
    public double numberPlusProcent = 1.1f;
    public int sek = 30;
    public GameObject obj;
    public Button buttonAd;

    private YandexSDK sdk;

    private void Start()
    {
        sdk = YandexSDK.instance;
        sdk.onRewardedAdReward += MultiplyCountLevelOnTenIn30Sekynd;
        isCheckMoney = false;
        AutomatizationInStart();
        PlusProcentInStart();
        StartCoroutine(AddMoneyLevel());
        PrisvoenieCountLevel();

        for (int i = 0; i < count.Length; i++)
        {
            count[i] = 1;
        }
        for (int i = 0; i <= priceBuyNewLayer.Length - 1; i++) 
        {
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
        if (Money >= addMinusMoney[number] )
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
                    LevelText[0].text = "Слой 2";
                    Player[0].SetActive(true);
                    checkBuyLevel[1] = true;
                    break;
                case 10000:
                    Panel[1].SetActive(false);
                    Player[1].SetActive(true);
                    LevelText[1].text = "Слой 3";
                    checkBuyLevel[2] = true;
                    break;
                case 300000:
                    Panel[2].SetActive(false);
                    Player[2].SetActive(true);
                    LevelText[2].text = "Слой 4";
                    checkBuyLevel[3] = true;
                    break;
                case 3000000:
                    Panel[3].SetActive(false);
                    Player[3].SetActive(true);
                    LevelText[3].text = "Слой 5";
                    checkBuyLevel[4] = true;
                    break;
                case 70000000:
                    Panel[4].SetActive(false);
                    Player[4].SetActive(true);
                    LevelText[4].text = "Слой 6";
                    checkBuyLevel[5] = true;
                    break;
                case 500000000:
                    Panel[5].SetActive(false);
                    Player[5].SetActive(true);
                    LevelText[5].text = "Зелёный куб";
                    checkBuyLevel[6] = true;
                    break;
                case 10000000000:
                    Panel[6].SetActive(false);
                    Player[6].SetActive(true);
                    LevelText[6].text = "Слой 8";
                    checkBuyLevel[7] = true;
                    break;
                case 250000000000:
                    Panel[7].SetActive(false);
                    Player[7].SetActive(true);
                    LevelText[7].text = "Слой 9";
                    checkBuyLevel[8] = true;
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
        if (Money >= minusMoney[number] && isUpgrade[number] == false && count[number] <= 6)  
        {
            countLevel[number] += countLevel2[number];
            Money -= minusMoney[number];
            minusMoney[number] += addMinusMoney[number];
            count[number]++;
        }
        if (count[number] == 5)
        {
            UpgradeText[number].text = "max";
            isUpgrade[number] = true;
        }
        else if (Money < minusMoney[number]) 
        {
            StartCoroutine(OpenNoMoney());
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
            if (countLevel[i] >= 1000 && countLevel[i] < 1000000)
            {
                countDoxod[i] =  countLevel[i] / 1000;
                DoxodText = "К";
            }
            else if(countLevel[i] >= 1000000 && countLevel[i] < 1000000000)
            {
                countDoxod[i] = countLevel[i] / 1000000;
                DoxodText = "М";
            }
            else if (countLevel[i] >= 1000000000)
            {
                countDoxod[i] = countLevel[i] / 1000000000;
                DoxodText = "B";
            }
            else
            {
                countDoxod[i] = countLevel[i];
                DoxodText = "";
            }
            if (isUpgrade[i] == false)
            {
                UpgradeText[i].text = count[i] + "/6";
                YlytsitText[i].text = "УЛУЧШИТЬ: " + minusMoney2[i] + YlytsitBykvaText;
            }
            else
            {
                YlytsitText[i].text = "УЛУЧШИТЬ: " + " МАКС";
            }
            Doxod[i].text = "ДОХОД: " + countDoxod[i] + DoxodText + "/УДАР";
        }
    }

    public void UpgradeProcentDoxod()
    {
        if (Money >= PricePlusProcent && isCheckPricePlus != true)
        {
            Money -= PricePlusProcent;
            PlusProcent += 10;
            if (PlusProcent <= 50)
            {
                PricePlusProcent *= 10;
                for (int i = 0; i < countLevel.Length; i++)
                {
                    countLevel[i] = Math.Round(countLevel[i] * numberPlusProcent);
                }
                numberPlusProcent = numberPlusProcent + 0.1;
                if (PricePlusProcent >= 1000 && PricePlusProcent < 1000000)
                {
                    PlusProcent2 = PricePlusProcent / 1000;
                    PlusProcentBykva = "К";
                }
                if (PricePlusProcent >= 1000000 && PricePlusProcent < 1000000000)
                {
                    PlusProcent2 = PricePlusProcent / 1000000;
                    PlusProcentBykva = "М";
                }
                PlusProcentText.text = PlusProcent2.ToString() + PlusProcentBykva;
                PlusProcentText2.text = "Увеличить общий доход на " + PlusProcent.ToString() + "%";
            }
            if (PlusProcent == 50)
            {
                PlusProcentText.text = "max";
                isCheckPricePlus = true;
                PlusProcent = 0;
            }
        }
        else if (Money < PricePlusProcent && isCheckPricePlus != true) 
        {
            StartCoroutine(OpenNoMoney());
        }
    }

    void PlusProcentInStart()
    {
        PlusProcentBykva = "К";
        PlusProcent2 = 1;
        PlusProcent = 10;
        PlusProcentText.text = PlusProcent2.ToString() + PlusProcentBykva;
        PlusProcentText2.text = "Увеличить общую скорость добычи на " + PlusProcent.ToString() + "%";

    }

    void AutomatizationInStart()
    {
        AutomatizationDoxodCount = 2f;
        AutomatizationDoxodPrice2 = 1;
        AutomatizationDoxodPrice = 1000;
        AutomatizationDoxodBykva = "К";
        AutomatizationDoxodText.text = "Скорость добычи: удар/в " + AutomatizationDoxodCount.ToString() + "секунду";
        AutomatizationDoxodPriceText.text = AutomatizationDoxodPrice2.ToString() + AutomatizationDoxodBykva;

    }

    void PrisvoenieCountLevel()
    {
        checkBuyLevel[0] = true;
        countLevel[0] = 10;
        countLevel[1] = 50;
        countLevel[2] = 250;
        countLevel[3] = 15000;
        countLevel[4] = 150000;
        countLevel[5] = 3500000;
        countLevel[6] = 25000000;
        countLevel[7] = 500000000;
        countLevel[8] = 12500000000;
    }

    public void UpgradeAutomatizationDoxod()
    {
        if (Money >= AutomatizationDoxodPrice && isCheckAutomatization != true)
        {
            Money -= AutomatizationDoxodPrice;
            AutomatizationDoxodPrice *= 10;
            AutomatizationDoxodCount = ((float)Math.Round(AutomatizationDoxodCount - 0.4f, 2));
            if (AutomatizationDoxodCount >= 0.4f)  
            {
                if (AutomatizationDoxodPrice >= 1000 && AutomatizationDoxodPrice < 1000000)
                {
                    AutomatizationDoxodBykva = "К";
                    AutomatizationDoxodPrice2 = AutomatizationDoxodPrice / 1000;
                }
                if (AutomatizationDoxodPrice >= 1000000 && AutomatizationDoxodPrice < 1000000000)
                {
                    AutomatizationDoxodPrice2 = AutomatizationDoxodPrice / 1000000;
                    AutomatizationDoxodBykva = "М";
                }
                if (AutomatizationDoxodPrice >= 1000000000 && AutomatizationDoxodPrice < 1000000000000)
                {
                    AutomatizationDoxodPrice2 = AutomatizationDoxodPrice / 1000000000;
                    AutomatizationDoxodBykva = "B";
                }
                AutomatizationDoxodText.text = "Скорость добычи: удар/в " + AutomatizationDoxodCount.ToString() + "секунду";
                AutomatizationDoxodPriceText.text = AutomatizationDoxodPrice2.ToString() + AutomatizationDoxodBykva;
            }
            if (AutomatizationDoxodCount < 0.45f && AutomatizationDoxodCount > 0.35f) 
            {
                AutomatizationDoxodPriceText.text = "max";
                isCheckAutomatization = true;
            }
        }
        else if (isCheckAutomatization != true) 
        {
            StartCoroutine(OpenNoMoney());
        }     
    }

    public void MultiplyCountLevelOnTenIn30Sekynd(string placement)
    {
        if(placement == "time")
        {
            Debug.Log(123);
            if (checkTimer == false)
            {
                sek = 30;
                for (int i = 0; i < countLevel.Length; i++)
                {
                    countLevel[i] *= 10;
                }
            }
            StartCoroutine(Timer());
        }
    }

    IEnumerator AddMoneyLevel()
    {
        while (true)
        {
            for (int i = 0; i < countLevel.Length; i++)
            {
                if (checkBuyLevel[i] == true)
                {
                    Money += countLevel[i];
                }
            }
            yield return new WaitForSeconds(AutomatizationDoxodCount);
        }
    }

    IEnumerator OpenNoMoney()
    {
        noMoneyText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        noMoneyText.SetActive(false);
    }

    IEnumerator Timer()
    {
        checkTimer = true;
        buttonAd.enabled = false;
        while (sek > 0) 
        {
            sek--;
            obj.SetActive(false);
            timer.enabled = true;
            timer.text = sek.ToString();
            yield return new WaitForSeconds(1f);
            if (sek == 0)
            {
                for (int i = 0; i < countLevel.Length; i++)
                {
                    countLevel[i] /= 10;
                }
                buttonAd.enabled = true;
                checkTimer = false;
                timer.enabled = false;
                obj.SetActive(true);
            }
        }
    }
}
