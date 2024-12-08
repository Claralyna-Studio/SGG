using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class recipe_book : MonoBehaviour
{
    GM gm;
    upgrades upg;
    traySpawner trayS;
    [SerializeField] private GameObject mark;

    [SerializeField] private int index1 = 0;
    //[SerializeField] private int index2 = 0;
    [Serializable]
    struct resep
    {
        public string prov;
        public string food_name;
        public Sprite food;
        public int unlockMoney;
        public int unlockCrystal;
        [TextArea] public string description;
    }
    [SerializeField] private List<resep> recipes;
    [SerializeField] private resep curr_recipe1;
    //[SerializeField] private resep curr_recipe2;
    [SerializeField] private Animator UI;

    [SerializeField] private Image food1;
    //[SerializeField] private Image food2;
    [SerializeField] private Image drink1;
    //[SerializeField] private Image drink2;
    //[SerializeField] private TextMeshProUGUI desc1;
    //[SerializeField] private TextMeshProUGUI desc2;
    [SerializeField] private TextMeshProUGUI food_name;
    [SerializeField] private TextMeshProUGUI desc3;

    [SerializeField] private bool isUI = false;
    [SerializeField] private destroy_anim lockR;
    //[SerializeField] private Button NEXT;
    [SerializeField] private Image nextS;
    //[SerializeField] private Button PREV;
    [SerializeField] private Image prevS;
    [SerializeField] private GameObject lockRecipe;
    [SerializeField] private TextMeshProUGUI lockText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI crystalText;
    // Start is called before the first frame update
    void Start()
    {
        upg = FindObjectOfType<upgrades>();
        gm = FindObjectOfType<GM>();
        trayS = FindObjectOfType<traySpawner>();
        //UI = GameObject.Find("RECIPE_BOOK(UI)").GetComponent<Animator>();
    }
    [SerializeField] private bool canUnlock = true;
    // Update is called once per frame
    void Update()
    {
        if (isUI)
        {
/*            for (int i = 0; i < mark.transform.childCount; i++)
            {
                if (upg.pulauUnlockedName.Contains(mark.transform.GetChild(i).name))
                {
                    mark.transform.GetChild(i).GetComponent<Button>().enabled = true;
                }
                else
                {
                    mark.transform.GetChild(i).GetComponent<Button>().enabled = false;
                }
            }*/
            curr_recipe1 = recipes[index1];
            Debug.Log((index1) % 3);
            if (trayS.canProv.Contains(curr_recipe1.prov) && trayS.canProv_maxFood[trayS.canProv.IndexOf(curr_recipe1.prov)]-1 >= (index1) % 3)
            {
                Debug.Log("unlocked");
                lockRecipe.SetActive(false);
                lockRecipe.GetComponent<Animator>().SetBool("out", true);
                moneyText.text = curr_recipe1.unlockMoney.ToString("##,#");
                crystalText.text = curr_recipe1.unlockCrystal.ToString("##,#");
                food_name.text = curr_recipe1.food_name;
                desc3.text = "Description: \n" + curr_recipe1.description + "\n\n\nOrigin: " + curr_recipe1.prov;
                food1.sprite = curr_recipe1.food;
                drink1.sprite = curr_recipe1.food;
            }
            else if(!trayS.canProv.Contains(curr_recipe1.prov))
            {
                Debug.Log("island locked");
                canUnlock = false;
                lockRecipe.GetComponent<Animator>().SetBool("out", false);
                moneyText.text = curr_recipe1.unlockMoney.ToString("##,#");
                crystalText.text = curr_recipe1.unlockCrystal.ToString("##,#");
                lockRecipe.SetActive(true);
                lockText.text = "Must unlock the island first";
                food_name.text = curr_recipe1.food_name;
                desc3.text = "Description:\n?????\n\n\n\nOrigin:\n???";
                food1.sprite = curr_recipe1.food;
                drink1.sprite = curr_recipe1.food;
            }
            else if(trayS.canProv_maxFood[trayS.canProv.IndexOf(curr_recipe1.prov)] - 1 < (index1) % 3 && trayS.canProv_maxFood[trayS.canProv.IndexOf(curr_recipe1.prov)] - 1 >= (index1-1) % 3)
            {
                Debug.Log("recipe locked");
                canUnlock = true;
                //lockRecipe.GetComponent<Animator>().SetBool("out", false);
                moneyText.text = curr_recipe1.unlockMoney.ToString("##,#");
                crystalText.text = curr_recipe1.unlockCrystal.ToString("##,#");
                lockRecipe.SetActive(true);
                lockText.text = " ";
                food_name.text = curr_recipe1.food_name;
                desc3.text = "Description:\n?????\n\n\n\nOrigin:\n???";
                food1.sprite = curr_recipe1.food;
                drink1.sprite = curr_recipe1.food;
            }
            else if (trayS.canProv_maxFood[trayS.canProv.IndexOf(curr_recipe1.prov)] - 1 < (index1) % 3 && trayS.canProv_maxFood[trayS.canProv.IndexOf(curr_recipe1.prov)] - 1 < (index1 - 1) % 3)
            {
                Debug.Log("recipe locked, need prev unlocked");
                canUnlock = false;
                lockRecipe.GetComponent<Animator>().SetBool("out", false);
                moneyText.text = curr_recipe1.unlockMoney.ToString("##,#");
                crystalText.text = curr_recipe1.unlockCrystal.ToString("##,#");
                lockRecipe.SetActive(true);
                lockText.text = "Must unlock the previous recipe first";
                food_name.text = curr_recipe1.food_name;
                desc3.text = "Description:\n?????\n\n\n\nOrigin:\n???";
                food1.sprite = curr_recipe1.food;
                drink1.sprite = curr_recipe1.food;
            }
            /*            desc1.text = "Description: " + curr_recipe1.description;
                        desc2.text = "Description: " + curr_recipe2.description;
                        curr_recipe1 = recipes[index1];
                        curr_recipe2 = recipes[index2];*/

            /*            food2.sprite = curr_recipe2.food;
                        drink2.sprite = curr_recipe2.food;*/
            if (curr_recipe1.prov == "Bali" || curr_recipe1.prov == "Nusa Tenggara Timur" || curr_recipe1.prov == "Nusa Tenggara Barat")
            {
                mark.transform.Find("Sunda Kecil").SetAsLastSibling();
            }
            else if(curr_recipe1.prov == "Banten" || 
                curr_recipe1.prov == "DKI Jakarta" || 
                curr_recipe1.prov == "Jawa Barat" || 
                curr_recipe1.prov == "D.I. Yogyakarta" || 
                curr_recipe1.prov == "Jawa Tengah" || 
                curr_recipe1.prov == "Jawa Timur")
            {
                mark.transform.Find("Jawa").SetAsLastSibling();
            }
            /*else if()
            {

            }*/
            if(curr_recipe1.food.name != "bajigur" && curr_recipe1.food.name != "esdawet")
            {
                food1.enabled = true;
                drink1.enabled = false;
            }
            else
            {
                food1.enabled = false;
                drink1.enabled = true;
            }
/*            if(curr_recipe2.food.name != "bajigur" && curr_recipe2.food.name != "esdawet")
            {
                food2.enabled = true;
                drink2.enabled = false;
            }
            else
            {
                food2.enabled = false;
                drink2.enabled = true;
            }*/
            if(index1 >= recipes.Count-1)
            {
                //nextS.color = Color.gray;
                nextS.transform.parent.GetComponent<Button>().enabled = false;
                nextS.enabled = false;
            }
            else
            {
                //nextS.color = Color.white;
                nextS.transform.parent.GetComponent<Button>().enabled = true;
                nextS.enabled = true;
            }
            if (index1 <= 0)
            {
                //prevS.color = Color.gray;
                prevS.transform.parent.GetComponent<Button>().enabled = false;
                prevS.enabled = false;
            }
            else
            {
                //prevS.color = Color.white;
                prevS.transform.parent.GetComponent<Button>().enabled = true;
                prevS.enabled = true;
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("in",gm.closed);
        }
    }
    bool click = false;
    public void clicked()
    {
        if(!GameObject.Find("penanda").GetComponent<Image>().enabled)
        {
            gm.isReadingRecipe = true;
            click = !click;
            UI.SetBool("clicked", click);
            GetComponent<Animator>().SetBool("clicked", click);
        }
    }
    public void backAnimUI()
    {
        gm.isReadingRecipe = false;
        UI.SetBool("clicked", false);
        GetComponent<Animator>().SetBool("clicked", false);
    }
    bool pindahMark = false;
    int tempIndex;
    public void goToMark(int idx)
    {
        if(index1 > idx)
        {
            tempIndex = idx;
            pindahMark = true;
            flip();
        }
        else if(index1 < idx)
        {
            tempIndex = idx;
            pindahMark = true;
            flip2();
        }
    }
    public void munculUI()
    {
        UI.SetBool("in", click);
    }
    public void next()
    {
        if (!pindahMark && index1 < recipes.Count-1)
        {
            index1++;
            //index2++;
            //Debug.Log((index1) % 3);
        }
        else if(pindahMark && index1 < recipes.Count - 1)
        {
            pindahMark = false;
            index1 = tempIndex;
        }
    }
    public void prev()
    {
        if(!pindahMark && index1 > 0)
        {
            index1--;
            //index2--;
        }
        else if(pindahMark && index1 > 0)
        {
            pindahMark = false;
            index1 = tempIndex;
        }
    }
    public void flip()
    {
        GetComponent<Animator>().ResetTrigger("flip2");
        GetComponent<Animator>().SetTrigger("flip");
        
    }
    public void flip2()
    {
        GetComponent<Animator>().ResetTrigger("flip");
        GetComponent<Animator>().SetTrigger("flip2");
    }
    public void openRecipe()
    {
        if(canUnlock && gm.money >=  curr_recipe1.unlockMoney && gm.crystal >= curr_recipe1.unlockCrystal)
        {
        //Debug.Log("AAA");
            gm.addMoney(-curr_recipe1.unlockMoney);
            gm.addCrystal(-curr_recipe1.unlockCrystal);
            lockRecipe.GetComponent<Animator>().SetBool("out", true);
            lockR.prov = curr_recipe1.prov;
        }
        else
        {
            lockRecipe.GetComponent<Animator>().SetTrigger("wrong");
        }
        //button.GetComponent<Animator>().SetBool("out",true);
        //trayS.canProv_maxFood[trayS.canProv.IndexOf(button.name)]++;
    }
/*    public void openRecipeNext(GameObject button)
    {
        //button.GetComponent<Animator>().SetBool("canBuy", true);
        button.GetComponent<Button>().interactable = true;
        button.transform.GetChild(0).gameObject.SetActive(false);
        button.transform.GetChild(1).gameObject.SetActive(true);
    }*/
}
