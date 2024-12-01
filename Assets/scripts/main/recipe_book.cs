using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recipe_book : MonoBehaviour
{
    struct resep
    {
        public string prov;
        public Sprite food;
    }
    [SerializeField] private List<resep> recipes;
    [SerializeField] private Animator UI;
    // Start is called before the first frame update
    void Start()
    {
        //UI = GameObject.Find("RECIPE_BOOK(UI)").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool click = false;
    public void clicked()
    {
        click = !click;
        GetComponent<Animator>().SetBool("clicked", click);
    }
    public void munculUI()
    {
        UI.SetBool("in", click);
    }
}
