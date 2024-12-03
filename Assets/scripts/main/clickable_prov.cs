using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class clickable_prov : MonoBehaviour
{
    //[SerializeField] private int idx = 0;
    Animator upgradeUI;
    GM gm;
    public List<PolygonCollider2D> colliders_to_be_unactived;
    traySpawner tray_spawner;
    [SerializeField] private Image penanda;
    [SerializeField] private GameObject bubbleProv;
    [SerializeField] private TextMeshProUGUI bubbleProvText;
    private void Awake()
    {
        penanda = GameObject.Find("penanda").GetComponent<Image>();
        colliders_to_be_unactived.Add(this.gameObject.GetComponent<PolygonCollider2D>());
        bubbleProv = GameObject.Find("BUBBLE_POINTER");
        bubbleProvText = bubbleProv.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        bubbleProvText.text = " ";
    }
    // Start is called before the first frame update
    void Start()
    {
/*        if(this.gameObject.tag == "Sunda Kecil")
        {
            idx = 0;
        }
        else if (this.gameObject.tag == "Sumatra")
        {
            idx = 1;
        }
        else if (this.gameObject.tag == "Papua")
        {
            idx = 2;
        }
        else if (this.gameObject.tag == "Kalimantan")
        {
            idx = 3;
        }
        else if (this.gameObject.tag == "Jawa")
        {
            idx = 4;
        }
        else if (this.gameObject.tag == "Sulawesi")
        {
            idx = 5;
        }*/
        //upgradeUI = GameObject.Find("upgradesUI").GetComponent<Animator>();
        upgradeUI = FindObjectOfType<upgrades>().GetComponent<Animator>();
        tray_spawner = FindObjectOfType<traySpawner>();
        gm = FindObjectOfType<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bubbleProv && penanda.enabled)
        {
            bubbleProv.transform.GetChild(0).gameObject.SetActive(true);
            bubbleProv.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            //bubbleProv.transform.position =Input.mousePosition;
        }
        else if(bubbleProv && !penanda.enabled)
        {
            bubbleProv.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private void OnMouseOver()
    {
        if(!gm.lose && penanda.enabled)
        {
            bubbleProvText.text = this.gameObject.name;
        }
    }
    private void OnMouseExit()
    {
        if (!gm.lose && penanda.enabled)
        {
            bubbleProvText.text = " ";
        }
    }
    private void OnMouseDown()
    {
        if(!gm.lose)
        {
            bubbleProvText.text = " ";
            //Debug.Log("clicked " + this.gameObject.name);
            if(gm.canShip && !gm.closed && !gm.isReadingRecipe)
            {
                if (!gm.isCooking[gm.provs.IndexOf(this.gameObject.transform)] && tray_spawner.curr_tray.prov == transform)
                {
                    tray_spawner.ship(transform);
                }
                else if(!gm.isCooking[gm.provs.IndexOf(this.gameObject.transform)] && tray_spawner.curr_tray.prov != transform && GameObject.Find("hearts").transform.childCount > 0)
                {
                    GameObject.Find("hearts").transform.GetChild(GameObject.Find("hearts").transform.childCount - 1).GetComponent<Animator>().SetBool("break",true);
                }
                else
                {
                    //the food is still cooking
                }
            }
            else if(gm.closed && !gm.isReadingRecipe && !upgradeUI.GetComponent<upgrades>().isUpgrading && 
                upgradeUI.GetComponent<upgrades>().pulauUnlocked[upgradeUI.GetComponent<upgrades>().pulauUnlockedName.IndexOf(this.gameObject.tag)]) //upgradeUI.GetComponent<upgrades>().pulauUnlocked[idx])
            {
                //upgradeUI.GetComponent<upgrades>().clicked = true;
                upgradeUI.GetComponent<upgrades>().pos = transform;
                upgradeUI.GetComponent<upgrades>().curr_prov = this;
                upgradeUI.SetTrigger("pindah");
            }
        }
    }
}
