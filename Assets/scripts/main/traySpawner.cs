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
        public List<string> foodName;
        public string province;
        public List<Sprite> food;
        public List<long> prices;
        public List<float> cookTime;
    }
    [Serializable]
    struct restrictions
    {
        public string foodName;
        [TextArea] public string diseases;
        public List<GameObject> curr_restrictions;
    }
    [SerializeField] private orders[] order;
    [SerializeField] private orders curr_order;

    [SerializeField] private restrictions[] restricts;
    [SerializeField] private List<restrictions> curr_restricts;
    //[SerializeField] private restrictions currTemp_restricts;

    //[SerializeField] private List<string> all_diseases;
    //[SerializeField] private List<GameObject> all_restrictions;
    //[SerializeField] private List<string> curr_disease;
    //[SerializeField] private List<GameObject> curr_restrictions;
    public List<string> canProv;
    public List<int> canProv_maxFood;
    bool restrictionTutor = true;
    // Start is called before the first frame update
    void Start()
    {
        if(GM.day > 2)
        {
            restrictionTutor = false;
        }
        upgraded = false;
        upg = FindObjectOfType<upgrades>();
        gm = FindObjectOfType<GM>();
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
/*            case 6:
                current = GameObject.Find("meja (1)").transform.GetChild(5).gameObject;
                next = GameObject.Find("meja (1)").transform.GetChild(5).gameObject;
                break;*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GM.day > 1)
        {
            for(int i= 0;i < GameObject.Find("meja (1)").transform.childCount-1;i++)
            {
                //upgraded
                if(i < trayMax && i > 1)
                {
                    GameObject.Find("meja (1)").transform.GetChild(i).GetComponent<Button>().interactable = false;
                    GameObject.Find("meja (1)").transform.GetChild(i).GetComponent<Animator>().SetBool("in", true);
                    GameObject.Find("meja (1)").transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("meja (1)").transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
                }
                //current upgrade
                else if(i >= trayMax)
                {
                    //Debug.Log(i);
                    if(gm.closed)
                    {
                        GameObject.Find("meja (1)").transform.GetChild(i).GetComponent<Button>().interactable = true;
                        GameObject.Find("meja (1)").transform.GetChild(i).GetComponent<Animator>().SetBool("in",true);
                        //current = GameObject.Find("meja (1)").transform.GetChild(i).GetComponent<Button>();
                        GameObject.Find("meja (1)").transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
                        GameObject.Find("meja (1)").transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        GameObject.Find("meja (1)").transform.GetChild(i).GetComponent<Button>().interactable = false;
                        GameObject.Find("meja (1)").transform.GetChild(i).GetComponent<Animator>().SetBool("in", false);
                        //current = GameObject.Find("meja (1)").transform.GetChild(i).GetComponent<Button>();
                        GameObject.Find("meja (1)").transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
                        GameObject.Find("meja (1)").transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(true);
                    }
                    break;
                }
            }
        }
        if(upgraded && current)
        {
            current.transform.GetChild(1).GetComponent<Image>().color = Color.gray;
        }

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
/*    public void bukaProv(string prov)
    {
        if(!canProv.Contains(prov))
        {
            canProv.Add(prov);
        }
        foreach(GameObject sprite in GameObject.FindGameObjectsWithTag(prov))
        {
            Debug.Log(sprite.name);
        }
    }*/
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
            bool canRestrict = false;
            int muchRes = UnityEngine.Random.Range(1, 9);
            //day2 pertama kali selalu restrictions
            if (restrictionTutor && GM.day == 2)
            {
                restrictionTutor = false;
                //curr_disease.Clear();
                //curr_restrictions.Clear();
                //canRestrict = true;
                if (muchRes <= 3)
                {
                    muchRes = 0;
                }
                else if (muchRes > 3 && muchRes <= 6)
                {
                    muchRes = 1;
                }
                else
                {
                    muchRes = 2;
                }
                curr_order = order[0];
                curr_restricts.Add(restricts[0]);
                GameObject clone = Instantiate(tray_prefab, parent.transform);
                clone.gameObject.GetComponent<tray>().canShip = false;
                clone.gameObject.GetComponent<tray>().restriction = true;
                clone.gameObject.GetComponent<tray>().curr_diseases.Add(curr_restricts[0].diseases);
                for (int j = 0; j < curr_restricts[0].curr_restrictions.Count; j++)
                {
                    clone.gameObject.GetComponent<tray>().curr_restrictions.Add(curr_restricts[0].curr_restrictions[j]);
                }
                clone.gameObject.GetComponent<tray>().orderName = curr_order.foodName[0];
                clone.gameObject.GetComponent<tray>().coins = curr_order.prices[0];
                clone.gameObject.GetComponent<tray>().food = curr_order.food[0];
                clone.gameObject.GetComponent<tray>().provName = curr_order.province;
                clone.transform.SetParent(parent.transform, false);
            }

            /*            int a = UnityEngine.Random.Range(0, canProv.Count);
                        curr_order = order[a];*/
            
            //idx itu food ke brpnya, random di maxfoodnya


            //chances buat restrictions (max 2 penyakit :p)
            else
            {
                curr_restricts.Clear();
                int idx = canProv_maxFood[canProv.IndexOf(curr_order.province)]-1;
                //int b = UnityEngine.Random.Range(0, curr_order.food.Count);
                //Debug.Log(curr_order.province + "." + idx);
            
                do
                {
                    int a = UnityEngine.Random.Range(0, order.Length);
                    curr_order = order[a];
                } while (!canProv.Contains(curr_order.province) || canProv_maxFood[canProv.IndexOf(curr_order.province)] <= 0);
                bool haveRes = false;
                //restrictions
                if (UnityEngine.Random.Range(0, 100) >= 50)
                {
                    for (int i = 0; i < restricts.Length; i++)
                    {
                        if (curr_order.foodName.Contains(restricts[i].foodName))
                        {
                            haveRes = true;
                            break;
                        }
                    }
                    if(haveRes)
                    {
                        //curr_disease.Clear();
                        //curr_restrictions.Clear();
                        canRestrict = true;
                        muchRes = UnityEngine.Random.Range(1, 9);
                        if (muchRes <= 3)
                        {
                            muchRes = 0;
                        }
                        else if (muchRes > 3 && muchRes <= 6)
                        {
                            muchRes = 1;
                        }
                        else
                        {
                            muchRes = 2;
                        }
                    }
                }
                //no restrictions
                GameObject clone = Instantiate(tray_prefab, parent.transform);

                //b itu recipenya
                int b = 0;
                do
                {
                    b = UnityEngine.Random.Range(-3, 3);
                } while (b > idx || b < -idx);
                if (b >= 0)
                {
                    if (canRestrict)
                    {
                        clone.gameObject.GetComponent<tray>().canShip = false;
                        clone.gameObject.GetComponent<tray>().restriction = true;
                        for (int i = 0; i < muchRes; i++)
                        {
                            int random = 0;
                            do
                            {
                                random = UnityEngine.Random.Range(0, restricts.Length);
                            } while (curr_restricts.Contains(restricts[random]) || curr_restricts[random].foodName != curr_order.foodName[b]);
                            curr_restricts.Add(restricts[random]);
                            clone.gameObject.GetComponent<tray>().curr_diseases.Add(curr_restricts[i].diseases);
                            for (int j = 0; j < curr_restricts[i].curr_restrictions.Count; j++)
                            {
                                clone.gameObject.GetComponent<tray>().curr_restrictions.Add(curr_restricts[i].curr_restrictions[j]);
                            }
                        }
                    }
                    clone.gameObject.GetComponent<tray>().orderName = curr_order.foodName[b];
                    clone.gameObject.GetComponent<tray>().coins = curr_order.prices[b];
                    clone.gameObject.GetComponent<tray>().food = curr_order.food[b];
                }
                else
                {
                    //curr_restricts = restricts[-b];
                    if (canRestrict)
                    {
                        clone.gameObject.GetComponent<tray>().restriction = true;
                        for (int i = 0; i < muchRes; i++)
                        {
                            int random = 0;
                            do
                            {
                                random = UnityEngine.Random.Range(0, restricts.Length);
                            } while (curr_restricts.Contains(restricts[random]) || curr_restricts[random].foodName != curr_order.foodName[-b]);
                            curr_restricts.Add(restricts[random]);
                            clone.gameObject.GetComponent<tray>().curr_diseases.Add(curr_restricts[i].diseases);
                            for (int j = 0; j < curr_restricts[i].curr_restrictions.Count; j++)
                            {
                                clone.gameObject.GetComponent<tray>().curr_restrictions.Add(curr_restricts[i].curr_restrictions[j]);
                            }
                        }
                    }
                    clone.gameObject.GetComponent<tray>().orderName = curr_order.foodName[-b];
                    clone.gameObject.GetComponent<tray>().coins = curr_order.prices[-b];
                    clone.gameObject.GetComponent<tray>().food = curr_order.food[-b];
                }
                clone.gameObject.GetComponent<tray>().provName = curr_order.province;
                clone.transform.SetParent(parent.transform, false);
                if(b >= 0)
                {
                    if(canRestrict)
                    {
                        for (int i = 0; i < muchRes; i++)
                        {
                            int random = 0;
                            do
                            {
                                random = UnityEngine.Random.Range(0, restricts.Length);
                            } while (curr_restricts.Contains(restricts[random]) || curr_restricts[random].foodName != curr_order.foodName[b]);
                            curr_restricts.Add(restricts[random]);
                            clone.gameObject.GetComponent<tray>().curr_diseases.Add(curr_restricts[i].diseases);
                            for(int j = 0;j< curr_restricts[i].curr_restrictions.Count;j++)
                            {
                                clone.gameObject.GetComponent<tray>().curr_restrictions.Add(curr_restricts[i].curr_restrictions[j]);
                            }
                        }
                    }
                    clone.gameObject.GetComponent<tray>().orderName = curr_order.foodName[b];
                    clone.gameObject.GetComponent<tray>().coins = curr_order.prices[b];
                    clone.gameObject.GetComponent<tray>().food = curr_order.food[b];
                }
                else
                {
                    //curr_restricts = restricts[-b];
                    if(canRestrict)
                    {
                        for (int i = 0; i < muchRes; i++)
                        {
                            int random = 0;
                            do
                            {
                                random = UnityEngine.Random.Range(0, restricts.Length);
                            } while (curr_restricts.Contains(restricts[random]) || curr_restricts[random].foodName != curr_order.foodName[-b]);
                            curr_restricts.Add(restricts[random]);
                            clone.gameObject.GetComponent<tray>().curr_diseases.Add(curr_restricts[i].diseases);
                            for (int j = 0; j < curr_restricts[i].curr_restrictions.Count; j++)
                            {
                                clone.gameObject.GetComponent<tray>().curr_restrictions.Add(curr_restricts[i].curr_restrictions[j]);
                            }
                        }
                    }
                    clone.gameObject.GetComponent<tray>().orderName = curr_order.foodName[-b];
                    clone.gameObject.GetComponent<tray>().coins = curr_order.prices[-b];
                    clone.gameObject.GetComponent<tray>().food = curr_order.food[-b];
                }
                clone.gameObject.GetComponent<tray>().provName = curr_order.province;
                clone.transform.SetParent(parent.transform, false);
            }

            //GameObject clone = Instantiate(tray_prefab, parent.transform);

            //if restricted UNUSED
/*            if(canRestrict)
            {
                //clone.gameObject.GetComponent<tray>().restriction = true;
                //clone.gameObject.GetComponent<tray>().canShip = false;
                //clone.gameObject.GetComponent<tray>().curr_restrictions = curr_restrictions;
            }*/

            //clone.transform.localScale = Vector3.one;
        }
        if(!gm.lose && gm.startDay)
        {
            spawn = StartCoroutine(spawnOrder());
        }
    }
    [SerializeField] private int[] upgradeTray = {50000, 100000, 200000, 500000};
    static int idx = 0;
    public bool upgraded = false;
    [SerializeField] private GameObject current;
    [SerializeField] private GameObject next;
    public void upgradeWaktu(Button curr)
    {
        if (!upgraded && GM.money >= upgradeTray[idx] && idx <= 3)
        {
            current = curr.gameObject;
            //curr.interactable = false;
            //current.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            
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
            current.GetComponent<Button>().interactable = false;
            gm.addMoney(-upgradeTray[idx]);
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
