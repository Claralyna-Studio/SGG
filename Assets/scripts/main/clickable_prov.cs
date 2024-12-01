using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickable_prov : MonoBehaviour
{
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
        tray_spawner = FindObjectOfType<traySpawner>();
        gm = FindObjectOfType<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("clicked " + this.gameObject.name);
        if(gm.canShip)
        {
            if (!gm.isCooking[gm.provs.IndexOf(this.gameObject.transform)])
            {
                tray_spawner.ship(transform);
            }
            else
            {
                //the food is still cooking
            }
        }
    }
}
