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
    //[SerializeField] private Dictionary<string, Sprite> order;
    [Serializable]
    struct orders
    {
        public string province;
        public List<Sprite> food;
    }
    [SerializeField] orders[] order;
    [SerializeField] orders curr_order;
    public bool Bali = false;
    public bool Banten = false;
    public bool Jogja = false;
    public bool Jakarta = false;
    public bool Jawa_Barat = false;
    public bool Jawa_Tengah = false;
    public bool Jawa_Timur = false;
    public bool NTB = false;
    public bool NTT = false;
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
            int a = UnityEngine.Random.Range(0, order.Length);
            curr_order = order[a];
            int b = UnityEngine.Random.Range(0, curr_order.food.Count);
            GameObject clone = Instantiate(tray_prefab, parent.transform);
            //clone.transform.localScale = Vector3.one;
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
}
