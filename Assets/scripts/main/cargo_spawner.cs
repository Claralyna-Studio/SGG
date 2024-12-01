using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargo_spawner : MonoBehaviour
{
    GM gm;
    [SerializeField] private GameObject cargo_prefab;
    [SerializeField] private List<GameObject> clones;
    public Sprite food;
    public float masakTime = 10f;
    public float speed = 0.5f;
    int idxLast = 0;
    Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = GameObject.Find("sprites").transform.Find("Bali");
        gm = FindObjectOfType<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawn(Transform target, tray order)
    {
        GameObject clone = Instantiate(cargo_prefab, pos.position, Quaternion.identity);
        clone.transform.GetChild(1).GetComponent<AIAgent>().Tray = order;
        clone.transform.GetChild(1).GetComponent<AIAgent>().foods = food;
        clone.transform.GetChild(1).GetComponent<AIAgent>().masakTime = masakTime;
        clone.transform.GetChild(1).GetComponent<AIAgent>().speed = speed;
        clone.transform.GetChild(1).GetComponent<AIAgent>().target = target;
        clone.transform.GetChild(1).GetComponent<AIAgent>().canMove = true;
        clones.Add(clone);
        gm.cargos.Add(clone.transform.GetChild(1).GetComponent<AIAgent>());
        idxLast++;
    }
}
