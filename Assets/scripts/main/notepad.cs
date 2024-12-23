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
            if (order && order.input_restrictions.Contains(restrictions_mark[i]))
            {
                restrictions_mark[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                restrictions_mark [i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public void tambah(int idx)
    {
        if (order && !order.input_restrictions.Contains(restrictions_mark[idx]))
        {
            order.input_restrictions.Add(restrictions_mark[idx]);
            restrictions_mark[idx].transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (order && order.input_restrictions.Contains(restrictions_mark[idx]))
        {
            //order.input_restrictions[order.input_restrictions.IndexOf(restrictions_mark[idx])] = null;
            //order.input_restrictions.Remove(restrictions_mark[idx]);
            order.input_restrictions.RemoveAll(x => x == restrictions_mark[idx]);
            restrictions_mark[idx].transform.GetChild(0).gameObject.SetActive(true);
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
    [SerializeField] private Animator sendButton;
    //bool canShip = true;
    public void send()
    {
        foreach (GameObject res in order.input_restrictions)
        {
            if (!order.curr_restrictions.Contains(res) || order.input_restrictions.Count != order.curr_restrictions.Count)
            {
                order.canShip = false; 
                break;
            }
            else
            {
                order.canShip = true;
            }
        }
        if (order.canShip)
        {
            //order.canShip = true;
            sendButton.Play("idle");
            exitNotePad();
            order.readyToShip();
        }
        else
        {
            sendButton.Play("wrong");
        }
    }
}
