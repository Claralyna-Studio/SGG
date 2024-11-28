using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class geoM : MonoBehaviour
{
    public Vector2 pin;
    public Vector2 kota;
    [SerializeField] private TextMeshProUGUI textProv;
    [SerializeField] private TextMeshProUGUI textAkurasi;
    [SerializeField] private List<GameObject> cities;
    public GameObject city;
    [SerializeField] private float toleransi_jarak = 0.05f;
    [SerializeField] private float akurasi = 0f;
    [SerializeField] private Image bubble;
    Color correct = Color.green;
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        cities = new List<GameObject>(0);
        for(int i=0;i < GameObject.Find("CITIES").transform.childCount;i++)
        {
            cities.Add(GameObject.Find("CITIES").transform.GetChild(i).gameObject);
        }
        city = cities[Random.Range(0,cities.Count)];
        kota = city.transform.position;
        textProv.text = "Find: " + city.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool counting = false;
    float akur = 0f;
    // Update is called once per frame
    void Update()
    {
        if(counting && akur < akurasi)
        {
            akur = Mathf.MoveTowards(akur, akurasi, Time.deltaTime*100f);
            akur = Mathf.Round(akur * 100f) / 100f;
            textAkurasi.text = akur.ToString() + "%";
            bubble.color = Color.Lerp(bubble.color, correct, Time.deltaTime*2);
        }
    }
    public void pinned()
    {
        city.GetComponent<Image>().enabled = true;
        counting = true;
        pin = Camera.main.ScreenToWorldPoint(pin);
        Debug.Log(Vector2.Distance(pin,city.transform.position));
        if(Vector2.Distance(pin, city.transform.position) <= toleransi_jarak)
        {
            akurasi = 100.00f;
        }
        else
        {
            akurasi = (100f - (Vector2.Distance(pin, city.transform.position) * 20f));
        }
        akurasi = Mathf.Round(akurasi * 100f) / 100f;
        //textAkurasi.text = akurasi.ToString() + "%";
    }
}
