using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
//using UnityEngine.UIElements;

public class upgrades : MonoBehaviour, IDataPersistence
{
    GM gm;
    public bool isUpgrading = false;
    public bool isUpgradingBoat = false;
    traySpawner tray_spawner;
    cargo_spawner cargo;
    [SerializeField] private List<GameObject> meja_upgrade;
    public List<string> pulauUnlockedName;
    public List<bool> pulauUnlocked;
    [Header("per prov")]
    [SerializeField] private List<float> foodPrep_seconds;
    [SerializeField] private List<int> berapaKaliUpgrade;
    [SerializeField] private List<int> crystalSpeedUp;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI crystalText;
    [SerializeField] private long moneyBoat;
    [SerializeField] private long crystalBoat;
    [SerializeField] private long moneyBoatSpeed;
    [SerializeField] private long crystalBoatSpeed;
    [SerializeField] private long money = 0;
    [SerializeField] private long crystal = 0;
    [SerializeField] private List<int> moneyUnlock;
    [SerializeField] private List<int> crystalUnlock;

    [SerializeField] private Animator unlockIslandButtons;

    [SerializeField] private Animator upgradeBoatButton;
    [SerializeField] private TextMeshProUGUI upgradeBoatText;
    [SerializeField] private TextMeshProUGUI upgradeBoatTextMoney;
    [SerializeField] private TextMeshProUGUI upgradeBoatTextCrystal;
    [SerializeField] private Button upgradeBoatSprt;

    [SerializeField] private Animator upgradeBoatButtonSpeed;
    [SerializeField] private TextMeshProUGUI upgradeBoatSpeedText;
    [SerializeField] private TextMeshProUGUI upgradeBoatSpeedTextMoney;
    [SerializeField] private TextMeshProUGUI upgradeBoatSpeedTextCrystal;
    [SerializeField] private Button upgradeBoatSpeedSprt;
    public List<Coroutine> upgrading;

