using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MainMenuType
{
    FastFood,
    Pizza,
    China,
    Coffee,
    Korea,
    Chicken
}

public struct KioskData
{
    public string name;
    public int prices;
    public string dec;
}

public class Kiosk : MonoBehaviour
{
    [SerializeField] GameObject menuObj;
    [SerializeField] GameObject detailMenuObj;
    [SerializeField] GameObject titleObj;

    [SerializeField] Transform titleParent;
    [SerializeField] Title titlePrefab;
    [SerializeField] Transform detailParent;
    [SerializeField] ItemDetail detailPrefab;

    List<string> titleList = new List<string>();
    Dictionary<string, KioskData> menuDic = new Dictionary<string, KioskData>();
    private MainMenuType selectType = MainMenuType.FastFood;

    List<ItemDetail> itemDetails = new List<ItemDetail>();

    // Start is called before the first frame update
    void Start()
    {
        titleList.Clear();
        // �޴� ����
        switch(selectType)
        {
            case MainMenuType.FastFood:
                {
                    string[] strs = { "�ܹ���", "����", "������", "�ҽ�", "���̽�ũ��" };
                    foreach (var item in strs)
                    {
                        titleList.Add(item);
                    }                    
                }
                break;
            case MainMenuType.Pizza:
                break;
            case MainMenuType.China:
                break;
        }
        // �޴� ����

        OnShowMain();
    }

    public void OnShowMain()
    {
        ShowMain(true);

        // Ÿ��Ʋ ����
        for (int i = 0; i < titleList.Count; i++)
        {
            Title title = Instantiate(titlePrefab, titleParent);
            title.SetText(titleList[i]);
            title.name = titleList[i];
            
            Toggle toggle = title.GetComponent<Toggle>();
            toggle.group = titleParent.GetComponent<ToggleGroup>();
            toggle.onValueChanged.AddListener(delegate{ OnToggle(title.GetComponent<Toggle>()); });

            if(i == 0)
            {
                toggle.isOn = true;
            }
        }
    }
    public void OnShowDetail()
    {
        ShowMain(false);
    }

    void ShowMain(bool isShow)
    {
        menuObj.SetActive(isShow);
        detailMenuObj.SetActive(!isShow);
        titleObj.SetActive(!isShow);
    }

    public void OnToggle(Toggle toggle)
    {
        if(toggle.isOn)
        {
            Debug.Log(toggle.name);
        }
    }

    void SubMenuSetting(Toggle toggle)
    {
        menuDic.Clear();

        /*for (int i = detailParent.childCount - 1; i >= 0; i--)
        {
            Destroy(detailParent.GetChild(i).gameObject);
        }*/

        switch(toggle.name)
        {
            case "�ܹ���":
                {
                    string[] keys = { "�Ұ�����", "�������", "�Ұ�����", "ġ�����", "ġŲ����" };
                    int[] prices = { 3000, 5000, 8000, 4500, 6000 };
                    DataSetCreateMenu(keys, prices);
                }
                break;
            case "����":
                {
                    string[] keys = { "�ݶ�", "�����ݶ�", "���̴�", "���λ��̴�", "ȯŸ", "��ġ��", "����Ŭ��" };
                    int[] prices = { 2000, 2500, 1500, 2000, 1000, 50, 1500 };
                    DataSetCreateMenu(keys, prices);
                }
                
                break;
            case "������":
                {
                    string[] keys = { "����Ƣ��", "��Ͼ�", "��¡��", "�ʰ�", "ġ�ƽ" };
                    int[] prices = { 500, 800, 1000, 300, 200 };
                    DataSetCreateMenu(keys, prices);
                }
                break;
            case "�ҽ�":
                {
                    string[] keys = { "ĥ���ҽ�", "��Ͼ�ҽ�", "ġ��", "�ӽ�Ÿ��", "����" };
                    int[] prices = { 300, 500, 800, 100, 50 };
                    DataSetCreateMenu(keys, prices);
                }
                break;
            case "���̽�ũ��":
                {
                    string[] keys = { "����", "�ٴҶ�", "����", "������", "����", "��Ʈ����" };
                    int[] prices = { 800, 800, 800, 900, 900, 1000 };
                    DataSetCreateMenu(keys, prices);
                }
                break;
                
        }
    }

    void DataSetCreateMenu(string[] keys, int[] prices)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            KioskData data = new KioskData();
            data.prices = prices[i];
            data.name = keys[i];

            menuDic.Add(keys[i], data);
        }

        if (itemDetails.Count == 0)
        {
            foreach (var item in menuDic)
            {
                ItemDetail id = Instantiate(detailPrefab, detailParent)
                .SetNameText(item.Value.name)
                .SetPriceText(item.Value.prices);

                itemDetails.Add(id);
            }
        }
        else if (itemDetails.Count <= keys.Length)
        {
            int addCount = 0;
            foreach (var item in menuDic)
            {
                if (addCount < detailParent.childCount)
                {
                    itemDetails[addCount].gameObject.SetActive(true);
                    itemDetails[addCount]
                        .SetNameText(item.Value.name)
                        .SetPriceText(item.Value.prices);
                }
                else
                {
                    ItemDetail id = Instantiate(detailPrefab, detailParent)
                .SetNameText(item.Value.name)
                .SetPriceText(item.Value.prices);

                    itemDetails.Add(id);
                }
                addCount++;
            }
        }
        else
        {
            for (int i = 0; i < keys.length; i++)
            {
                foreach (var item in menuDic)
                {
                    if (addCount < keys.Length)
                    {
                        itemDetails[addCount].gameObject.SetActive(true);
                        itemDetails[addCount]
                            .SetNameText(item.Value.name)
                            .SetPriceText(item.Value.prices);
                    }
                    else
                    {
                        itemDetails[addCount].gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
