using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class fastTime : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Image img;
    GM gm;
    // Start is called before the first frame update
    void Start()
    {
        gm =FindObjectOfType<GM>();
        img = GetComponent<Image>();
    }
    bool count = false;
    // Update is called once per frame
    void Update()
    {
        if (gm.closed)
        {
            count = false;
            img.color = Color.gray;
        }
        else if (!count && !gm.closed)
        {
            count = true;
            img.color = Color.white;
        }
    }
    public void up()
    {
        if(!gm.closed)
        {
            Time.timeScale = 1;
            img.color = Color.white;
        }
    }
    public void down()
    {
        if(!gm.closed)
        {
            Time.timeScale = speed;
            img.color = Color.gray;
        }
    }
}
