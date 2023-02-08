using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBtmController : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text totalPriceText;

    private List<KioskData> kioskDatas = new List<KioskData>();

    [HideInInspector]
    public List<ItemBtmDetail> itemDetails = new List<ItemBtmDetail>();

    public bool IsCheck(string name, KioskData data)
    {
        if(kioskDatas.Count == 0)
        {
            kioskDatas.Add(data);
            return true;
        }
        else
        {
            bool check = true;
            foreach (var item in kioskDatas)
            {
                if (item.name == name)
                    check = false;
            }

            if(check)
            {
                kioskDatas.Add(data);
            }
            return check;
        }
    }

    public void AddCount(string name)
    {
        foreach (var item in itemDetails)
        {
            if(item.kioskData.name == name)
            {
                item.Count += 1;
                item.ChangeSum();
                break;
            }
        }        
    }

    private void Start()
    {                
        TotalPrice();        
    }
    public void TotalPrice()
    {
        if(itemDetails.Count == 0)
        {
            totalPriceText.text = "0¿ø";
            return;
        }
        int sum = 0;
        foreach (var item in itemDetails)
        {
            sum += item.Count * item.kioskData.price;
        }
        totalPriceText.text = string.Format("{0:#,###}¿ø", sum);
    }

    public void DeleteData(KioskData data)
    {
        foreach (var item in itemDetails)
        {
            if(data.name == item.kioskData.name)
            {
                itemDetails.Remove(item);
                kioskDatas.Remove(item.kioskData);
                TotalPrice();
                break;
            }
        }
    }
}
