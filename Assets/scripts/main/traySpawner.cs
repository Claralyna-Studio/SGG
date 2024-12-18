using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class traySpawner : MonoBehaviour, IDataPersistence
{
    GM gm;
    public float orderTime = 5f;
    [SerializeField] private Transform parent;
    public int trayCount;
    public static int trayMax = 2;
    [SerializeField] private GameObject tray_prefab;
    //[SerializeField] private List<GameObject> restrictions;
    public tray curr_tray;
    [SerializeField] private upgrades upg;
    //[SerializeField] private Dictionary<string, Sprite> order;
    [Serializable]
    struct orders
    {
        public string province;
        public List<Sprite> food;
        public List<long> prices;
        public List<float> cookTime;
        public List<GameObject> restrictions;
    }
    [SerializeField] private orders[] order;
    [SerializeField] private orders curr_order;

    [SerializeField] private List<GameObject> curr_restrictions;
    public List<string> canProv;
    public List<int> canProv_maxFood;
    // Start is called before the first frame update
    void Start()
    {
        upgraded = false;
        upg = FindObjectOfType<upgrades>();
        gm = FindObjectOfType<GM>();
        for(int i= 0;i < GameObject.Find("meja (1)").transform.childCount;i++)
        {
            if(i < trayMax && i > 1)
            {
                GameObject.Find("meja (1)").transform.GetChild(i).GetComponent<Animator>().SetBool("in", true);
                GameObject.Find("meja (1)").transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("meja (1)").transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
            }
            else if(i >= trayMax)
            {
                //Debug.Log(i);
                GameObject.Find("meja (1)").transform.GetChild(i).GetComponent<Animator>().SetBool("in",true);
                //current = GameObject.Find("meja (1)").transform.GetChild(i).GetComponent<Button>();
                GameObject.Find("meja (1)").transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("meja (1)").transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(true);
                break;
            }
        }
        switch(trayMax)
        {
            case 2:
                current = GameObject.Find("meja (1)").transform.GetChild(2).gameObject;
                next = GameObject.Find("meja (1)").transform.GetChild(3).gameObject;
                break;
            case 3:
                current = GameObject.Find("meja (1)").transform.GetChild(3).gameObject;
                next = GameObject.Find("meja (1)").transform.GetChild(4).gameObject;
                break;
            case 4:
                current = GameObject.Find("meja (1)").transform.GetChild(4).gameObject;
                next = GameObject.Find("meja (1)").transform.GetChild(5).gameObject;
                break;
            case 5:
                current = GameObject.Find("meja (1)").transform.GetChild(5).gameObject;
                next = GameObject.Find("meja (1)").transform.GetChild(5).gameObject;
                break;
            case 6:
                current = GameObject.Find("meja (1)").transform.GetChild(5).gameObject;
                next = GameObject.Find("meja (1)").transform.GetChild(5).gameObject;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        trayCount = parent.transform.childCount;
        if(!gm.lose && !gm.startDay)
        {
            if(spawn != null)
            {
                StopCoroutine(spawn);
            }
            //StopAllCoroutines();
            //Debug.LogWarning("A");
        }
    }
    public void bukaProv(string prov)
    {
        if(!canProv.Contains(prov))
        {
            canProv.Add(prov);
        }
        foreach(GameObject sprite in GameObject.FindGameObjectsWithTag(prov))
        {
            Debug.Log(sprite.name);
        }
    }
    /*    public void bukaRecipe(int idx)
        {
            if(canProv_maxFood[idx] < 3)
            {
                canProv_maxFood[idx]++;
            }
        }*/
    Coroutine spawn;
    public IEnumerator spawnOrder()
    {
        yield return new WaitForSeconds(orderTime);
        if (!gm.lose && gm.startDay && trayCount < trayMax)
        {
            do
            {
                int a = UnityEngine.Random.Range(0, order.Length);
                curr_order = order[a];
            } while (!canProv.Contains(curr_order.province) || canProv_maxFood[canProv.IndexOf(curr_order.province)] <= 0);

            /*            int a = UnityEngine.Random.Range(0, canProv.Count);
                        curr_order = order[a];*/
            int idx = canProv_maxFood[canProv.IndexOf(curr_order.province)]-1;
            //int b = UnityEngine.Random.Range(0, curr_order.food.Count);
            //Debug.Log(curr_order.province + "." + idx);
            int b = 0;
            do
            {
                b = UnityEngine.Random.Range(-3, 3);
            } while (b > idx || b < -idx);

            GameObject clone = Instantiate(tray_prefab, parent.transform);
            //clone.transform.localScale = Vector3.one;
            if(b >= 0)
            {
                clone.gameObject.GetComponent<tray>().coins = curr_order.prices[b];
                clone.gameObject.GetComponent<tray>().food = curr_order.food[b];
            }
            else
            { 
                clone.gameObject.GetComponent<tray>().coins = curr_order.prices[-b];
                clone.gameObject.GetComponent<tray>().food = curr_order.food[-b];
            }
            clone.gameObject.GetComponent<tray>().provName = curr_order.province;
            clone.transform.SetParent(parent.transform, false);
        }
        if(!gm.lose && gm.startDay)
        {
            spawn = StartCoroutine(spawnOrder());
        }
    }
    [SerializeField] private int[] upgradeTray = {50000, 100000, 200000, 500000};
    static int idx = 0;
    bool upgraded = false;
    [SerializeField] private GameObject current;
    [SerializeField] private GameObject next;
    public void upgradeWaktu(Button curr)
    {
        if (!upgraded && GM.money >= upgradeTray[idx] && idx <= 3)
        {
            current = curr.gameObject;
            curr.interactable = false;
            //current.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gm.addMoney(-upgradeTray[idx]);
        }
    }
    [SerializeField] private TextMeshProUGUI text;
    public void upgradeWaktuText(int time)
    {
        if (!upgraded && GM.money >= upgradeTray[idx] && idx <= 3)
        {
            //Debug.LogWarning("upgrade slot");
            //upg.waktu.Add(time);
            //upgrades.waktu[upgrades.waktu.Count-1] = time;
            //idxUpgradeTime = upgrades.waktu.Count-1;
            upgrades.waktu[3] = time;
            upgraded = true;
            text = current.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            //upgrades.tempTextSlot[3] = text;
            if (upg.upgrading[3] != null)
            {
                StopCoroutine(upg.upgrading[3]);
            }
            upg.upgrading[3] = StartCoroutine(upg.upgradeTimerSlot());
        }
    }
    public void nextButton(Button next)
    {
        if (!upgraded && GM.money >= upgradeTray[idx] && idx <= 3)
        {
            this.next = next.gameObject;
        }
    }
    [SerializeField] private Image img;

    public void doneUpgrade()
    {
        //upg.upgrading.RemoveAt(idxUpgradeTime);
        //upg.waktu.RemoveAt(idxUpgradeTime);
        idx++;
        trayMax++;
        upgraded = false;
        current.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        //img.color = Color.white;
        if (current.gameObject != GameObject.Find("upgrade (5)"))
        {
            next.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            next.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            next.GetComponent<Button>().interactable = true;
        }

    }
/*    public void upgrade(Button curr)
    {
        if (curr != null && gm.money >= upgradeTray[idx] && idx <= 3)
        {
            upgraded = true;
            curr.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            curr.interactable = false;
            gm.addMoney(-upgradeTray[idx]);
            idx++;
            trayMax++;
        }
    }
    public void upgradeBuka(Button next)
    {
        if (next != null && upgraded)
        {
            upgraded = false;
            next.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            next.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            next.interactable = true;
        }
    }*/
    public void hover(Image img)
    {
        if(!gm.lose && !upgraded)
        {
            this.img = img;
            img.color = Color.gray;
        }
    }
    public void unhover(Image img)
    {
        if(!gm.lose && !upgraded)
        {  
            this.img = img;
            img.color = Color.white;
        }
    }
    public void hover1(Image img)
    {
        if(!GameObject.Find("penanda").GetComponent<Image>().enabled)
        {
            img.color = Color.gray; 
        }
    }
    public void unhover1(Image img)
    {
        img.color = Color.white;
    }
    public void ship(Transform prov)
    {
        curr_tray.prov = prov;
        curr_tray.ship();
    }

    public void LoadData(GameData data)
    {
        trayMax = data.trayMax;
/*        for (int i = 0; i < GameObject.Find("sprites").transform.childCount; i++)
        {
            if(upg.pulauUnlockedName.Contains(GameObject.Find("sprites").transform.GetChild(i).tag))
            {
                Debug.Log(GameObject.Find("sprites").transform.GetChild(i).name);
            }
        }*/
        if (data.canProv.Count == canProv.Count)
        {
            canProv = data.canProv;
        }
        if(data.canProv_maxFood.Count == canProv_maxFood.Count)
        {
            canProv_maxFood = data.canProv_maxFood;
        }
    }

    public void SaveData(ref GameData data)
    {
        data.trayMax = trayMax;
        if (data.canProv.Count != canProv.Count)
        {
            //data.waktu.Capacity = waktu.Count;
            for (int i = 0; i < canProv.Count; i++)
            {

                data.canProv.Add(canProv[i]);
            }
        }
        if (data.canProv_maxFood.Count != canProv_maxFood.Count)
        {
            //data.waktu.Capacity = waktu.Count;
            for (int i = 0; i < canProv_maxFood.Count; i++)
            {

                data.canProv_maxFood.Add(canProv_maxFood[i]);
            }
        }
        for (int i = 0; i < canProv.Count; i++)
        {
            data.canProv[i] = canProv[i];
            data.canProv_maxFood[i] = canProv_maxFood[i];
        }
    }
}
