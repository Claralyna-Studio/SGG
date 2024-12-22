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
    [SerializeField] private Texture2D cursor;
    [SerializeField] private bool manager = false;
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
        if(gameObject.name == "Bali")
        {
            upgradeUI.GetComponent<upgrades>().pos = transform;
            upgradeUI.GetComponent<upgrades>().curr_prov = this;
            upgradeUI.SetTrigger("pindah");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.closed)
        {
            idxHeart = 3;
            isbreaking = false;
        }
        if(manager && penanda.enabled && !gm.lose)
        {
            Cursor.SetCursor(cursor, new Vector2(70,120), CursorMode.Auto);
            bubbleProv.transform.GetChild(0).gameObject.SetActive(true);
            //bubbleProv.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            bubbleProv.transform.position = Input.mousePosition;
        }
        else if(manager && !penanda.enabled && !gm.lose)
        {
            Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
            bubbleProv.transform.GetChild(0).gameObject.SetActive(false);
        }
        if(!upgradeUI.GetComponent<upgrades>().pulauUnlocked[upgradeUI.GetComponent<upgrades>().pulauUnlockedName.IndexOf(this.gameObject.tag)])
        {
            //GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.6f);
            //GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("MAP").transform.Find(this.gameObject.name).gameObject.GetComponent<Image>().color = Color.Lerp(GameObject.Find("MAP").transform.Find(this.gameObject.name).gameObject.GetComponent<Image>().color, new Color(0,0,0,0.6f), Time.deltaTime * 10f);
            GameObject.Find("MAP").transform.Find(this.gameObject.name).gameObject.GetComponent<Image>().enabled = true;
        }
        else if(gm.closed && upgradeUI.GetComponent<upgrades>().pulauUnlocked[upgradeUI.GetComponent<upgrades>().pulauUnlockedName.IndexOf(this.gameObject.tag)])
        {
            //GetComponent<SpriteRenderer>().color = new Color(1,0,0,0.6f);
            //GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("MAP").transform.Find(this.gameObject.name).gameObject.GetComponent<Image>().color = Color.Lerp(GameObject.Find("MAP").transform.Find(this.gameObject.name).gameObject.GetComponent<Image>().color, Color.clear,Time.deltaTime*10f);
            //GameObject.Find("MAP").transform.Find(this.gameObject.name).gameObject.GetComponent<Image>().enabled = false;
        }
    }
    private void OnMouseOver()
    {
        if(!gm.lose && penanda.enabled)
        {
            bubbleProv.GetComponent<Animator>().SetBool("hovering", true);
            if(!gm.isCooking[gm.provs.IndexOf(this.gameObject.transform)])
            {
                if(this.gameObject.name == "DKI Jakarta")
                {
                    bubbleProvText.text = "Jakarta";
                }
                else if (this.gameObject.name == "Nusa Tenggara Barat")
                {
                    bubbleProvText.text = "West Nusa Tenggara";
                }
                else if (this.gameObject.name == "Nusa Tenggara Timur")
                {
                    bubbleProvText.text = "East Nusa Tenggara";
                }
                else if (this.gameObject.name == "Jawa Barat")
                {
                    bubbleProvText.text = "West Java";
                }
                else if (this.gameObject.name == "Jawa Tengah")
                {
                    bubbleProvText.text = "Central Java";
                }
                else if (this.gameObject.name == "D.I. Yogyakarta")
                {
                    bubbleProvText.text = "Yogyakarta";
                }
                else if (this.gameObject.name == "Jawa Timur")
                {
                    bubbleProvText.text = "East Java";
                }
                else
                {
                    bubbleProvText.text = this.gameObject.name;
                }
            }
            else
            {
                //bubbleProvText.text = this.gameObject.name + "\nstill preparing food...";
                bubbleProvText.text = "Still processing food...";
            }
        }
    }
    private void OnMouseExit()
    {
        if (!gm.lose && penanda.enabled)
        {
            bubbleProv.GetComponent<Animator>().SetBool("hovering", false);
            bubbleProvText.text = " ";
        }
    }
    static int idxHeart = 3;
    static bool isbreaking = false;
    private void OnMouseDown()
    {
        if(!gm.lose)
        {
            bubbleProvText.text = " ";
            //bubbleProv.SetActive(false);
            //Debug.Log("clicked " + this.gameObject.name);
            if(TM.isTutoring && TM.canClick)
            {
                TM tm = FindObjectOfType<TM>();
                if (gm.canShip && !gm.closed && !gm.isReadingRecipe)
                {
                    if (!gm.isCooking[gm.provs.IndexOf(this.gameObject.transform)] && tray_spawner.curr_tray.prov == transform)
                    {
                        if(tm.idx == 8)
                        {
                            tm.next();
                        }
                        bubbleProv.GetComponent<Animator>().SetBool("hovering", false);
                        CancelInvoke("doneBreaking");
                        isbreaking = false;
                        doneBreaking();
                        tray_spawner.ship(transform);
                    }
                }
            }
            else if(!TM.isTutoring)
            {
                if(gm.canShip && !gm.closed && !gm.isReadingRecipe)
                {
                    if (!gm.isCooking[gm.provs.IndexOf(this.gameObject.transform)] && tray_spawner.curr_tray.prov == transform)
                    {
                        bubbleProv.GetComponent<Animator>().SetBool("hovering", false);
                        CancelInvoke("doneBreaking");
                        isbreaking = false;
                        doneBreaking();
                        tray_spawner.ship(transform);
                    }
                    else if(!gm.isCooking[gm.provs.IndexOf(this.gameObject.transform)] && tray_spawner.curr_tray.prov != transform)
                    {
                        //GameObject.Find("hearts").transform.GetChild(GameObject.Find("hearts").transform.childCount - 1).GetComponent<Animator>().SetBool("break",true);
                        if(idxHeart >= 0 && !isbreaking)
                        {
                            isbreaking = true;
                            idxHeart--;
                            GameObject.Find("hearts").transform.GetChild(idxHeart).GetComponent<Animator>().SetBool("break",true);
                            //GameObject.Find("hearts").transform.GetChild(idxHeart).gameObject.SetActive(false);
                            //Debug.LogWarning(idxHeart + " dan " + GameObject.Find("hearts").transform.GetChild(idxHeart).GetComponent<Animator>().GetBool("break"));
                            gm.heart--;
                            if(gm.heart <= 0)
                            {
                                gm.lose = true;
                            }
                            Invoke("doneBreaking", 1f);
                        }
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
                    //upgradeUI.GetComponent<upgrades>().provText.text = this.gameObject.name;
                    upgradeUI.GetComponent<upgrades>().pos = transform;
                    upgradeUI.GetComponent<upgrades>().curr_prov = this;
                    upgradeUI.SetTrigger("pindah");
                }
            }
        }
    }
    void doneBreaking()
    {
        isbreaking = false;
    }
}
