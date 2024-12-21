using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour, IDataPersistence
{
    [SerializeField] private List<int> waktu;
    [SerializeField] private GameObject minigameUI;
    int chanceMiniGames = 30;
    public bool isReadingRecipe = false;
    [SerializeField] private GameObject upgradesUI;
    [SerializeField] private Animator gameoverUI;
    cargo_spawner spawner;
    traySpawner traySpawner;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider sfx;
    //public List<AIAgent> cargos;
    public List<Transform> provs;
    public List<bool> isCooking;
    public int level;
    public static long day = 1;
    public bool startDay = true;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI crystalText;
    public static long money = 0;
    //public long curr_money = 0;
    //public long curr_crystal = 0;
    public static long crystal = 0;
    public bool canShip = false;
    [SerializeField] private Animator timeAnim;
    // Start is called before the first frame update
    void Start()
    {
        if(day<=0)
        {
            dayText.text = "0";
        }
        else
        {
            dayText.text = "Day: " + day.ToString("##,#");
        }
        //money = 1000000;
        //crystal = 1000000;
        GameObject.Find("LOADING").GetComponent<Animator>().Play("out");
/*        if(PlayerPrefs.HasKey("day"))
        {
            day = PlayerPrefs.GetInt("day");
        }*/
/*        if(PlayerPrefs.HasKey("money"))
        {
            money = PlayerPrefs.GetInt("money");
        }
        if(PlayerPrefs.HasKey("crystal"))
        {
            crystal = PlayerPrefs.GetInt("crystal");
        }*/
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
        Time.timeScale = 1;
        spawner = FindObjectOfType<cargo_spawner>();
        traySpawner = FindObjectOfType<traySpawner>();
        pauseUI.SetActive(false);
        //Screen.SetResolution(1920, 1080, true);
        for(int i = 0; i < GameObject.Find("sprites").transform.childCount; i++)
        {
            isCooking.Add(false);
            provs.Add(GameObject.Find("sprites").transform.GetChild(i));
        }
        StartCoroutine(timing());
    }
    char money2 = ' ';
    char crystal2 = ' ';
    //[SerializeField] private GameObject startButton;
    public bool lose = false;
    public int heart = 3;
    // Update is called once per frame
    void Update()
    {
        waktu = upgrades.waktu;
/*        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("crystal", crystal);*/
        //gameover
        if (/*GameObject.Find("hearts").transform.childCount <= 0*/ lose)
        {
            Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
            gameoverUI.SetBool("lose", true);
            Time.timeScale = 0;
        }

        if(PlayerPrefs.HasKey("bgm") && PlayerPrefs.HasKey("sfx"))
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

        timeAnim.SetBool("startDay", startDay);
        //startButton.SetActive(!startDay);
        //startButton.GetComponent<Animator>().SetBool("start",!closed);
        //moneyText.text = money.ToString("C", CultureInfo.CreateSpecificCulture("id-id"));
        /*        if (money >= 1000 && money < 1000000)
                {
                    //moneyText.text = money.ToString("##,#");
                    money2 = 'K';
                    moneyText.text = (money/1000).ToString() + money2;
                }*/
        /*        if(curr_money < money)
                {
                    curr_money++;
                }
                else
                {
                    curr_money--;
                }
                if (curr_crystal < crystal)
                {
                    curr_crystal++;
                }
                else
                {
                    curr_crystal--;
                }*/
/*        curr_money = (long)Mathf.MoveTowards(curr_money, money, Time.deltaTime * 5000f);
        curr_crystal = (long)Mathf.MoveTowards(curr_crystal, crystal, Time.deltaTime * 5000f);*/

        if (money >= 1000000)
        {
            money2 = 'M';
            moneyText.text = ((float)money / 100000f).ToString("#.00") + money2;
            //moneyText.text = (curr_money / 1000000).ToString("##,#") + money2;
        }
        else if(money > 0)
        {
            moneyText.text = money.ToString("##,#");
            //moneyText.text = curr_money.ToString("##,#");
        }
        else
        {
            moneyText.text = money.ToString();
            //moneyText.text = curr_money.ToString();
        }
/*        if (crystal >= 1000 && crystal < 1000000)
        {
            //crystalText.text = crystal.ToString("##,#");
            crystal2 = 'K';
            crystalText.text = (crystal/1000).ToString() + crystal2;
        }*/
        if(crystal >= 1000000)
        {
            crystal2 = 'M';
            crystalText.text = ((float)crystal/1000000f).ToString("#.00") + crystal2;
            //crystalText.text = (curr_crystal/1000000).ToString("##,#") + crystal2;
        }
        else if(crystal > 0)
        {
            //crystalText.text = crystal.ToString();
            crystalText.text = crystal.ToString("##,#");
            //crystalText.text = curr_crystal.ToString("##,#");
        }
        else
        {
            crystalText.text = crystal.ToString();
            //crystalText.text = curr_crystal.ToString();
        }
        /*        for(int i=0;i<provs.Count;i++)
                {
                    provs[i].GetComponent<SpriteRenderer>().enabled = isCooking[i];
                }*/

        //timer
/*        if (timerGo)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= timerAlert && !flicker)
                {
                    flicker = true;
                    timerText.color = Color.red;
                    StartCoroutine(alerting());
                }
            }
            else
            {
                timer = 0;
                isLose = true;
            }
        }*/
        

    }
    int jam = 8;
    int menit = 0;
    IEnumerator timing()
    {
        //int menit = Mathf.FloorToInt(timer / 60);
        //int detik = Mathf.FloorToInt(timer % 60);
        if(startDay)
        {
            timeText.text = string.Format("{0:00}:{1:00}", jam, menit);
            yield return new WaitForSeconds(4.5f);
            if (jam < 18)
            {
                StartCoroutine(timing());
                menit += 30;
                if(menit >= 60)
                {
                    menit = 0;
                    jam++;
                }
            }
            if(jam == 18 && menit == 0)
            {
                startDay = false;
            }
            timeText.text = string.Format("{0:00}:{1:00}", jam, menit);
        }
    }
    public bool closed = true;
    public void close_day()
    {
        day++;
        if(day % 7 == 0)
        {
            addCrystal(10);
        }
        dayText.text = "Day: " + day.ToString("##,#");
        if(day > 2)
        {
            if(day == 3 || day % 7 == 0)
            {
                chanceMiniGames = 100;
            }
            else
            {
                chanceMiniGames = 50;
            }
            if(Random.Range(1,100) <= chanceMiniGames)
            {
                minigameUI.GetComponent<Animator>().Play("in");
            }
/*            if(chanceMiniGames < 50)
            {
                chanceMiniGames += 2;
            }*/
        }
        //PlayerPrefs.SetInt("day", day);
        closed = true;
        //upgradesUI.SetActive(true);
        heart = 3;
        if(upgradesUI.GetComponent<upgrades>().curr_prov)
        {
            upgradesUI.GetComponent<Animator>().SetBool("in", true);
        }
        GameObject.Find("hearts").transform.GetChild(2).GetComponent<Animator>().SetBool("break", false);
        GameObject.Find("hearts").transform.GetChild(1).GetComponent<Animator>().SetBool("break", false);
        GameObject.Find("hearts").transform.GetChild(0).GetComponent<Animator>().SetBool("break", false);
    }
    public void start_day()
    {
        if(!lose)
        {
            playSfx(GameObject.Find("sfx_startDay").GetComponent<AudioSource>());
            closed = false;
            upgradesUI.GetComponent<Animator>().SetBool("in", false);
            //upgradesUI.SetActive(false);
            //day++;
            jam = 8;
            menit = 0;
            startDay = true;
            spawner.tutup = false;
            StartCoroutine(timing());
            StartCoroutine(traySpawner.spawnOrder());
        }
    }
    public void spawning(Transform prov, Sprite food, tray order)
    {
        if (spawner.clones.Count < spawner.maxBoats)
        {
            int idx = provs.IndexOf(prov);
            //not cooking
            if (!isCooking[idx])
            {
                isCooking[idx] = true;
                //prov.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                foreach (PolygonCollider2D col in prov.GetComponent<clickable_prov>().colliders_to_be_unactived)
                {
                    //col.enabled = false;
                    col.isTrigger = true;
                }
                spawner.food = food;
                spawner.spawn(prov, order);
            }
            //cooking
            else
            {

            }
        }
    }
    public bool paused = false;
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
/*        if(day >= 1)
        {
            //PlayerPrefs.SetInt("day", day--);
            day--;
        }*/
        GameObject.Find("LOADING").GetComponent<loading>().gameScene = "gameplay";
        GameObject.Find("LOADING").GetComponent<Animator>().Play("in");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void backMenu()
    {
        GameObject.Find("LOADING").GetComponent<loading>().gameScene = "main_menu";
        GameObject.Find("LOADING").GetComponent<Animator>().Play("in");
        //SceneManager.LoadScene(0);
    }
    public void minigame()
    {
        if(Random.Range(1,100) <= 50)
        {
            GameObject.Find("LOADING").GetComponent<loading>().gameScene = "Indo_guesser";
        }
        else
        {
            GameObject.Find("LOADING").GetComponent<loading>().gameScene = "propince";
        }
        GameObject.Find("LOADING").GetComponent<Animator>().Play("in");
    }
    public void addMoney(long plus)
    {
        money += plus;
    }
    public void addCrystal(long plus)
    {
        crystal += plus;
    }
    public void playSfx(AudioSource clip)
    {
        clip.Play();
    }

    public void LoadData(GameData data)
    {
        money = data.money;
        crystal = data.crystal;
        day = data.day;
    }

    public void SaveData(ref GameData data)
    {
        data.money = money;
        data.crystal = crystal;
        data.day = day;
    }
}
