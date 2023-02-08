using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDetail : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text priceText;

    [SerializeField] Transform parent;
    [SerializeField] ItemBtmDetail itembd;
    private ItemBtmController ibCont;
    private KioskData kioskData;
    public ItemDetail SetNameText(string name)
    {
        nameText.text = name;
        return this;
    }

    public ItemDetail SetPriceText(int price)
    {
        priceText.text = string.Format("{0:#,###}¿ø", price);
        return this;
    }

    public ItemDetail SetIBCont(ItemBtmController cont)
    {
        ibCont = cont;
        return this;
    }

    public ItemDetail SetKioskData(KioskData data)
    {
        kioskData = data;
        SetNameText(data.name);
        SetPriceText(data.price);
        return this;
    }

    public ItemDetail SetParent(Transform parent)
    {
        this.parent = parent;
        return this;
    }

    public ItemDetail SetItemBD(ItemBtmDetail itembd)
    {
        this.itembd = itembd;
        return this;
    }

    public void OnClick()
    {
        if(ibCont.IsCheck(kioskData.name, kioskData) )
        {
            ItemBtmDetail ibd = Instantiate(itembd, parent);
            ibd.DataSetting(kioskData, ibCont);

            ibCont.itemDetails.Add(ibd);
        }
        else
        {
            ibCont.AddCount(kioskData.name);
        }
        ibCont.TotalPrice();
    }
}
