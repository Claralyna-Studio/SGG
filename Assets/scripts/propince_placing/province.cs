using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class province : MonoBehaviour
{
    propinM gm;
    [SerializeField] private Image prov;
    [SerializeField] private Color curr_color;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<propinM>();
        prov = GameObject.Find("MAP").transform.Find(this.gameObject.name).GetComponent<Image>();
        curr_color = new Color(1, 1, 1, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = prov.gameObject.transform.position;
        //transform.localScale = prov.gameObject.transform.lossyScale * 100f;
        prov.color = Color.Lerp(prov.color, curr_color, Time.deltaTime * 10f);
    }
    [SerializeField] private pin_collider curr = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pin" && collision.TryGetComponent<pin_collider>(out pin_collider coll) && !coll.select && coll.prov == "" && !curr)
        {
            //Debug.Log("WOI");
            /*            if (collision.TryGetComponent<pin_collider>(out pin_collider coll) && !coll.select && !coll.prov)
                        {
                            coll.prov = this.gameObject;
                            coll.select = true;
                        }*/
            curr = coll;
            coll.prov = this.gameObject.name;
            coll.select = true;
            curr_color = new Color(coll.color.r, coll.color.g, coll.color.b, 0.7f);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "pin" && collision.TryGetComponent<pin_collider>(out pin_collider coll) && !coll.select && coll.prov == "" && !curr)
        {
            //Debug.Log("WOI");
            /*            if (collision.TryGetComponent<pin_collider>(out pin_collider coll) && !coll.select && !coll.prov)
                        {
                            coll.prov = this.gameObject;
                            coll.select = true;
                        }*/
            curr = coll;   
            coll.prov = this.gameObject.name;
            coll.select = true;
            curr_color = new Color(coll.color.r, coll.color.g, coll.color.b,0.7f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "pin" && collision.TryGetComponent<pin_collider>(out pin_collider coll) && coll.select && curr)
        {
            curr = null;
/*            if(collision.TryGetComponent<pin_collider>(out pin_collider coll) && coll.select)
            {
                if(coll.prov = this.gameObject)
                {
                    coll.prov = null;
                }
                coll.select = false;
            }*/
            if (coll.prov == this.gameObject.name)
            {
                coll.prov = "";
            }
            coll.select = false;
            curr_color = new Color(1, 1, 1, 0.3f);
        }
    }
}
