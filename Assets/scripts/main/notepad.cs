using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class notepad : MonoBehaviour
{
    public tray order;
    [SerializeField] private List<GameObject> restrictions_mark;
    [SerializeField] private Image food;
    [SerializeField] private Image drink;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI res;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < restrictions_mark.Count; i++)
        {
            if (order.curr_restrictions.Contains(restrictions_mark[i].transform.parent.gameObject))
            {
                restrictions_mark[i].SetActive(true);
            }
            else
            {
                restrictions_mark [i].SetActive(false);
            }
        }
    }
    public void buka()
    {
        string temp = " ";
        for (int i = 0; i < order.curr_diseases.Count; i++)
        {
            temp += order.curr_diseases[i];
            if(i != order.curr_diseases.Count-1)
            {
                temp += ", ";
            }
            else
            {
                temp += ".";
            }
        }
        title.text = order.orderName;
        res.text = "Restrictions:\n" + temp;
        if (order.food.name != "bajigur" && order.food.name != "esdawet")
        {
            drink.enabled = false;
            food.sprite = order.food;
            food.enabled = true;
        }
        else
        {
            food.enabled = false;
            drink.sprite = order.food;
            drink.enabled = true;
        }
    }
    public void exitNotePad()
    {
        GetComponent<Animator>().SetBool("in", false);
        order.exitNotePad();
    }
    public void send()
    {
        order.readyToShip();
    }
}
