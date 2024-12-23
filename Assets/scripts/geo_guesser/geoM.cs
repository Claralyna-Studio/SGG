using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class geoM : MonoBehaviour
{
    public Vector2 pin;
    public Vector2 kota;
    [SerializeField] private float timer = 60f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI textDay;
    [SerializeField] private TextMeshProUGUI textProv;
    [SerializeField] private TextMeshProUGUI textAkurasi;
    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] private TextMeshProUGUI textCrystal;
    [SerializeField] private List<GameObject> cities;
    public GameObject city;
    [SerializeField] private float toleransi_jarak = 0.05f;
    [SerializeField] private float akurasi = 0f;
    [SerializeField] private Image bubble;
    Color correct = Color.green;
    private void Awake()
    {
        //Screen.SetResolution(1920, 1080, true);
        cities = new List<GameObject>(0);
        /*for(int i=0;i < GameObject.Find("CITIES").transform.childCount;i++)
        {
            cities.Add(GameObject.Find("CITIES").transform.GetChild(i).gameObject);
        }*/
        GameObject[] tempCities = GameObject.FindGameObjectsWithTag("Cities");
        cities = tempCities.ToList();
        for (int i = 0; i < cities.Count; i++)
        {
            //cities.Add(GameObject.FindWithTag("Cities"));
            cities[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        city = cities[Random.Range(0,cities.Count)];
        kota = city.transform.position;
        textProv.text = "Find: " + city.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        textDay.text = "Day: " + GM.day.ToString("##,#");
        GameObject.Find("LOADING").GetComponent<Animator>().Play("out");
        GameObject.Find("Timer (1)").GetComponent<Animator>().SetBool("startDay", true);
        StartCoroutine(countdown());
    }
    bool counting = false;
    float akur = 0f;
    public bool fail = false;
    char money2;
    char crystal2;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider sfx;
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("bgm") && PlayerPrefs.HasKey("sfx"))
        {
            PlayerPrefs.SetFloat("bgm", bgm.value);
            PlayerPrefs.SetFloat("sfx", sfx.value);
            mixer.SetFloat("bgm", Mathf.Log10(PlayerPrefs.GetFloat("bgm")) * 20);
            mixer.SetFloat("sfx", Mathf.Log10(PlayerPrefs.GetFloat("sfx")) * 20);
        }
        else
        {
            mixer.SetFloat("bgm", Mathf.Log10(bgm.value) * 20);
            mixer.SetFloat("sfx", Mathf.Log10(sfx.value) * 20);
        }
        if (GM.money >= 1000000)
        {
            money2 = 'M';
            textMoney.text = ((float)GM.money / 1000000f).ToString("#.00") + money2;
            //crystalText.text = (curr_crystal/1000000).ToString("##,#") + crystal2;
        }
        else if (GM.money > 0)
        {
            //crystalText.text = crystal.ToString();
            textMoney.text = GM.money.ToString("##,#");
            //crystalText.text = curr_crystal.ToString("##,#");
        }
        else
        {
            textMoney.text = GM.money.ToString();
            //crystalText.text = curr_crystal.ToString();
        }
        if (GM.crystal >= 1000000)
        {
            crystal2 = 'M';
            textCrystal.text = ((float)GM.crystal / 1000000f).ToString("#.00") + crystal2;
            //crystalText.text = (curr_crystal/1000000).ToString("##,#") + crystal2;
        }
        else if (GM.crystal > 0)
        {
            //crystalText.text = crystal.ToString();
            textCrystal.text = GM.crystal.ToString("##,#");
            //crystalText.text = curr_crystal.ToString("##,#");
        }
        else
        {
            textCrystal.text = GM.crystal.ToString();
            //crystalText.text = curr_crystal.ToString();
        }


        if (counting && akur < akurasi)
        {

            akur = Mathf.MoveTowards(akur, akurasi, Time.deltaTime*100f);
            akur = Mathf.Round(akur * 100f) / 100f;
            textAkurasi.text = akur.ToString() + "%";
            bubble.color = Color.Lerp(bubble.color, correct, Time.deltaTime*2);
        }
        int menit = Mathf.FloorToInt(timer / 60);
        int detik = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
    }
    [SerializeField] private GameObject crystalPar;
    public void pinned()
    {
        city.transform.GetChild(0).gameObject.SetActive(true);
        pin = Camera.main.ScreenToWorldPoint(pin);
        //Debug.Log(Vector2.Distance(pin,city.transform.position));
        if(Vector2.Distance(pin, city.transform.position) <= toleransi_jarak)
        {
            akurasi = 100.00f;
            //GM.crystal++;
        }
        else
        {
            akurasi = (100f - (Vector2.Distance(pin, city.transform.position) * 20f));
        }
        akurasi = Mathf.Round(akurasi * 100f) / 100f;
        if(akurasi >= 80.00f)
        {
            crystalPar.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+ 2";
            crystalPar.SetActive(true);
            GM.crystal += 2;
        }
        else if(akurasi >= 60.00f && akur < 80.00f)
        {
            crystalPar.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+ 1";
            crystalPar.SetActive(true);
            GM.crystal += 1;
        }
        //GameObject.Find("moneyPlus_particle").SetActive(true);
        counting = true;
        Invoke("balikGame", 5f);
        //textAkurasi.text = akurasi.ToString() + "%";
    }
    void balikGame()
    {
        GameObject.Find("LOADING").GetComponent<loading>().gameScene = "gameplay";
        GameObject.Find("LOADING").GetComponent<Animator>().Play("in");
    }
    IEnumerator countdown()
    {
        if(timer <= 0)
        {
            fail = true;
            pinned();
        }
        yield return new WaitForSeconds(1);
        if(timer > 0)
        {
            timer--;
            StartCoroutine(countdown());
        }
    }
    public void playSfx(AudioSource clip)
    {
        clip.Play();
    }
    public bool paused = false;
    [SerializeField] private GameObject pauseUI;
    public void pause()
    {
        paused = !paused;
        pauseUI.SetActive(paused);
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void backMenu()
    {
        GameObject.Find("LOADING").GetComponent<loading>().gameScene = "main_menu";
        GameObject.Find("LOADING").GetComponent<Animator>().Play("in");
        //SceneManager.LoadScene(0);
    }
}
