using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sprite_to_UI_pos : MonoBehaviour
{
    GameObject pos;
    // Start is called before the first frame update
    void Awake()
    {
        pos = GameObject.Find("MAP").transform.Find(this.gameObject.name).gameObject;  
        if(pos.gameObject.GetComponent<Image>().enabled)
        {
            pos.gameObject.GetComponent<Image>().enabled = false;
        }        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = pos.gameObject.transform.position;
        transform.localScale = pos.gameObject.transform.lossyScale * 100f;
    }
}
