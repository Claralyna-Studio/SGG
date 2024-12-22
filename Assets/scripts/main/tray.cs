using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class tray : MonoBehaviour
{
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite sad;
    [SerializeField] private Sprite angry;
    public float patience = 3f;
    [SerializeField] private float reducePricePercentage = 0.05f;
    public float cookTime;
    GameObject moneyPlus_particle;
    traySpawner spawner;
    cargo_spawner spawner2;
    GM gm;
    public string orderName;
    public Sprite food;
    public string provName;
    public Transform prov;
    //Image food_notepad;
    //Image food_notepad2;
    //TextMeshProUGUI text_notepad;
    [SerializeField] private GameObject penanda;
    [SerializeField] private Image[] img;
    public long coins = 0;
    Animator anim;
    [SerializeField] private Animator anim2;
    [SerializeField] private notepad note;
    public bool isCooking = false;
    public bool restriction = false;
    public List<string> curr_diseases;
    public List<GameObject> curr_restrictions;
    //[SerializeField] private List<GameObject> curr_restrictions;
    public bool done = false;
    TM tm;
    //public static bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        if (!restriction)
        {
            transform.GetChild(0).GetChild(1).Find("restrict").gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).GetChild(1).Find("restrict").gameObject.SetActive(true);
        }
        if(GM.day > 1)
        {
            StartCoroutine(patienceTimer());
        }
        spawner2 = FindObjectOfType<cargo_spawner>();
        moneyPlus_particle = GameObject.Find("UI").transform.GetChild(0).gameObject;
        prov = GameObject.Find("sprites").transform.Find(provName);
        //food_notepad = GameObject.Find("FOOD_NOTEPAD").GetComponent<Image>();
        //food_notepad2 = GameObject.Find("DRINK_NOTEPAD").GetComponent<Image>();
        //text_notepad = GameObject.Find("TEXT_NOTEPAD").GetComponent<TextMeshProUGUI>();
        //tray.isClicked = false;
        anim = GetComponent<Animator>();
        anim2 = GameObject.Find("NOTEPAD").GetComponent<Animator>();
        note = FindObjectOfType<notepad>();
        penanda = GameObject.Find("penanda");
        gm = FindObjectOfType<GM>();
        gm.playSfx(GameObject.Find("sfx_orderUp").GetComponent<AudioSource>());
        spawner = FindObjectOfType<traySpawner>();
        if(food.name != "bajigur" && food.name != "esdawet")
        {
            img[0].sprite = food;
            img[0].gameObject.SetActive(true);
            img[1].gameObject.SetActive(false);
            //food_notepad.sprite = food;
            //food_notepad.enabled = true;
            //food_notepad2.enabled = false;
        }
        else
        {
            img[1].sprite = food;
            img[1].gameObject.SetActive(true);
            img[0].gameObject.SetActive(false);
            //food_notepad2.sprite = food;
            //food_notepad.enabled = false;
            //food_notepad2.enabled = true;
        }
        tm = FindObjectOfType<TM>();
        if(TM.isTutoring && TM.canClick && tm)
        {
            if(tm.idx == 6 || tm.idx == 13)
            {
                int temp = tm.idx;
                tm.curr_button[temp+1] = this.transform;
                tm.next();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("cooking", isCooking);
        anim.SetBool("done", done);
    }
    [SerializeField] private bool clicked2 = false;
    public void clicked()
    {
        if (TM.isTutoring && tm)
        {
            if(tm.idx == 7)
            {
                tm.next();
            }
            if (!clicked2 && !penanda.GetComponent<Image>().enabled && spawner2.clones.Count < spawner2.maxBoats && !isCooking && !gm.lose)
            {
                //clicked2 = true;
                spawner.curr_tray = this;
                if (!restriction)
                {
                    gm.canShip = true;
                    anim.SetBool("clicked", true);
                    penanda.GetComponent<Image>().enabled = true;
                }
                else if (restriction/* && note.order != this*/)
                {
                    anim.SetBool("clicked", true);
                    callNotePad();
                    //tray.isClicked = true;
                }
            }
            else if (clicked2 && penanda.GetComponent<Image>().enabled && spawner2.clones.Count < spawner2.maxBoats && !isCooking && !gm.lose)
            {

                //clicked2 = false;
                exitNotePad();
            }
            else if (spawner2.clones.Count >= spawner2.maxBoats)
            {
                GameObject.Find("TextBoat").GetComponent<Animator>().Play("warning");
            }
        }
        else if (!TM.isTutoring)
        {
            if (!clicked2 && !penanda.GetComponent<Image>().enabled && spawner2.clones.Count < spawner2.maxBoats && !isCooking && !gm.lose)
            {
                //clicked2 = true;
                spawner.curr_tray = this;
                if (!restriction)
                {
                    gm.canShip = true;
                    anim.SetBool("clicked", true);
                    penanda.GetComponent<Image>().enabled = true;
                }
                else if (restriction/* && note.order != this*/)
                {
                    anim.SetBool("clicked", true);
                    callNotePad();
                    //tray.isClicked = true;
                }
            }
            else if (clicked2 && penanda.GetComponent<Image>().enabled && spawner2.clones.Count < spawner2.maxBoats && !isCooking && !gm.lose)
            {

                //clicked2 = false;
                exitNotePad();
            }
            else if (spawner2.clones.Count >= spawner2.maxBoats)
            {
                GameObject.Find("TextBoat").GetComponent<Animator>().Play("warning");
            }
        }
    }
    public void addRes(GameObject res)
    {
        if (curr_restrictions.Contains(res))
        {
            curr_restrictions.Remove(res);
        }
        else
        {
            curr_restrictions.Add(res);
        }
    }
    public void readyToShip()
    {
        gm.canShip = true;
        anim.SetBool("clicked", true);
        penanda.GetComponent<Image>().enabled = true;
    }
    public void hasClicked()
    {
        clicked2 = true;
    }
    public void hasClicked2()
    {
        clicked2 = false;
    }
    public void ship()
    {
        //StopAllCoroutines();
        penanda.GetComponent<Image>().enabled = false;
        gm.canShip = false;
        anim.SetBool("clicked", false);
        gm.spawning(prov, food, this);
        //tray.isClicked = false;
        //penanda.GetComponent<Image>().enabled = true;
    }
    public void callNotePad()
    {
        if(restriction)
        {
            note.order = this;
            anim2.SetBool("in", true);
        }
    }
    public void exitNotePad()
    {
        if (!restriction)
        {
            penanda.GetComponent <Image>().enabled = false;
            gm.canShip = false;
            anim.SetBool("clicked", false);
        }
        else
        {
            if (note.order == this)
            {
                penanda.GetComponent<Image>().enabled = false;
                gm.canShip = false;
                note.order = null;
                anim2.SetBool("in", false);
                anim.SetBool("clicked", false);
                //tray.isClicked = false;
            }   
        }
    }
    public void doneCooking()
    {
        StopAllCoroutines();
        done = true;
        isCooking = false;
    }
    bool collected = false;
    public void moneyBag()
    {
        if (!collected)
        {
            collected = true;
            gm.playSfx(GameObject.Find("sfx_orderMoney").GetComponent<AudioSource>());
            GameObject par = Instantiate(moneyPlus_particle,transform.parent.gameObject.transform.parent);
            par.transform.position = transform.GetChild(0).position;
            par.transform.localScale = transform.localScale * 0.2f;
            par.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+"+coins.ToString("##,#");
            par.SetActive(true);
            gm.addMoney(coins);
            anim.SetTrigger("out");
        }
    }
    public void destroying()
    {
        Destroy(gameObject);
    }
    public void getRestrictions(GameObject restrict)
    {
        if(!curr_restrictions.Contains(restrict))
        {
            curr_restrictions.Add(restrict);
        }
        else
        {
            curr_restrictions.Remove(restrict);
        }
    }
    IEnumerator patienceTimer()
    {
        yield return new WaitForSeconds(20f);
        patience--;
        long temp = 0;
        switch(patience)
        {            
            case 1:
                temp += (long)(coins * reducePricePercentage);
                coins -= temp;
                transform.GetChild(0).GetChild(5).GetComponent<Image>().sprite = sad;
                StartCoroutine(patienceTimer());
                break;

            case 2:
                temp = (long)(coins * reducePricePercentage);
                coins -= temp;
                transform.GetChild(0).GetChild(5).GetComponent<Image>().sprite = normal;
                StartCoroutine(patienceTimer());
                break;

            default:
                temp += (long)(coins * reducePricePercentage);
                coins -= temp;
                transform.GetChild(0).GetChild(5).GetComponent<Image>().sprite = angry;
                break;
        }
    }
    public void playSfx(string audioSource)
    {
        GameObject.Find(audioSource).GetComponent<AudioSource>().Play();
    }
}
