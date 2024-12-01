using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickable_prov : MonoBehaviour
{
    Animator upgradeUI;
    GM gm;
    public List<PolygonCollider2D> colliders_to_be_unactived;
    traySpawner tray_spawner;
    private void Awake()
    {
        colliders_to_be_unactived.Add(this.gameObject.GetComponent<PolygonCollider2D>());
    }
    // Start is called before the first frame update
    void Start()
    {
        //upgradeUI = GameObject.Find("upgradesUI").GetComponent<Animator>();
        upgradeUI = FindObjectOfType<upgrades>().GetComponent<Animator>();
        tray_spawner = FindObjectOfType<traySpawner>();
        gm = FindObjectOfType<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if(!gm.lose)
        {
            //Debug.Log("clicked " + this.gameObject.name);
            if(gm.canShip && !gm.closed)
            {
                if (!gm.isCooking[gm.provs.IndexOf(this.gameObject.transform)] && tray_spawner.curr_tray.prov == transform)
                {
                    tray_spawner.ship(transform);
                }
                else if(!gm.isCooking[gm.provs.IndexOf(this.gameObject.transform)] && tray_spawner.curr_tray.prov != transform && GameObject.Find("hearts").transform.childCount > 0)
                {
                    GameObject.Find("hearts").transform.GetChild(GameObject.Find("hearts").transform.childCount - 1).GetComponent<Animator>().SetBool("break",true);
                }
                else
                {
                    //the food is still cooking
                }
            }
            else if(gm.closed)
            {
                if(gameObject.tag == "Bali" || gameObject.tag == "Jawa")
                {
                    //upgradeUI.GetComponent<upgrades>().clicked = true;
                    upgradeUI.GetComponent<upgrades>().pos = transform;
                    upgradeUI.GetComponent<upgrades>().curr_prov = this;
                    upgradeUI.SetTrigger("pindah");
                }
            }
        }
    }
}
