using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class fastTime : MonoBehaviour
{
    //[SerializeField] private float speed = 2f;
    [SerializeField] private Image img;
    [SerializeField] private GameObject fastUI;
    GM gm;
    // Start is called before the first frame update
    void Start()
    {
        gm =FindObjectOfType<GM>();
        img = GetComponent<Image>();
        fastUI.SetActive(false);
    }
    bool count = false;
    // Update is called once per frame
    void Update()
    {
        if (gm.closed)
        {
            fastUI.SetActive(false);
            count = false;
            img.color = Color.gray;
        }
        else if (!count && !gm.closed)
        {
            count = true;
            img.color = Color.white;
        }
        if(!gm.closed && Input.GetMouseButtonUp(0))
        {
            up();
        }
    }
    bool isFast = false;
    public void Switch()
    {
        isFast = !isFast;
        if (isFast)
        {
            down();
        }
        else
        {
            up();
        }
    }
    public void up()
    {
        if(!gm.paused)
        {
            fastUI.SetActive(false);
            Time.timeScale = 1;
            img.color = Color.white;
        }
    }
    public void down()
    {
        if(!gm.paused && !gm.closed && !gm.lose)
        {
            if(TM.isTutoring && TM.canClick)
            {
                TM tm = FindObjectOfType<TM>();
                if(tm && tm.idx == 9)
                {
                    tm.next();
                }
                else if(tm && tm.idx > 9)
                {
                    fastUI.SetActive(true);
                    Time.timeScale = 2.5f;
                    img.color = Color.gray;
                }
            }
            else if(!TM.isTutoring)
            {
                fastUI.SetActive(true);
                Time.timeScale = 2.5f;
                img.color = Color.gray;
            }
        }
    }
}
