using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cargo_spawner : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<traySpawner>();
        pos = GameObject.Find("sprites").transform.Find("Bali");
        gm = FindObjectOfType<GM>();
    }
    public bool tutup = true;
    // Update is called once per frame
    void Update()
    {
        cargoText.text = clones.Count.ToString() + "/" + maxBoats.ToString();
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
            if(target.gameObject.tag == "Bali")
            {
                idx = 0;
            }
            else if(target.gameObject.tag == "Jawa")
            {
                idx = 1;
            }
            GameObject clone = Instantiate(cargo_prefab, pos.position, Quaternion.identity);
            clone.transform.GetChild(1).GetComponent<AIAgent>().Tray = order;
            clone.transform.GetChild(1).GetComponent<AIAgent>().foods = food;
            clone.transform.GetChild(1).GetComponent<AIAgent>().masakTime = masakTime[idx];
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
}
