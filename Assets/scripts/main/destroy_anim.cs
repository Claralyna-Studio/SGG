using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_anim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void destroy()
    {
        Destroy(gameObject);
    }
    public string prov;
    //public string island;
    public void openRecipe()
    {
        traySpawner trayS = FindObjectOfType<traySpawner>();

        trayS.canProv_maxFood[trayS.canProv.IndexOf(prov)]++;
    }
    public void heartBreaking()
    {
        //clickable_prov.isbreaking = true;
    }
    public void heartDoneBreaking()
    {
        //clickable_prov.isbreaking = false;
    }
/*    public void unlockIsland()
    {
        upgrades upg = FindObjectOfType<upgrades>();
        upg.pulauUnlocked[upg.pulauUnlockedName.IndexOf(island)] = true;
    }*/
}
