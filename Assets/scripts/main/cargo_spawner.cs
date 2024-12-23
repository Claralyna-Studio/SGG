using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cargo_spawner : MonoBehaviour, IDataPersistence
{
    GM gm;
    [SerializeField] private TextMeshProUGUI cargoText;
    [SerializeField] private GameObject cargo_prefab;
    public List<GameObject> clones;
    public int maxBoats = 2;
    public Sprite food;
    traySpawner spawner;
    public List<float> masakTime;
    public float speed = 0.5f;
    int idxLast = 0;
    Transform pos;
    upgrades upg;
    // Start is called before the first frame update
    void Start()
    {
        upg = FindObjectOfType<upgrades>();
        spawner = FindObjectOfType<traySpawner>();
        pos = GameObject.Find("sprites").transform.Find("Bali");
        gm = FindObjectOfType<GM>();
    }
    public bool tutup = true;
    // Update is called once per frame
    void Update()
    {
        cargoText.text = (maxBoats - clones.Count).ToString() + "/" + maxBoats.ToString();
        if(!gm.startDay && clones.Count <= 0 && !tutup && spawner.trayCount <= 0)
        {
            tutup = true;
            gm.close_day();
        }
        clones.RemoveAll(x => x == null);
    }
    public void spawn(Transform target, tray order)
    {
        if(!gm.lose)
        {
            int idx = 0;
            /*            if(target.gameObject.tag == "Bali")
                        {
                            idx = 0;
                        }
                        else if(target.gameObject.tag == "Jawa")
                        {
                            idx = 1;
                        }*/
            if (target.gameObject.name == "Bali")
            {
                idx = 0;
            }
            else if (target.gameObject.name == "Nusa Tenggara Timur")
            {
                idx = 1;
            }
            else if (target.gameObject.name == "Nusa Tenggara Barat")
            {
                idx = 2;
            }

            else if (target.gameObject.name == "Banten")
            {
                idx = 3;
            }
            else if (target.gameObject.name == "DKI Jakarta")
            {
                idx = 4;
            }
            else if (target.gameObject.name == "Jawa Barat")
            {
                idx = 5;
            }
            else if (target.gameObject.name == "Jawa Tengah")
            {
                idx = 6;
            }
            else if (target.gameObject.name == "D.I. Yogyakarta")
            {
                idx = 7;
            }
            else if (target.gameObject.name == "Jawa Timur")
            {
                idx = 8;
            }
            GameObject clone = Instantiate(cargo_prefab, pos.position, Quaternion.identity);
            clone.transform.GetChild(1).GetComponent<AIAgent>().Tray = order;
            clone.transform.GetChild(1).GetComponent<AIAgent>().foods = food;
            clone.transform.GetChild(1).GetComponent<AIAgent>().masakTime = masakTime[idx];
            //clone.transform.GetChild(1).GetComponent<AIAgent>().masakTime = upg.foodPrep_seconds[idx];
            clone.transform.GetChild(1).GetComponent<AIAgent>().speed = speed;
            clone.transform.GetChild(1).GetComponent<AIAgent>().target = target;
            clone.transform.GetChild(1).GetComponent<AIAgent>().canMove = true;
            clones.Add(clone);
            //gm.cargos.Add(clone.transform.GetChild(1).GetComponent<AIAgent>());
            idxLast++;
        }
    }
    public void doneShip(AIAgent ai)
    {
        //Debug.LogWarning(cargo.name);
        //clones.RemoveAt(clones.IndexOf(cargo));
        clones.RemoveAll(x => x.GetComponent<AIAgent>() == ai);
        //clones.Remove(cargo);
    }

    public void LoadData(GameData data)
    {
        speed = data.boatSpeed;
        maxBoats = data.maxBoats;
    }

    public void SaveData(ref GameData data)
    {
        data.boatSpeed = speed;
        data.maxBoats = maxBoats;
    }
}
