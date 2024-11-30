using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class traySpawner : MonoBehaviour
{
    GM gm;
    [SerializeField] private Transform parent;
    [SerializeField] private int trayCount;
    [SerializeField] private int trayMax = 2;
    [SerializeField] private GameObject tray_prefab;
    [SerializeField] private List<GameObject> orders;
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
            GameObject clone = Instantiate(tray_prefab);
            clone.transform.parent = parent;
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
            curr.gameObject.transform.GetChild(0).gameObject.SetActive(false);
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
            next.interactable = true;
        }
    }
}
