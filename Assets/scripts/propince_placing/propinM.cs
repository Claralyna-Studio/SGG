using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class propinM : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float timer = 300f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI textDay;
    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] private TextMeshProUGUI textCrystal;
    [SerializeField] private List<string> all_provinces;
    [SerializeField] private List<string> curr_province;
    [SerializeField] private List<TextMeshProUGUI> texts_province;
    [SerializeField] private List<Color> colors;
    [SerializeField] private List<pin_collider> pins;
    province[] provs;
    public bool fail = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("bgm") || !PlayerPrefs.HasKey("sfx"))
        {
            PlayerPrefs.SetFloat("bgm", 1);
            PlayerPrefs.SetFloat("sfx", 1);
            bgm.value = 1;
            sfx.value = 1;
            mixer.SetFloat("bgm", PlayerPrefs.GetFloat("bgm"));
            mixer.SetFloat("sfx", PlayerPrefs.GetFloat("sfx"));
        }
        else
        {
            bgm.value = PlayerPrefs.GetFloat("bgm");
            sfx.value = PlayerPrefs.GetFloat("sfx");
            mixer.SetFloat("bgm", PlayerPrefs.GetFloat("bgm"));
            mixer.SetFloat("sfx", PlayerPrefs.GetFloat("sfx"));
        }
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        textDay.text = "Day: " + GM.day.ToString("##,#");
        GameObject.Find("LOADING").GetComponent<Animator>().Play("out");
        GameObject.Find("Timer (1)").GetComponent<Animator>().SetBool("startDay",true);
        //Screen.SetResolution(1920, 1080, true);
        provs = FindObjectsOfType<province>();
        //curr_province = provinces[Random.Range(0, provinces.Count)];
        for (int i = 0; i < 6; i++)
        {
            int idx = Random.Range(0, all_provinces.Count);
            curr_province.Add(all_provinces[idx]);
            all_provinces.RemoveAt(idx);
            //texts_province[i].text = curr_province[i];
            if (curr_province[i] == "DKI Jakarta")
            {
                texts_province[i].text = "Jakarta";
            }
            else if (curr_province[i] == "Nusa Tenggara Barat")
            {
                texts_province[i].text = "West Nusa Tenggara";
            }
            else if (curr_province[i] == "Nusa Tenggara Timur")
            {
                texts_province[i].text = "East Nusa Tenggara";
            }
            else if (curr_province[i] == "Jawa Barat")
            {
                texts_province[i].text = "West Java";
            }
            else if (curr_province[i] == "Jawa Tengah")
            {
                texts_province[i].text = "Central Java";
            }
            else if (curr_province[i] == "D.I. Yogyakarta")
            {
                texts_province[i].text = "Yogyakarta";
            }
            else if (curr_province[i] == "Jawa Timur")
            {
                texts_province[i].text = "East Java";
            }
            else
            {
                texts_province[i].text = curr_province[i];
            }
            texts_province[i].color = colors[idx];

            pins[i].color = colors[idx];
            pins[i].province = curr_province[i];
            colors.Remove(pins[i].color);
        }
        StartCoroutine(countdown());
    }
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
        int menit = Mathf.FloorToInt(timer / 60);
        int detik = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
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
    }
    bool allCorrect = true;
    int correct = 0;
    [SerializeField] private GameObject[] moneyPar;
    [SerializeField] private GameObject crystalPar;
    public void locked(Button lokk)
    {
        lokk.interactable = false;
        GameObject.Find("sfx_locked").GetComponent<AudioSource>().Play();
        int idx = 0;
        foreach(pin_collider pin in pins)
        {
            if (pin.province.ToLower() == pin.prov.ToLower())
            {
                //allCorrect = true;
                correct++;
                Debug.Log("Betul");
                pin.checkText.text = "Correct!";
                pin.check.color = Color.green;
                moneyPar[idx].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+ 50,000";
                moneyPar[idx].transform.position = new Vector2(Camera.main.WorldToScreenPoint(pin.transform.position).x, Camera.main.WorldToScreenPoint(pin.transform.position).y + 2f);
                moneyPar[idx].SetActive(true);
                GM.money += 50000;
            }
            else
            {
                allCorrect = false;
                Debug.Log("Salah");
                pin.checkText.text = "Wrong!";
                pin.check.color = Color.red;
            }
            idx++;
            pin.check.GetComponent<Animator>().SetTrigger("check");
        }
        if (/*allCorrect*/ correct >= 5)
        {
            //PlayerPrefs.SetInt("crystal", PlayerPrefs.GetInt("crystal")+1);
            crystalPar.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+ 2";
            //crystalPar.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y+5f);
            crystalPar.SetActive(true);
            GM.crystal += 2;
        }
        else if (correct == 3 || correct == 4)
        {
            crystalPar.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+ 1";
            //crystalPar.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 5f);
            crystalPar.SetActive(true);
            GM.crystal += 1;
        }
        Invoke("balikGame", 5f);    
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
            locked(GameObject.Find("LOCK").GetComponent<Button>());
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

    public void LoadData(GameData data)
    {
        //kosong
    }

    public void SaveData(ref GameData data)
    {
        data.money = GM.money;
        data.crystal = GM.crystal;
        data.day = GM.day;
    }
}
