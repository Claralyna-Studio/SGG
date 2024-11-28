using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    cargo_spawner spawner;
    [SerializeField] private GameObject pauseUI;
    public List<AIAgent> cargos;
    public List<Transform> provs;
    public List<bool> isCooking;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI crystalText;
    public int money = 0;
    public int crystal = 0;
    public bool canShip = false;
    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<cargo_spawner>();
        pauseUI.SetActive(false);
        Screen.SetResolution(1920, 1080, true);
        for(int i = 0; i < GameObject.Find("sprites").transform.childCount; i++)
        {
            isCooking.Add(false);
            provs.Add(GameObject.Find("sprites").transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
/*        for(int i=0;i<provs.Count;i++)
        {
            provs[i].GetComponent<SpriteRenderer>().enabled = isCooking[i];
        }*/
        AstarPath.active.Scan();
    }
    public void spawning(Transform prov)
    {
        int idx = provs.IndexOf(prov);
        //not cooking
        if (!isCooking[idx])
        {
            isCooking[idx] = true;
            //prov.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            foreach (PolygonCollider2D col in prov.GetComponent<clickable_prov>().colliders_to_be_unactived)
            {
                col.enabled = false;
            }
            spawner.spawn(prov);
        }
        //cooking
        else
        {

        }
    }
    bool paused = false;
    public void pause()
    {
        paused = !paused;
        pauseUI.SetActive(paused);
        if(paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void backMenu()
    {
        SceneManager.LoadScene(0);
    }
}
