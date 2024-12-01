using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class traySpawner : MonoBehaviour
{
    GM gm;
    [SerializeField] private Transform parent;
    [SerializeField] private int trayCount;
    [SerializeField] private int trayMax = 2;
    [SerializeField] private GameObject tray_prefab;
    [SerializeField] private List<string> restrictions;
    public tray curr_tray;
    //[SerializeField] private Dictionary<string, Sprite> order;
    [Serializable]
    struct orders
    {
        public string province;
        public List<Sprite> food;
        public List<int> prices;
    }
    [SerializeField] orders[] order;
    [SerializeField] orders curr_order;

    [SerializeField] private List<string> canProv;
    [SerializeField] private List<int> canProv_maxFood;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        trayCount = parent.transform.childCount;
    }
    public IEnumerator spawnOrder()
    {
        if (gm.startDay && trayCount < trayMax)
        {
            do
            {
                int a = UnityEngine.Random.Range(0, order.Length);
                curr_order = order[a];
            } while (!canProv.Contains(curr_order.province));
            /*            int a = UnityEngine.Random.Range(0, canProv.Count);
                        curr_order = order[a];*/
            int idx = canProv.IndexOf(curr_order.province);
            //int b = UnityEngine.Random.Range(0, curr_order.food.Count);
            int b = UnityEngine.Random.Range(0, idx);
            GameObject clone = Instantiate(tray_prefab, parent.transform);
            //clone.transform.localScale = Vector3.one;
            clone.gameObject.GetComponent<tray>().coins = curr_order.prices[b];
            clone.gameObject.GetComponent<tray>().provName = curr_order.province;
            clone.gameObject.GetComponent<tray>().food = curr_order.food[b];
            clone.transform.SetParent(parent.transform, false);
        }
        yield return new WaitForSeconds(5f);
        if(gm.startDay)
        {
            StartCoroutine(spawnOrder());
        }
    }
    [SerializeField] private int[] upgradeTray = {50000, 100000, 200000, 500000};
    int idx = 0;
    bool upgraded = false;
    public void upgrade(Button curr)
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
    }
    public void hover(Image img)
    {
        img.color = Color.gray;
    }
    public void unhover(Image img)
    {
        img.color = Color.white;
    }
    public void ship(Transform prov)
    {
        curr_tray.prov = prov;
        curr_tray.ship();
    }
}
