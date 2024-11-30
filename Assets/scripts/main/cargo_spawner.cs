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
    public void spawn(Transform target)
    {
        GameObject clone = Instantiate(cargo_prefab, pos.position, Quaternion.identity);
        clone.GetComponent<AIAgent>().foods = food;
        clone.GetComponent<AIAgent>().masakTime = masakTime;
        clone.GetComponent<AIAgent>().speed = speed;
        clone.GetComponent<AIAgent>().target = target;
        clone.GetComponent<AIAgent>().canMove = true;
        clones.Add(clone);
        gm.cargos.Add(clone.GetComponent<AIAgent>());
        idxLast++;
    }
}