    public float TimeTest;
    static bool upgradeSize = false;
    static bool upgradeSpeed = false;
    // Start is called before the first frame update
    void Start()
    {
        /*        tempText = new List<TextMeshProUGUI>(6);
                tempText.Add(null);
                tempText.Add(null);
                tempText.Add(null);
                tempText.Add(null);
                tempText.Add(null);
                tempText.Add(null);*/

        /*        if(!PlayerPrefs.HasKey("waktu0"))
                {
                    waktu[0] = 0;
                }
                else
                {
                    waktu[0] = PlayerPrefs.GetInt("waktu0");
                }
                if (!PlayerPrefs.HasKey("waktu1"))
                {
                    waktu[1] = 0;
                }
                else
                {
                    waktu[1] = PlayerPrefs.GetInt("waktu1");
                }
                if (!PlayerPrefs.HasKey("waktu2"))
                {
                    waktu[2] = 0;
                }
                else
                {
                    waktu[2] = PlayerPrefs.GetInt("waktu2");
                }
                if (!PlayerPrefs.HasKey("waktu3"))
                {
                    waktu[3] = 0;
                }
                else
                {
                    waktu[3] = PlayerPrefs.GetInt("waktu3");
                }
                if (!PlayerPrefs.HasKey("waktu4"))
                {
                    waktu[4] = 0;
                }
                else
                {
                    waktu[4] = PlayerPrefs.GetInt("waktu4");
                }*/
        upgrading = new List<Coroutine>();
        upgrading.Add(null);
        upgrading.Add(null);
        upgrading.Add(null);
        upgrading.Add(null);
        upgrading.Add(null);
        upgrading.Add(null);
        gm = FindObjectOfType<GM>();
        cargo = FindObjectOfType<cargo_spawner>();
        tray_spawner = FindObjectOfType<traySpawner>();
        for(int i = 0; i < GameObject.Find("meja (1)").transform.childCount;i++)
        {
            meja_upgrade.Add(GameObject.Find("meja (1)").transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < waktu.Count; i++)
        {
            //cara 1
            /*            long timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
                        //string value = PlayerPrefs.GetString("Time");
                        string value = waktuStamp;
                        long oldTimestamp = Convert.ToInt64(value);

                        long elapsedSeconds = timestamp - oldTimestamp;
                        TimeSpan interval = TimeSpan.FromSeconds(elapsedSeconds);
                        float days = interval.Days;
                        float hours = interval.Hours;
                        float minutes = interval.Minutes;
                        float seconds = interval.Seconds;

                        float Timesystem = (days * 24) + (hours * 60) + (minutes * 60) + seconds;
                        float TimeSaves = waktu[i];
                        float t = Timesystem - TimeSaves;
                        Debug.LogWarning(Timesystem + " :system dan save: " + t);
                        if (t < 300)
                        {
                            TimeTest = t;
                        }
                        else if (t < 600)
                        {
                            TimeTest = t - 300;
                        }
                        else if (t < 900)
                        {
                            TimeTest = t - 600;
                        }
                        else if (t < 1200)
                        {
                            TimeTest = t - 900;
                        }
                        else if (t < 1500)
                        {
                            TimeTest = t - 1200;
                        }
                        waktu[i] = (int)TimeTest;*/

            //cara 2
            //Debug.LogWarning(waktu[i]);
            //Debug.LogError(DateTime.Now.ToString() + " dan " + pausedDate.ToString());
            if (waktu[i] > 0)
            {
                waktu[i] -= (int)(DateTime.Now - pausedDate).TotalSeconds;
                if(waktu[i] < 0)
                {
                    waktu[i] = 1;
                }
                switch (i)
                {
                    default:
                        break;

                    case 0:
                        //food prep
                        isUpgrading = true;
                        non_active[0].SetActive(false);
                        non_active[1].SetActive(false);
                        non_active[2].SetActive(true);
                        non_active[3].SetActive(true);
                        coll.gameObject.SetActive(false);
                        //StopCoroutine(upgrading[0]);
                        upgrading[0] = StartCoroutine(upgradeTimer(i));
                        break;

                    case 1:
                        //boat size
                        isUpgradingBoat = true;
                        upgradeSize = true;
                        upgradeSpeed = false;
                        //StopCoroutine(upgrading[1]);
                        upgrading[1] = StartCoroutine(upgradeTimerBoat(i, tempText[1]));
                        break;

                    case 2:
                        //boat speed
                        isUpgradingBoat = true;
                        upgradeSize = false;
                        upgradeSpeed = true;
                        //StopCoroutine(upgrading[2]);
                        upgrading[2] = StartCoroutine(upgradeTimerBoat(i, tempText[2]));
                        break;

                    case 3:
                        //tray slot
                        //StopCoroutine(upgrading[3]);
                        upgrading[3] = StartCoroutine(upgradeTimerSlot());
                        break;

                    case 4:
                        break;

                }
            }
        }
        //pindah();
        //Debug.LogError("Start: " + DateTime.Now.ToString() + " dan " + pausedDate.ToString());
    }
    static DateTime pausedDate;
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            //cara 1
            /*            //your app is NO LONGER in the background
                        for(int i=0;i<waktu.Count;i++)
                        {
                            long timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
                            //string value = PlayerPrefs.GetString("Time");
                            string value = waktuStamp;
                            long oldTimestamp = Convert.ToInt64(value);

                            long elapsedSeconds = timestamp - oldTimestamp;
                            TimeSpan interval = TimeSpan.FromSeconds(elapsedSeconds);
                            float days = interval.Days;
                            float hours = interval.Hours;
                            float minutes = interval.Minutes;
                            float seconds = interval.Seconds;

                            float Timesystem = (days * 24) + (hours * 60) + (minutes * 60) + seconds;
                            float TimeSaves = waktu[i];
                            float t = Timesystem - TimeSaves;
                            Debug.LogWarning(Timesystem + " :system dan save: " + t);
                            if (t < 300)
                            {
                                TimeTest = t;
                            }
                            else if (t < 600)
                            {
                                TimeTest = t - 300;
                            }
                            else if (t < 900)
                            {
                                TimeTest = t - 600;
                            }
                            else if (t < 1200)
                            {
                                TimeTest = t - 900;
                            }
                            else if (t < 1500)
                            {
                                TimeTest = t - 1200;
                            }
                            waktu[i] = (int)TimeTest;
                        }*/

            //cara 2
            //Debug.LogWarning("game paused");
            pausedDate = DateTime.Now;
        }
        else
        {
            //cara 1
            /*            long timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
                        string value = timestamp.ToString();
                        waktuStamp = value;*/

            //cara 2
            //Debug.LogWarning("game resumed");
/*            for (int i = 0; i < waktu.Count; i++)
            {
                if (waktu[i] > 0)
                {
                    waktu[i] -= (int)(DateTime.Now - pausedDate).TotalSeconds;
                    if (waktu[i] < 0)
                    {
                        waktu[i] = 0;
                    }
                    *//*switch (i)
                    {
                        default:
                            break;

                        case 0:
                            //food prep
                            isUpgrading = true;
                            if(upgrading[i]!=null)
                            {
                                StopCoroutine(upgrading[i]);
                            }
                            upgrading[0] = StartCoroutine(upgradeTimer(i));
                            break;

                        case 1:
                            //boat size
                            isUpgradingBoat = true;
                            upgradeSize = true;
                            upgradeSpeed = false;
                            if (upgrading[i] != null)
                            {
                                StopCoroutine(upgrading[i]);
                            }
                            upgrading[1] = StartCoroutine(upgradeTimerBoat(i, tempText[1]));
                            break;

                        case 2:
                            //boat speed
                            isUpgradingBoat = true;
                            upgradeSize = false;
                            upgradeSpeed = true;
                            if (upgrading[i] != null)
                            {
                                StopCoroutine(upgrading[i]);
                            }
                            upgrading[2] = StartCoroutine(upgradeTimerBoat(i, tempText[2]));
                            break;

                        case 3:
                            //tray slot
                            if (upgrading[i] != null)
                            {
                                StopCoroutine(upgrading[i]);
                            }
                            upgrading[3] = StartCoroutine(upgradeTimerSlot());
                            break;

                        case 4:
                            break;

                    }*//*
                }
            }*/
        }
    }
    private void OnApplicationQuit()
    {
        //cara 1
        /*        long timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
                string value = timestamp.ToString();
                waktuStamp = value;*/

        //cara 2
        pausedDate = DateTime.Now;
    }
    public void unlockPulau(GameObject pulau)
    {
        int idx = 0;
        if(pulau.name == "Jawa")
        {
            idx = 0;
        }
        else if (pulau.name == "Kalimantan")
        {
            idx = 1;
        }
        else if (pulau.name == "Sulawesi")
        {
            idx = 2;
        }
        else if (pulau.name == "Papua")
        {
            idx = 3;
        }
        else if (pulau.name == "Sumatra")
        {
            idx = 4;
        }
        if (pulauUnlockedName.Contains(pulau.name) && GM.money >= moneyUnlock[idx] && GM.crystal >= crystalUnlock[idx])
        {
            pulauUnlocked[pulauUnlockedName.IndexOf(pulau.name)] = true;
            for (int i = 0; i < GameObject.Find("sprites").transform.childCount; i++)
            {
                if (pulauUnlocked[pulauUnlockedName.IndexOf(GameObject.Find("sprites").transform.GetChild(i).tag)])
                {
                    //Debug.Log(GameObject.Find("sprites").transform.GetChild(i).name);
                    if(!tray_spawner.canProv.Contains(GameObject.Find("sprites").transform.GetChild(i).name))
                    {
                        tray_spawner.canProv.Add(GameObject.Find("sprites").transform.GetChild(i).name);
                        tray_spawner.canProv_maxFood.Add(1);
                    }
                }
            }
            gm.addMoney(-moneyUnlock[idx]);
            gm.addCrystal(-crystalUnlock[idx]);
        }
        pulau.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //deactivate unlock button
        for(int i=0;i<GameObject.Find("buttonsUnlock").transform.childCount;i++)
        {
            if(pulauUnlocked[pulauUnlockedName.IndexOf(GameObject.Find("buttonsUnlock").transform.GetChild(i).GetChild(2).gameObject.name)])
            {
                //Debug.Log(GameObject.Find("buttonsUnlock").transform.GetChild(i).GetChild(2).gameObject.name);
                GameObject.Find("buttonsUnlock").transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                GameObject.Find("buttonsUnlock").transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
            }
        }

        if (cargo.masakTime[idx] < 3)
        {
            buy_button.gameObject.SetActive(false);
            timerText.text = "Maximum food preperations reached!";
        }
        else
        {
            if (waktu[0] <= 0)
            {
                float currFoodPrep = foodPrep_seconds[idx];
                timerText.text = "Faster Food Preperations: \n" + currFoodPrep.ToString() + " seconds -> " + (currFoodPrep -= 1).ToString() + " seconds";
/*                if (foodPrep_seconds[idx] > 1)
                {
                    timerText.text = "Faster Food Preperations: \n" + currFoodPrep.ToString() + " seconds -> " + (currFoodPrep -= 1).ToString() + " seconds";
                }
                else
                {
                    buy_button.gameObject.SetActive(false);
                    timerText.text = "Maximum Food Preperations Time Reached!";
                }*/
            }
            buy_button.gameObject.SetActive(true);
        }
        if (money > 0)
        {
            moneyText.text = money.ToString("##,#");
        }
        else
        {
            moneyText.text = money.ToString();
        }
        if (crystal > 0)
        {
            crystalText.text = crystal.ToString("##,#");
        }
        else
        {
            crystalText.text = crystal.ToString();
        }
        non_active[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = crystalSpeedUp[idx].ToString();
        upgradeBoatButton.GetComponent<Animator>().SetBool("in",gm.closed);
        unlockIslandButtons.SetBool("in",gm.closed);

        upgradeBoatSpeedTextMoney.text = moneyBoatSpeed.ToString("#,##");
        upgradeBoatSpeedTextCrystal.text = crystalBoatSpeed.ToString("#,##");
        upgradeBoatTextMoney.text = moneyBoat.ToString("#,##");
        upgradeBoatTextCrystal.text = crystalBoat.ToString("#,##");

        if(!isUpgradingBoat)
        {
            if (GM.money >= moneyBoat && GM.crystal >= crystalBoat)
            {
                upgradeBoatSprt.GetComponent<Image>().enabled = true;
                upgradeBoatButton.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.white;
            }
            else
            {
                upgradeBoatSprt.GetComponent<Image>().enabled = false;
                upgradeBoatButton.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
            }
            if(GM.money >= moneyBoatSpeed && GM.crystal >= crystalBoatSpeed)
            {
                upgradeBoatSpeedSprt.GetComponent<Image>().enabled = true;
                upgradeBoatButtonSpeed.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.white;
            }
            else
            {
                upgradeBoatSpeedSprt.GetComponent<Image>().enabled = false;
                upgradeBoatButtonSpeed.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
            }
        }
        else
        {
            upgradeBoatSprt.GetComponent <Image>().enabled = false;
            upgradeBoatSpeedSprt.GetComponent<Image>().enabled = false;
            upgradeBoatButton.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
            upgradeBoatButtonSpeed.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;

        }

        if (cargo.maxBoats >= 10)
        {
            upgradeBoatButton.SetBool("in", false);
        }
        else
        {
            upgradeBoatButton.SetBool("in", gm.closed);
        }

        if (cargo.speed >= 2)
        {
            upgradeBoatButtonSpeed.SetBool("in", false);
        }
        else
        {
            upgradeBoatButtonSpeed.SetBool("in", gm.closed);
        }
        //can upgrade foodprep
        if (GM.money >= money && GM.crystal >= crystal && waktu[0] <= 0)
        {
            //buy_button.interactable = true;
            buy_button.gameObject.GetComponent<Image>().color = Color.white;
        }
        else
        {
            //buy_button.interactable = false;
            buy_button.gameObject.GetComponent<Image>().color = Color.red;
        }

        //if(clicked) GetComponent<Animator>().SetBool("in", gm.closed);
        GetComponent<Animator>().SetBool("in", gm.closed);
        if(!gm.startDay && gm.closed)
        {
            if(traySpawner.trayMax < 6)
            {
                for(int i = traySpawner.trayMax; i < meja_upgrade.Count;i++)
                {
                    //meja_upgrade[i].SetActive(true);
                    if(i == traySpawner.trayMax || traySpawner.trayMax == 5)
                    {
                        meja_upgrade[i].GetComponent<Animator>().SetBool("in",true);
                    }
                    else
                    {
                        meja_upgrade[i].GetComponent <Animator>().SetBool("in",false);
                    }
                    //Debug.Log("A");
                }
            }
        }
        else
        {
            if (traySpawner.trayMax < 6)
            {
                for (int i = traySpawner.trayMax; i < meja_upgrade.Count; i++)
                {
                    //meja_upgrade[i].SetActive(false);
                    meja_upgrade[i].GetComponent<Animator>().SetBool("in", false);
                    //Debug.Log("B");
                }
                for (int i = 2; i < traySpawner.trayMax; i++)
                {
                    meja_upgrade[i].GetComponent<Animator>().SetBool("in", true);
                }
            }
        }
    }
    public Transform pos;
    public clickable_prov curr_prov;
    public bool clicked = false;
    [Header("0 = upgradeUI\n1 = boat size\n2 = boat speed\n3 = upgrade slot\n4,5,6,7,8,9,10,11,12 = ...")]
    public static List<int> waktu;
    //public static string waktuStamp;
    public List<TextMeshProUGUI> tempText;
    public List<TextMeshProUGUI> tempTextSlot;
    public TextMeshProUGUI provText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button buy_button;
    [Header("0 = moneyText, 1 = textSpeedUp, 2 = crystalButton")]
    [SerializeField] private GameObject[] non_active;
    //[SerializeField] private Button crystalButton;
    public void pindah()
    {
        //upgrading.Add(StartCoroutine(upgradeTimer(1)));
        if (curr_prov.gameObject.name == "Bali")
        {
            idx = 0;
            /*            if (berapaKaliUpgrade[idx] == 0)
            {
                money = 50000;
                crystal = 0;
            }
            else if (berapaKaliUpgrade[idx] == 1)
            {
                money = 100000;
                crystal = 0;
            }
            else if (berapaKaliUpgrade[idx] == 2)
            {
                money = 150000;
                crystal = 1;
            }
            else if (berapaKaliUpgrade[idx] == 3)
            {
                money = 200000;
                crystal = 1;
            }
            else
            {
                money = 250000;
                crystal = 2;
            }*/
        }
        else if(curr_prov.gameObject.name == "Nusa Tenggara Timur")
        {
            idx = 1;
            /*            if (berapaKaliUpgrade[idx] == 0)
            {
                money = 50000;
                crystal = 0;
            }
            else if (berapaKaliUpgrade[idx] == 1)
            {
                money = 100000;
                crystal = 0;
            }
            else if (berapaKaliUpgrade[idx] == 2)
            {
                money = 150000;
                crystal = 1;
            }
            else if (berapaKaliUpgrade[idx] == 3)
            {
                money = 200000;
                crystal = 1;
            }
            else
            {
                money = 250000;
                crystal = 2;
            }*/
        }
        else if (curr_prov.gameObject.name == "Nusa Tenggara Barat")
        {
            idx = 2;
            /*            if (berapaKaliUpgrade[idx] == 0)
                        {
                            money = 50000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 1)
                        {
                            money = 100000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 2)
                        {
                            money = 150000;
                            crystal = 1;
                        }
                        else if (berapaKaliUpgrade[idx] == 3)
                        {
                            money = 200000;
                            crystal = 1;
                        }
                        else
                        {
                            money = 250000;
                            crystal = 2;
                        }*/
        }

        else if(curr_prov.gameObject.name == "Banten")
        {
            idx = 3;
            /*            if (berapaKaliUpgrade[idx] == 0)
                        {
                            money = 50000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 1)
                        {
                            money = 100000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 2)
                        {
                            money = 150000;
                            crystal = 1;
                        }
                        else if (berapaKaliUpgrade[idx] == 3)
                        {
                            money = 200000;
                            crystal = 1;
                        }
                        else
                        {
                            money = 250000;
                            crystal = 2;
                        }*/
        }
        else if (curr_prov.gameObject.name == "DKI Jakarta")
        {
            idx = 4;
            /*            if (berapaKaliUpgrade[idx] == 0)
                        {
                            money = 50000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 1)
                        {
                            money = 100000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 2)
                        {
                            money = 150000;
                            crystal = 1;
                        }
                        else if (berapaKaliUpgrade[idx] == 3)
                        {
                            money = 200000;
                            crystal = 1;
                        }
                        else
                        {
                            money = 250000;
                            crystal = 2;
                        }*/
        }
        else if (curr_prov.gameObject.name == "Jawa Barat")
        {
            idx = 5;
            /*            if (berapaKaliUpgrade[idx] == 0)
                        {
                            money = 50000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 1)
                        {
                            money = 100000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 2)
                        {
                            money = 150000;
                            crystal = 1;
                        }
                        else if (berapaKaliUpgrade[idx] == 3)
                        {
                            money = 200000;
                            crystal = 1;
                        }
                        else
                        {
                            money = 250000;
                            crystal = 2;
                        }*/
        }
        else if (curr_prov.gameObject.name == "Jawa Tengah")
        {
            idx = 6;
            /*            if (berapaKaliUpgrade[idx] == 0)
                        {
                            money = 50000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 1)
                        {
                            money = 100000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 2)
                        {
                            money = 150000;
                            crystal = 1;
                        }
                        else if (berapaKaliUpgrade[idx] == 3)
                        {
                            money = 200000;
                            crystal = 1;
                        }
                        else
                        {
                            money = 250000;
                            crystal = 2;
                        }*/
        }
        else if (curr_prov.gameObject.name == "D.I. Yogyakarta")
        {
            idx = 7;
            /*            if (berapaKaliUpgrade[idx] == 0)
                        {
                            money = 50000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 1)
                        {
                            money = 100000;
                            crystal = 0;
                        }
                        else if (berapaKaliUpgrade[idx] == 2)
                        {
                            money = 150000;
                            crystal = 1;
                        }
                        else if (berapaKaliUpgrade[idx] == 3)
                        {
                            money = 200000;
                            crystal = 1;
                        }
                        else
                        {
                            money = 250000;
                            crystal = 2;
                        }*/
        }
        else if (curr_prov.gameObject.name == "Jawa Timur")
        {
            idx = 8;
/*            if (berapaKaliUpgrade[idx] == 0)
            {
                money = 50000;
                crystal = 0;
            }
            else if (berapaKaliUpgrade[idx] == 1)
            {
                money = 100000;
                crystal = 0;
            }
            else if (berapaKaliUpgrade[idx] == 2)
            {
                money = 150000;
                crystal = 1;
            }
            else if (berapaKaliUpgrade[idx] == 3)
            {
                money = 200000;
                crystal = 1;
            }
            else
            {
                money = 250000;
                crystal = 2;
            }*/
        }
        if (berapaKaliUpgrade[idx] == 0)
        {
            money = 50000;
            crystal = 0;
        }
        else if (berapaKaliUpgrade[idx] == 1)
        {
            money = 100000;
            crystal = 0;
        }
        else if (berapaKaliUpgrade[idx] == 2)
        {
            money = 150000;
            crystal = 1;
        }
        else if (berapaKaliUpgrade[idx] == 3)
        {
            money = 200000;
            crystal = 1;
        }
        else
        {
            money = 250000;
            crystal = 2;
        }
        //transform.position = pos.position;
        provText.text = curr_prov.name;
        transform.position = Camera.main.WorldToScreenPoint(pos.position);
        clicked = true;
        if (waktu[0] > 0)
        {
            timerText.text = "Calculating...";
        }
    }
    public void finishUpg()
    {
        if(GM.crystal >= crystalSpeedUp[idx])
        {
            timerText.text = "Finishing Up...";
            non_active[0].SetActive(true);
            non_active[1].SetActive(true);
            non_active[2].SetActive(false);
            non_active[3].SetActive(false);
            //GM.crystal -= crystalSpeedUp[idx];
            gm.addCrystal(-crystalSpeedUp[idx]);
            crystalSpeedUp[idx] += 3;
            isUpgrading = false;
            waktu[0] = 0;
        }
    }
    [SerializeField] private int idx = 0;
    public void upgradeBoat(TextMeshProUGUI text)
    {
        if(!isUpgradingBoat && GM.money >= moneyBoat && GM.crystal >= crystalBoat)
        {
            //Debug.LogWarning(text.gameObject.name);
            tempText[1] = text;
            //Debug.LogWarning("clicked");
            isUpgradingBoat = true;
            upgradeSize = true;
            upgradeSpeed = false;
            gm.addMoney(-moneyBoat);
            gm.addCrystal(-crystalBoat);
            //GM.money -= moneyBoat;
            //GM.crystal -= crystalBoat;
            //waktu.Add(30);
            waktu[1] = 30;
            //int index = waktu.Count-1;
            //upgrading.Add(null);
            upgrading[1] = StartCoroutine(upgradeTimerBoat(1, tempText[1]));
        }
        else
        {
            Debug.LogWarning("gk cukup");
        }
    }
    public void upgradeBoatSpeed(TextMeshProUGUI text)
    {
        if (!isUpgradingBoat && GM.money >= moneyBoatSpeed && GM.crystal >= crystalBoatSpeed)
        {
            tempText[2] = text;
            Debug.LogWarning("clicked");
            isUpgradingBoat = true;
            upgradeSize = false;
            upgradeSpeed = true;
            gm.addMoney(-moneyBoatSpeed);
            gm.addCrystal(-crystalBoatSpeed);
            //GM.money -= moneyBoatSpeed;
            //GM.crystal -= crystalBoatSpeed;
            waktu[2] = 30;
            //int index = waktu.Count - 1;
            //upgrading.Add(null);
            upgrading[2] = StartCoroutine(upgradeTimerBoat(2, tempText[2]));
        }
        else
        {
            Debug.LogWarning("gk cukup");
        }
    }
    public void buy()
    {
        if(GM.money >= money && GM.crystal >= crystal)
        {
            non_active[0].SetActive(false);
            non_active[1].SetActive(false);
            non_active[2].SetActive(true);
            non_active[3].SetActive(true);
            isUpgrading = true;
            /*        if (curr_prov.gameObject.tag == "Bali")
                    {
                        idx = 0;
                    }
                    else if (curr_prov.gameObject.tag == "Jawa")
                    {
                        idx = 1;
                    }*/
            if (berapaKaliUpgrade[idx] == 0)
            {
                waktu[0] = 30;
            }
            else if(berapaKaliUpgrade[idx] == 1)
            {
                waktu[0] = 60;
            }
            else if (berapaKaliUpgrade[idx] == 2)
            {
                waktu[0] = 60;
            }
            else if (berapaKaliUpgrade[idx] == 3)
            {
                waktu[0] = 300;
            }
            else
            {
                waktu[0] = 600;
            }
            gm.addMoney(-money);
            gm.addCrystal(-crystal);
            //GM.money -= money;
            //GM.crystal -= crystal;
            //Debug.Log(upgrading.Count);
            tempText[0] = timerText;
            upgrading[0] = StartCoroutine(upgradeTimer(0));
        }
    }
    public IEnumerator upgradeTimer(int idx)
    {
        if(waktu[idx] > 0)
        {
            int menit1 = Mathf.FloorToInt(waktu[idx] / 60);
            int detik1 = Mathf.FloorToInt(waktu[idx] % 60);
            timerText.text = string.Format("{0:00}:{1:00}", menit1, detik1);
        }
        isUpgrading = true;
        coll.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        waktu[idx]--;
        int menit = Mathf.FloorToInt(waktu[idx] / 60);
        int detik = Mathf.FloorToInt(waktu[idx] % 60);
        timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
        
        if (waktu[idx] > 0) StartCoroutine(upgradeTimer(idx));
        else
        {
            non_active[0].SetActive(true);
            non_active[1].SetActive(true);
            non_active[2].SetActive(false);
            non_active[3].SetActive(false);
            isUpgrading = false;
            cargo.masakTime[this.idx]--;
            foodPrep_seconds[this.idx] -= 1;
            berapaKaliUpgrade[this.idx]++;
            float currFoodPrep = foodPrep_seconds[this.idx];
            timerText.text = "Faster Food Preperations: \n" + currFoodPrep.ToString() + " seconds -> " + (currFoodPrep -= 1).ToString() + " seconds";
            coll.SetActive(true);
        }
    }
    public IEnumerator upgradeTimerBoat(int idx, TextMeshProUGUI text)
    {
        int menit = Mathf.FloorToInt(waktu[idx] / 60);
        int detik = Mathf.FloorToInt(waktu[idx] % 60);
        text.text = string.Format("{0:00}:{1:00}", menit, detik);
        yield return new WaitForSecondsRealtime(1);
        waktu[idx]--;
        menit = Mathf.FloorToInt(waktu[idx] / 60);
        detik = Mathf.FloorToInt(waktu[idx] % 60);
        text.text = string.Format("{0:00}:{1:00}", menit, detik); 
        if (waktu[idx] > 0) StartCoroutine(upgradeTimerBoat(idx,text));
        else
        {
            isUpgradingBoat = false;
            if(upgradeSize)
            {
                moneyBoat += 5;
                crystalBoat += 5;
                cargo.maxBoats++;
                //text.text = "Increase max boats to " + (cargo.maxBoats + 1).ToString();
                text.text = "Add boat (" + (cargo.maxBoats + 1).ToString() + ")";
            }
            else if(upgradeSpeed)
            {
                moneyBoatSpeed += 3;
                crystalBoatSpeed += 3;
                cargo.speed += 0.2f;
                //text.text = "Increase boat speed to " + (cargo.speed + 0.2f).ToString();
                text.text = "Increase boat speed";
            }
            upgradeSpeed = false;
            upgradeSize = false;
        }
    }
    [SerializeField] private Button[] slotButtons;
    public IEnumerator upgradeTimerSlot()
    {
        foreach (Button button in slotButtons)
        {
            button.interactable = false;
            button.GetComponent<Image>().raycastTarget = false;
        }
        int idx = 3;
        //Debug.LogWarning("countdown");
        /* foreach(TextMeshProUGUI text in tempTextSlot)
         {
             tempText[idx] = text;
         }*/
        //size tempTextSlot = 5
        int menit = Mathf.FloorToInt(waktu[idx] / 60);
        int detik = Mathf.FloorToInt(waktu[idx] % 60);
        //tempText[idx].text = string.Format("{0:00}:{1:00}", menit, detik);
        tempTextSlot[traySpawner.trayMax - 2].text = string.Format("{0:00}:{1:00}", menit, detik);

        yield return new WaitForSecondsRealtime(1f);

        waktu[idx]--;
        menit = Mathf.FloorToInt(waktu[idx] / 60);
        detik = Mathf.FloorToInt(waktu[idx] % 60);
        //tempText[idx].text = string.Format("{0:00}:{1:00}", menit, detik);
        tempTextSlot[traySpawner.trayMax - 2].text = string.Format("{0:00}:{1:00}", menit, detik);

        if (waktu[idx] > 0)
        {
            //Debug.LogWarning("A");
            StartCoroutine(upgradeTimerSlot());
        }
        else
        {
            foreach (Button button in slotButtons)
            {
                button.interactable = true;
                button.GetComponent<Image>().raycastTarget = true;
            }
            //Debug.LogWarning("B");
            tray_spawner.doneUpgrade();
        }
    }
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private GameObject coll;
    public void hoverWindow()
    {
        //Debug.LogWarning("hover");
        //GetComponent<CanvasGroup>().alpha = 0.3f;
        canvas.alpha = 0.3f;
    }
    public void unhoverWindow()
    {
        //Debug.LogWarning("unhover");
        //GetComponent<CanvasGroup>().alpha = 1f;
        canvas.alpha = 1f;
    }

    //public int tempWaktu;
    public void LoadData(GameData data)
    {
        //Debug.Log("PANGGIL");
        //throw new NotImplementedException();
        //pausedDate = data.pausedDate;

        pausedDate = data.pausedDate.PausedDate;
        //Debug.LogError("Load:" + DateTime.Now.ToString() + " dan " + pausedDate.ToString());
        //Debug.LogWarning(pausedDate);

        if (waktu == null)
        {
            waktu = new List<int>();
            waktu.Add(0);
            waktu.Add(0);
            waktu.Add(0);
            waktu.Add(0);
            waktu.Add(0);
            waktu.Add(0);
        }
        if (data.pulauUnlockedName.Count == pulauUnlockedName.Count)
        {
            pulauUnlockedName = data.pulauUnlockedName;
        }
        if (data.pulauUnlocked.Count == pulauUnlocked.Count)
        {
            pulauUnlocked = data.pulauUnlocked;
        }
        if (data.foodPrep_seconds.Count == foodPrep_seconds.Count)
        {
            foodPrep_seconds = data.foodPrep_seconds;
        }
        if(data.berapaKaliUpgrade.Count == berapaKaliUpgrade.Count)
        {
            berapaKaliUpgrade = data.berapaKaliUpgrade;
        }
        if(data.crystalSpeedUp.Count == crystalSpeedUp.Count)
        {
            crystalSpeedUp = data.crystalSpeedUp;
        }
        //tempText = data.tempText;
        if (data.waktu.Count == waktu.Count)
        {
            waktu = data.waktu;
        }
        //upgrading = data.upgrading;
/*        for (int i = 0; i < 6; i++)
        {
            data.waktu.TryGetValue(i.ToString(), out tempWaktu);
            waktu.Add(tempWaktu);

        }*/
        //waktuStamp = data.waktuStamp;
    }

    public void SaveData(ref GameData data)
    {
        //Debug.Log("PANGGIL");
        /*PlayerPrefs.SetInt("waktu0", waktu[0]);
        PlayerPrefs.SetInt("waktu1", waktu[1]);
        PlayerPrefs.SetInt("waktu2", waktu[2]);
        PlayerPrefs.SetInt("waktu3", waktu[3]);
        PlayerPrefs.SetInt("waktu4", waktu[4]);*/
        //throw new NotImplementedException();
        //data.pausedDate = pausedDate;
        //data.pausedDate = DateTime.Now;
        //data.foodPrep_seconds = foodPrep_seconds;

        //data.pausedDate.PausedDate = pausedDate;
        data.pausedDate.PausedDate = DateTime.Now;
        if(data.pulauUnlocked.Count != pulauUnlocked.Count)
        {
            for (int i = 0; i < pulauUnlocked.Count; i++)
            {
                data.pulauUnlocked.Add(pulauUnlocked[i]);
            }
        }
        if (data.pulauUnlockedName.Count != pulauUnlockedName.Count)
        {
            for (int i = 0; i < pulauUnlockedName.Count; i++)
            {
                data.pulauUnlockedName.Add(pulauUnlockedName[i]);
            }
        }
        if (data.foodPrep_seconds.Count != foodPrep_seconds.Count)
        {
            //data.foodPrep_seconds.Capacity = foodPrep_seconds.Count;
            for (int i = 0; i < foodPrep_seconds.Count; i++)
            {
                /*data.foodPrep_seconds.Add(foodPrep_seconds[i]);
                data.berapaKaliUpgrade.Add(berapaKaliUpgrade[i]);
                data.crystalSpeedUp.Add(crystalSpeedUp[i]);*/

                data.foodPrep_seconds.Add(foodPrep_seconds[i]);
            }
        }   
        if (data.berapaKaliUpgrade.Count != berapaKaliUpgrade.Count)
        {
            //data.berapaKaliUpgrade.Capacity = berapaKaliUpgrade.Count;
            for (int i = 0; i < berapaKaliUpgrade.Count; i++)
            {
                /*data.foodPrep_seconds.Add(foodPrep_seconds[i]);
                data.berapaKaliUpgrade.Add(berapaKaliUpgrade[i]);
                data.crystalSpeedUp.Add(crystalSpeedUp[i]);*/

                data.berapaKaliUpgrade.Add(berapaKaliUpgrade[i]);
            }
        }
        if (data.crystalSpeedUp.Count != crystalSpeedUp.Count)
        {
            //data.crystalSpeedUp.Capacity = crystalSpeedUp.Count;
            for (int i = 0; i < crystalSpeedUp.Count; i++)
            {
                /*data.foodPrep_seconds.Add(foodPrep_seconds[i]);
                data.berapaKaliUpgrade.Add(berapaKaliUpgrade[i]);
                data.crystalSpeedUp.Add(crystalSpeedUp[i]);*/

                data.crystalSpeedUp.Add(crystalSpeedUp[i]);
            }
        }
        if (data.waktu.Count != waktu.Count)
        {
            //data.waktu.Capacity = waktu.Count;
            for (int i = 0; i < waktu.Count; i++)
            {
                /*data.foodPrep_seconds.Add(foodPrep_seconds[i]);
                data.berapaKaliUpgrade.Add(berapaKaliUpgrade[i]);
                data.crystalSpeedUp.Add(crystalSpeedUp[i]);*/

                data.waktu.Add(waktu[i]);
            }
        }
        for(int i=0;i<pulauUnlockedName.Count;i++)
        {
            data.pulauUnlockedName[i] = pulauUnlockedName[i];
            data.pulauUnlocked[i] = pulauUnlocked[i];
        }
        for (int i=0;i<9;i++)
        {
            /*data.foodPrep_seconds.Add(foodPrep_seconds[i]);
            data.berapaKaliUpgrade.Add(berapaKaliUpgrade[i]);
            data.crystalSpeedUp.Add(crystalSpeedUp[i]);*/

            data.foodPrep_seconds[i] = foodPrep_seconds[i];
            data.berapaKaliUpgrade[i] = berapaKaliUpgrade[i];
            data.crystalSpeedUp[i] = crystalSpeedUp[i];
        }
        //data.berapaKaliUpgrade = berapaKaliUpgrade;
        //data.tempText = tempText;
        //data.waktu = waktu;
        for (int i = 0; i < waktu.Count; i++)
        {
            /*            if (data.waktu.ContainsKey(i.ToString()))
                        {
                            data.waktu.Remove(i.ToString());
                        }
                        data.waktu.Add(i.ToString(), waktu[i]);*/
            /*            if (data.waktu[i]==null)
                        {
                            data.waktu[i] = waktu[i];
                        }*/
            //data.waktu.Add(waktu[i]);
            data.waktu[i] = waktu[i];
            //Debug.LogError($"\n{data.waktu[i].ToString()}");
        }
        //data.upgrading = upgrading;
        //data.waktuStamp = waktuStamp;
    }

}
