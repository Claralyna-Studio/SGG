using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class tray : MonoBehaviour
{
    GM gm;
    public Sprite food;
    [SerializeField] private GameObject penanda;
    [SerializeField] private Image[] img;
    public int coins = 0;
    Animator anim;
    [SerializeField] private Animator anim2;
    [SerializeField] private notepad note;
    public bool isCooking = false;
    public bool restriction = false;
    public List<string> restrictions;
    //public static bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        //tray.isClicked = false;
        anim = GetComponent<Animator>();
        anim2 = GameObject.Find("NOTEPAD").GetComponent<Animator>();
        note = FindObjectOfType<notepad>();
        penanda = GameObject.Find("penanda");
        gm = FindObjectOfType<GM>();
        if(food.name != "bajigur" && food.name != "esdawet")
        {
            img[0].sprite = food;
            img[0].gameObject.SetActive(true);
            img[1].gameObject.SetActive(false);
        }
        else
        {
            img[1].sprite = food;
            img[1].gameObject.SetActive(true);
            img[0].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("cooking", isCooking);
    }
    bool clicked2 = false;
    public void clicked()
    {
        if (note.order != this)
        {
            anim.SetBool("clicked",true);
            //tray.isClicked = true;
        }
    }
    public void ship()
    {
        anim.SetBool("clicked", false);
        //tray.isClicked = false;
        penanda.GetComponent<Image>().enabled = true;
    }
    public void callNotePad()
    {
        anim2.SetBool("in", true);
        note.order = this;
    }
    public void exitNotePad()
    {
        if (note.order == this)
        {
            note.order = null;
            anim2.SetBool("in", false);
            anim.SetBool("clicked", false);
            //tray.isClicked = false;
        }
    }
    public void doneCooking()
    {
        isCooking = false;
    }
    public void moneyBag()
    {
        gm.addMoney(coins);
        anim.SetTrigger("out");
    }
    public void destroying()
    {
        Destroy(gameObject);
    }
}
