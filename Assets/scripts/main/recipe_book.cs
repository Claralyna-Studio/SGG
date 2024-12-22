using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class recipe_book : MonoBehaviour, IDataPersistence
{
    GM gm;
    upgrades upg;
    traySpawner trayS;
    [SerializeField] private GameObject mark;

    [SerializeField] private int index1 = 0;
    [SerializeField] private List<int> all_index;
    //[SerializeField] private int index2 = 0;
    [Serializable]
    struct resep
    {
        public string prov;
        public string food_name;
        public Sprite food;
        public long unlockMoney;
        public long unlockCrystal;
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
    bool locked = false;
    [SerializeField] private bool canUnlock = true;
    // Update is called once per frame
    void Update()
    {
        if (isUI)
        {
            for (int i = 0; i < mark.transform.childCount; i++)
            {
                if(upg.pulauUnlocked[upg.pulauUnlockedName.IndexOf(mark.transform.GetChild(i).gameObject.name)])
                {
                    mark.transform.GetChild(i).GetComponent<Button>().interactable = true;
                    if(mark.transform.GetChild(i).GetChild(2))
                    {
                        mark.transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
                    }
                }
                else
                {
                    mark.transform.GetChild(i).GetComponent<Button>().interactable = false;
                    if (mark.transform.GetChild(i).GetChild(2))
                    {
                        mark.transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                    }
                }
            }
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
            curr_recipe1 = recipes[all_index[index1]];
            //Debug.Log((index1) % 3 +" dan " + (index1-1) % 3);
            if (trayS.canProv.Contains(curr_recipe1.prov) && trayS.canProv_maxFood[trayS.canProv.IndexOf(curr_recipe1.prov)]-1 >= (all_index[index1]) % 3)
            {
                //Debug.Log("unlocked");
                lockRecipe.transform.parent.gameObject.SetActive(false);
                lockRecipe.GetComponent<Animator>().SetBool("out", true);
                if(curr_recipe1.unlockMoney > 0)
                { 
                    moneyText.text = curr_recipe1.unlockMoney.ToString("##,#");
                }
                else
                {
                    moneyText.text = curr_recipe1.unlockMoney.ToString();
                }
                if (curr_recipe1.unlockCrystal > 0)
                {
                    crystalText.text = curr_recipe1.unlockCrystal.ToString("##,#");
                }
                else
                {
                    crystalText.text = curr_recipe1.unlockCrystal.ToString();
                }
                food_name.text = curr_recipe1.food_name;
                string tempProv = "";
                if (curr_recipe1.prov == "DKI Jakarta")
                {
                    tempProv = "Jakarta";
                }
                else if (curr_recipe1.prov == "Nusa Tenggara Barat")
                {
                    tempProv = "West Nusa Tenggara";
                }
                else if (curr_recipe1.prov == "Nusa Tenggara Timur")
                {
                    tempProv = "East Nusa Tenggara";
                }
                else if (curr_recipe1.prov == "Jawa Barat")
                {
                    tempProv = "West Java";
                }
                else if (curr_recipe1.prov == "Jawa Tengah")
                {
                    tempProv = "Central Java";
                }
                else if (curr_recipe1.prov == "D.I. Yogyakarta")
                {
                    tempProv = "Yogyakarta";
                }
                else if (curr_recipe1.prov == "Jawa Timur")
                {
                    tempProv = "East Java";
                }
                else
                {
                    tempProv = curr_recipe1.prov;
                }
                //desc3.text = "Description: \n" + curr_recipe1.description + "\n\n\nOrigin: " + curr_recipe1.prov;
                desc3.text = "Description: \n" + curr_recipe1.description + "\n\n\nOrigin: " + tempProv;
                food1.sprite = curr_recipe1.food;
                food1.gameObject.SetActive(true);
                drink1.sprite = curr_recipe1.food;
                drink1.gameObject.SetActive(true);
            }

            //must unlock island first
            else if(!trayS.canProv.Contains(curr_recipe1.prov))
            {
                locked = true;
                //Debug.Log("island locked");
                canUnlock = false;
                lockRecipe.GetComponent<Animator>().SetBool("out", false);
                moneyText.text = "-";
                crystalText.text = "-";
/*                if (curr_recipe1.unlockMoney > 0)
                {
                    moneyText.text = curr_recipe1.unlockMoney.ToString("##,#");
                }
                else
                {
                    moneyText.text = curr_recipe1.unlockMoney.ToString();
                }
                if (curr_recipe1.unlockCrystal > 0)
                {
                    crystalText.text = curr_recipe1.unlockCrystal.ToString("##,#");
                }
                else
                {
                    crystalText.text = curr_recipe1.unlockCrystal.ToString();
                }*/
                lockRecipe.transform.parent.gameObject.SetActive(true);
                lockText.text = "Must unlock the island first";
                food_name.text = "?????";
                desc3.text = "Description:\n?????\n\n\n\nOrigin:\n???";
                food1.sprite = curr_recipe1.food;
                food1.gameObject.SetActive(false);
                drink1.sprite = curr_recipe1.food;
                drink1.gameObject.SetActive(false);
            }

            else if(trayS.canProv_maxFood[trayS.canProv.IndexOf(curr_recipe1.prov)] - 1 < (all_index[index1]) % 3 &&
                trayS.canProv_maxFood[trayS.canProv.IndexOf(curr_recipe1.prov)] - 1 >= (all_index[index1] - 1) % 3 ||
                (all_index[index1]) % 3 == 0)
            {
                //Debug.Log("recipe locked");
                canUnlock = true;
                //lockRecipe.GetComponent<Animator>().SetBool("out", false);
                //moneyText.text = curr_recipe1.unlockMoney.ToString("##,#");
                //crystalText.text = curr_recipe1.unlockCrystal.ToString("##,#");
                if (curr_recipe1.unlockMoney > 0)
                {
                    moneyText.text = curr_recipe1.unlockMoney.ToString("##,#");
                }
                else
                {
                    moneyText.text = curr_recipe1.unlockMoney.ToString();
                }
                if (curr_recipe1.unlockCrystal > 0)
                {
                    crystalText.text = curr_recipe1.unlockCrystal.ToString("##,#");
                }
                else
                {
                    crystalText.text = curr_recipe1.unlockCrystal.ToString();
                }
                lockRecipe.transform.parent.gameObject.SetActive(true);
                lockText.text = " ";
                food_name.text = "?????";
                desc3.text = "Description:\n?????\n\n\n\nOrigin:\n???";
                food1.sprite = curr_recipe1.food;
                food1.gameObject.SetActive(false);
                drink1.sprite = curr_recipe1.food;
                drink1.gameObject.SetActive(false);
            }

            //unlock prev first 
            else if (trayS.canProv_maxFood[trayS.canProv.IndexOf(curr_recipe1.prov)] - 1 < (all_index[index1]) % 3 && 
                trayS.canProv_maxFood[trayS.canProv.IndexOf(curr_recipe1.prov)] - 1 < (all_index[index1] - 1) % 3 &&
                (all_index[index1]) % 3 > 0)
            {
                //Debug.Log("recipe locked, need prev unlocked");
                canUnlock = false;
                //canUnlock = true;


                lockRecipe.GetComponent<Animator>().SetBool("out", false);
                moneyText.text = "-";
                crystalText.text = "-";
/*                if (curr_recipe1.unlockMoney > 0)
                {
                    moneyText.text = curr_recipe1.unlockMoney.ToString("##,#");
                }
                else
                {
                    moneyText.text = curr_recipe1.unlockMoney.ToString();
                }
                if (curr_recipe1.unlockCrystal > 0)
                {
                    crystalText.text = curr_recipe1.unlockCrystal.ToString("##,#");
                }
                else
                {
                    crystalText.text = curr_recipe1.unlockCrystal.ToString();
                }*/
                lockRecipe.transform.parent.gameObject.SetActive(true);
                lockText.text = "Must unlock the previous recipe first";
                //lockText.text = " ";
                food_name.text = "?????";
                desc3.text = "Description:\n?????\n\n\n\nOrigin:\n???";
                food1.sprite = curr_recipe1.food;
                food1.gameObject.SetActive(false);
                drink1.sprite = curr_recipe1.food;
                drink1.gameObject.SetActive(false);
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

            resep temp = new resep();
            if (index1 < recipes.Count-1)
            {
                temp = recipes[all_index[index1+1]];
            }
            if(index1 >= recipes.Count-1 || !trayS.canProv.Contains(temp.prov))
            {
                //nextS.color = Color.gray;
                nextS.transform.parent.GetComponent<Button>().enabled = false;
                nextS.enabled = false;
            }
            else if(index1 < recipes.Count-1)
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
        if(TM.isTutoring && TM.canClick)
        {
            TM tm = FindObjectOfType<TM>();
            if(tm.idx == 3)
            {
                tm.next();
            }
            if (!GameObject.Find("penanda").GetComponent<Image>().enabled)
            {
                gm.isReadingRecipe = true;
                click = !click;
                UI.SetBool("clicked", click);
                GetComponent<Animator>().SetBool("clicked", click);
            }
        }
        else if(!TM.isTutoring)
        {
            if (!GameObject.Find("penanda").GetComponent<Image>().enabled)
            {
                gm.isReadingRecipe = true;
                click = !click;
                UI.SetBool("clicked", click);
                GetComponent<Animator>().SetBool("clicked", click);
            }
        }
    }
    public void backAnimUI()
    {
        if (TM.isTutoring && TM.canClick)
        {
            TM tm = FindObjectOfType<TM>();
            if (tm.idx >= 5)
            {
                //tm.next();
                gm.isReadingRecipe = false;
                UI.SetBool("clicked", false);
                GetComponent<Animator>().SetBool("clicked", false);
            }
        }
        else if (!TM.isTutoring)
        {
            gm.isReadingRecipe = false;
            UI.SetBool("clicked", false);
            GetComponent<Animator>().SetBool("clicked", false);
        }
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
        if(TM.isTutoring && TM.canClick)
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
            TM tm = FindObjectOfType<TM>();
            if(tm.idx == 4)
            {
                tm.next();
            }
        }
        else if(!TM.isTutoring)
        {
            if (!pindahMark && index1 < recipes.Count - 1)
            {
                index1++;
                //index2++;
                //Debug.Log((index1) % 3);
            }
            else if (pindahMark && index1 < recipes.Count - 1)
            {
                pindahMark = false;
                index1 = tempIndex;
            }
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
        GameObject.Find("sfx_recipePages1").GetComponent<AudioSource>().Play();
        GetComponent<Animator>().ResetTrigger("flip2");
        GetComponent<Animator>().SetTrigger("flip");
        
    }
    public void flip2()
    {
        GameObject.Find("sfx_recipePages2").GetComponent<AudioSource>().Play();
        GetComponent<Animator>().ResetTrigger("flip");
        GetComponent<Animator>().SetTrigger("flip2");
    }
    public void openRecipe()
    {
        if(!TM.isTutoring && canUnlock && GM.money >=  curr_recipe1.unlockMoney && GM.crystal >= curr_recipe1.unlockCrystal)
        {
        //Debug.Log("AAA");
            gm.addMoney(-curr_recipe1.unlockMoney);
            gm.addCrystal(-curr_recipe1.unlockCrystal);
            lockR.prov = curr_recipe1.prov;
            //lockRecipe.GetComponent<destroy_anim>().prov = curr_recipe1.prov;
            lockRecipe.GetComponent<Animator>().SetBool("out", true);
            int temp = all_index[index1];
            all_index.Remove(temp);
            for (int i = 0; i < all_index.Count; i++)
            {
                //di sort
                /*if (all_index[i] > temp)
                {
                    all_index.Insert(i, temp);
                    break;
                }
                else if (all_index[i] < temp && i == all_index.Count-1)
                {
                    all_index.Add(temp);
                }*/

                //cek apakah yg sblmnya udh kebuka
                resep tempR1 = new resep();
                resep tempR2 = new resep();
                if(i > 0 && i < all_index.Count-1)
                {
                    tempR1 = recipes[all_index[i-1]];
                    tempR2 = recipes[all_index[i+1]];
                    if(tempR2.prov != curr_recipe1.prov && tempR1.prov == curr_recipe1.prov)
                    {
                        all_index.Insert(i, temp);
                        index1 = i;
                        break;
                    }
                }
                else
                {
                    index1 = i;
                    all_index.Add(temp);
                }
            }
            //index1 = all_index[all_index.IndexOf(temp)];
        }
        else
        {
            lockRecipe.GetComponent<Animator>().SetTrigger("wrong");
        }
        //button.GetComponent<Animator>().SetBool("out",true);
        //trayS.canProv_maxFood[trayS.canProv.IndexOf(button.name)]++;
    }

    public void LoadData(GameData data)
    {
        if (data.all_index.Count == all_index.Count)
        {
            all_index = data.all_index;
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.all_index.Count != all_index.Count)
        {
            for (int i = 0; i < all_index.Count; i++)
            {
                data.all_index.Add(all_index[i]);
            }
        }
        else
        {
            for (int i = 0; i < all_index.Count; i++)
            {
                data.all_index[i] = all_index[i];
            }
        }
    }
    /*    public void openRecipeNext(GameObject button)
   {
       //button.GetComponent<Animator>().SetBool("canBuy", true);
       button.GetComponent<Button>().interactable = true;
       button.transform.GetChild(0).gameObject.SetActive(false);
       button.transform.GetChild(1).gameObject.SetActive(true);
   }*/
}
