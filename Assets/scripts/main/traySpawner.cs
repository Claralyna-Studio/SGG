using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class traySpawner : MonoBehaviour
{
    GM gm;
    public float orderTime = 5f;
    [SerializeField] private Transform parent;
    public int trayCount;
    public int trayMax = 2;
    [SerializeField] private GameObject tray_prefab;
    [SerializeField] private List<GameObject> restrictions;
    public tray curr_tray;
    [SerializeField] private upgrades upg;
    //[SerializeField] private Dictionary<string, Sprite> order;
    [Serializable]
    struct orders
    {
        public string province;
        public List<Sprite> food;
        public List<int> prices;
        public List<float> cookTime;
    }
    [SerializeField] private orders[] order;
    [SerializeField] private orders curr_order;

    [SerializeField] private List<string> canProv;
    [SerializeField] private List<int> canProv_maxFood;
    // Start is called before the first frame update
    void Start()
    {
        upg = FindObjectOfType<upgrades>();
        gm = FindObjectOfType<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        trayCount = parent.transform.childCount;
    }
    public IEnumerator spawnOrder()
    {
        if (!gm.lose && gm.startDay && trayCount < trayMax)
        {
            do
            {
                int a = UnityEngine.Random.Range(0, order.Length);
                curr_order = order[a];
            } while (!canProv.Contains(curr_order.province));
            /*            int a = UnityEngine.Random.Range(0, canProv.Count);
                        curr_order = order[a];*/
            int idx = canProv_maxFood[canProv.IndexOf(curr_order.province)]-1;
            //int b = UnityEngine.Random.Range(0, curr_order.food.Count);
            Debug.Log(curr_order.province + "." + idx);
            int b = UnityEngine.Random.Range(0, idx);
            GameObject clone = Instantiate(tray_prefab, parent.transform);
            //clone.transform.localScale = Vector3.one;
            clone.gameObject.GetComponent<tray>().coins = curr_order.prices[b];
            clone.gameObject.GetComponent<tray>().provName = curr_order.province;
            clone.gameObject.GetComponent<tray>().food = curr_order.food[b];
            clone.transform.SetParent(parent.transform, false);
        }
        yield return new WaitForSeconds(orderTime);
        if(!gm.lose && gm.startDay)
        {
            StartCoroutine(spawnOrder());
        }
    }
    [SerializeField] private int[] upgradeTray = {50000, 100000, 200000, 500000};
    int idx = 0;
    bool upgraded = false;
    [SerializeField] private Button current;
    [SerializeField] private Button next;
    public void upgradeWaktu(Button curr)
    {
        if (!upgraded && gm.money >= upgradeTray[idx] && idx <= 3)
        {
            current = curr;
            curr.interactable = false;
            //current.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gm.addMoney(-upgradeTray[idx]);
        }
    }
    int idxUpgradeTime = 0;
    [SerializeField] private TextMeshProUGUI text;
    public void upgradeWaktuText(int time)
    {
        if (!upgraded && gm.money >= upgradeTray[idx] && idx <= 3)
        {
            upg.waktu.Add(time);
            idxUpgradeTime = upg.waktu.Count-1;
            upgraded = true;
            text = current.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            upg.upgrading.Add(
                StartCoroutine(
                    upg.upgradeTimerSlot(idxUpgradeTime, text)
                    )
                );
        }
    }
    public void nextButton(Button next)
    {
        this.next = next;
    }
    Image img;

    public void doneUpgrade()
    {
        upg.upgrading.RemoveAt(idxUpgradeTime);
        upg.waktu.RemoveAt(idxUpgradeTime);
        idx++;
        trayMax++;
        img.color = Color.white;
        upgraded = false;
        current.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        if (current.gameObject != GameObject.Find("upgrade (5)"))
        {
            next.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            next.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            next.interactable = true;
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
        img.color = Color.gray; 
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
}
