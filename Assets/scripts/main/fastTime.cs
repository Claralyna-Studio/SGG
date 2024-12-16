using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class fastTime : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
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
    public void up()
    {
        fastUI.SetActive(false);
        Time.timeScale = 1;
        img.color = Color.white;
    }
    public void down()
    {
        if(!gm.closed && !gm.lose)
        {
            fastUI.SetActive(true);
            Time.timeScale = speed;
            img.color = Color.gray;
        }
    }
}
