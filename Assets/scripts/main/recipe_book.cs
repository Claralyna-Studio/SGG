using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class recipe_book : MonoBehaviour
{
    GM gm;
    [SerializeField] private GameObject mark;

    [SerializeField] private int index1 = 0;
    //[SerializeField] private int index2 = 0;
    [Serializable]
    struct resep
    {
        public string prov;
        public string food_name;
        public Sprite food;
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

    //[SerializeField] private Button NEXT;
    [SerializeField] private Image nextS;
    //[SerializeField] private Button PREV;
    [SerializeField] private Image prevS;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GM>();
        //UI = GameObject.Find("RECIPE_BOOK(UI)").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isUI)
        {
            curr_recipe1 = recipes[index1];
            /*            desc1.text = "Description: " + curr_recipe1.description;
                        desc2.text = "Description: " + curr_recipe2.description;
                        curr_recipe1 = recipes[index1];
                        curr_recipe2 = recipes[index2];*/
            food_name.text = curr_recipe1.food_name;
            desc3.text = "Description: \n" + curr_recipe1.description;
            food1.sprite = curr_recipe1.food;
            drink1.sprite = curr_recipe1.food;

/*            food2.sprite = curr_recipe2.food;
            drink2.sprite = curr_recipe2.food;*/
            if(curr_recipe1.prov == "Bali" || curr_recipe1.prov == "Nusa Tenggara Timur" || curr_recipe1.prov == "Nusa Tenggara Barat")
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
            if(index1 >= recipes.Count)
            {
                nextS.color = Color.gray;
            }
            else
            {
                nextS.color = Color.white;
            }
            if (index1 <= 0)
            {
                prevS.color = Color.gray;
            }
            else
            {
                prevS.color = Color.white;
            }
        }
    }
    bool click = false;
    public void clicked()
    {
        gm.isReadingRecipe = true;
        click = !click;
        UI.SetBool("clicked", click);
        GetComponent<Animator>().SetBool("clicked", click);
    }
    public void backAnimUI()
    {
        gm.isReadingRecipe = false;
        UI.SetBool("clicked", false);
        GetComponent<Animator>().SetBool("clicked", false);
    }
    public void munculUI()
    {
        UI.SetBool("in", click);
    }
    public void next()
    {
        if (index1 < recipes.Count-1)
        {
            index1++;
            //index2++;
        }
    }
    public void prev()
    {
        if(index1 > 0)
        {
            index1--;
            //index2--;
        }
    }
    public void flip()
    {
        if (index1 > 0)
        {
            GetComponent<Animator>().ResetTrigger("flip2");
            GetComponent<Animator>().SetTrigger("flip");
        }
    }
    public void flip2()
    {
        if (index1 < recipes.Count - 1)
        {
            GetComponent<Animator>().ResetTrigger("flip");
            GetComponent<Animator>().SetTrigger("flip2");
        }
    }
}
