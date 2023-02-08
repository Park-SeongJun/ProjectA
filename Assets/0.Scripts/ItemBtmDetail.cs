using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBtmDetail : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text sumText;

    private ItemBtmController ibCont;

    public KioskData kioskData;
    int price = 0;

    int count = 0;
    public int Count 
    {
        get { return count; } 
        set
        {
            count = value;
            countText.text = string.Format("{0}°³", count);
        }
    }

    int sum = 0;
    public int Sum
    {
        get { return sum; }
        set
        {
            sum = value;
            sumText.text = string.Format("{0:#,###}¿ø", sum);
        }
    }

    public void DataSetting(KioskData data, ItemBtmController cont)
    {
        ibCont = cont;
        kioskData = data;
        Count += 1;

        ChangeSum();
        titleText.text = data.name;
    }
    public void OnMinus()
    {
        if (Count <= 1)
            return;

        Count -= 1;
        ChangeSum();
        ibCont.TotalPrice();
    }

    public void OnPlus()
    {
        if (Count >= 99)
            return;

        Count += 1;
        ChangeSum();
        ibCont.TotalPrice();
    }

    public void ChangeSum()
    {
        Sum = Count * price;
    }
    public void OnDelete()
    {
        ibCont.DeleteData(kioskData);
        Destroy(gameObject);
    }
}
