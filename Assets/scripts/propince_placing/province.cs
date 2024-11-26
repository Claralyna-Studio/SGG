using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class province : MonoBehaviour
{
    propinM gm;
    [SerializeField] private Image prov;
    Color curr_color;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<propinM>();
        prov = GameObject.Find("MAP").transform.Find(this.gameObject.name).GetComponent<Image>();
        curr_color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = prov.gameObject.transform.position;
        transform.localScale = prov.gameObject.transform.lossyScale * 100f;
        prov.color = Color.Lerp(prov.color, curr_color, Time.deltaTime * 10f);
    }
    pin_collider curr = null;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "pin" && collision.TryGetComponent<pin_collider>(out pin_collider coll) && !coll.select && coll.prov == "" && !curr)
        {
            /*            if (collision.TryGetComponent<pin_collider>(out pin_collider coll) && !coll.select && !coll.prov)
                        {
                            coll.prov = this.gameObject;
                            coll.select = true;
                        }*/
            curr = coll;   
            coll.prov = this.gameObject.name;
            coll.select = true;
            curr_color = coll.color;
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
            curr_color = Color.white;
        }
    }
}
