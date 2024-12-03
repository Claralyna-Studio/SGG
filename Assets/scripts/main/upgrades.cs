using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class upgrades : MonoBehaviour
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
    [SerializeField] private int moneyBoat;
    [SerializeField] private int crystalBoat;
    [SerializeField] private int money = 0;
    [SerializeField] private int crystal = 0;
    [SerializeField] private GameObject upgradeBoatButton;
    public List<Coroutine> upgrading;
    // Start is called before the first frame update
    void Start()
    {
        upgrading = new List<Coroutine>(2);
        upgrading.Add(null);
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
    }

    // Update is called once per frame
    void Update()
    {
        if (cargo.masakTime[idx] <= 0)
        {
            buy_button.gameObject.SetActive(false);
            timerText.text = "Maximum food preperations reached!";
        }
        else
        {
            if (waktu[idx] <= 0)
            {
                float currFoodPrep = foodPrep_seconds[idx];
                timerText.text = "Faster Food Preperations: \n" + currFoodPrep.ToString() + " seconds -> " + (currFoodPrep -= 1).ToString() + " seconds";
            }
            buy_button.gameObject.SetActive(true);
        }
        moneyText.text = money.ToString("##,#");
        crystalText.text = crystal.ToString("##,#");
        non_active[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = crystalSpeedUp[idx].ToString();
        upgradeBoatButton.GetComponent<Animator>().SetBool("in",gm.closed);
        if(!isUpgradingBoat && gm.money >= moneyBoat && gm.crystal >= crystalBoat)
        {
            upgradeBoatButton.GetComponent<Image>().enabled = true;
            upgradeBoatButton.transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        else
        {
            upgradeBoatButton.GetComponent <Image>().enabled = false;
            upgradeBoatButton.transform.GetChild(1).GetComponent<Image>().color = Color.red;
        }

        if (gm.money >= money && gm.crystal >= crystal && waktu[idx] <= 0)
        {
            //buy_button.interactable = true;
            buy_button.gameObject.GetComponent<Image>().color = Color.white;
        }
        else
        {
            //buy_button.interactable = false;
            buy_button.gameObject.GetComponent<Image>().color = Color.red;
        }

        if(clicked) GetComponent<Animator>().SetBool("in", gm.closed);
        if(!gm.startDay && gm.closed)
        {
            if(tray_spawner.trayMax < 6)
            {
                for(int i = tray_spawner.trayMax; i < meja_upgrade.Count;i++)
                {
                    //meja_upgrade[i].SetActive(true);
                    meja_upgrade[i].GetComponent<Animator>().SetBool("in",true);
                    //Debug.Log("A");
                }
            }
        }
        else
        {
            if (tray_spawner.trayMax < 6)
            {
                for (int i = tray_spawner.trayMax; i < meja_upgrade.Count; i++)
                {
                    //meja_upgrade[i].SetActive(false);
                    meja_upgrade[i].GetComponent<Animator>().SetBool("in", false);
                    //Debug.Log("B");
                }
                for (int i = 2; i < tray_spawner.trayMax; i++)
                {
                    meja_upgrade[i].GetComponent<Animator>().SetBool("in", true);
                }
            }
        }
    }
    public Transform pos;
    public clickable_prov curr_prov;
    public bool clicked = false;
    public List<int> waktu;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button buy_button;
    [Header("0 = moneyText, 1 = textSPeedUp, 2 = crystalButton")]
    [SerializeField] private GameObject[] non_active;
    //[SerializeField] private Button crystalButton;
    public void pindah()
    {
        //upgrading.Add(StartCoroutine(upgradeTimer(1)));
        if (curr_prov.gameObject.name == "Bali")
        {
            idx = 0;
            money = 100;
            crystal = 100;
        }
        else if(curr_prov.gameObject.name == "Nusa Tenggara Timur")
        {
            idx = 1;
            money = 100;
            crystal = 100;
        }
        else if (curr_prov.gameObject.name == "Nusa Tenggara Barat")
        {
            idx = 2;
            money = 100;
            crystal = 100;
        }

        else if(curr_prov.gameObject.name == "Banten")
        {
            idx = 3;
            money = 100;
            crystal = 100;
        }
        else if (curr_prov.gameObject.name == "DKI Jakarta")
        {
            idx = 4;
            money = 100;
            crystal = 100;
        }
        else if (curr_prov.gameObject.name == "Jawa Barat")
        {
            idx = 5;
            money = 100;
            crystal = 100;
        }
        else if (curr_prov.gameObject.name == "Jawa Tengah")
        {
            idx = 6;
            money = 100;
            crystal = 100;
        }
        else if (curr_prov.gameObject.name == "D.I. Yogyakarta")
        {
            idx = 7;
            money = 100;
            crystal = 100;
        }
        else if (curr_prov.gameObject.name == "Jawa Timur")
        {
            idx = 8;
            money = 100;
            crystal = 100;
        }
        transform.position = pos.position;
        clicked = true;
        if (waktu[idx] > 0)
        {
            timerText.text = "Calculating...";
        }
    }
    public void finishUpg()
    {
        if(gm.crystal >= crystalSpeedUp[idx])
        {
            timerText.text = "Finishing Up...";
            non_active[0].SetActive(true);
            non_active[1].SetActive(true);
            non_active[2].SetActive(false);
            non_active[3].SetActive(false);
            gm.crystal -= crystalSpeedUp[idx];
            crystalSpeedUp[idx] += 3;
            isUpgrading = false;
            waktu[idx] = 0;
        }
    }
    [SerializeField] private int idx = 0;
    public void upgradeBoat(TextMeshProUGUI text)
    {
        if(!isUpgradingBoat && gm.money >= moneyBoat && gm.crystal >= crystalBoat)
        {
            isUpgradingBoat = true;
            gm.money -= moneyBoat;
            gm.crystal -= crystalBoat;
            waktu.Add(30);
            int index = waktu.Count-1;
            upgrading.Add(null);
            upgrading[index] = StartCoroutine(upgradeTimerBoat(waktu.Count-1, text));
        }
    }
    public void buy()
    {
        if(gm.money >= money && gm.crystal >= crystal)
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
                waktu[idx] = 30;
            }
            else if(berapaKaliUpgrade[idx] == 1)
            {
                waktu[idx] = 60;
            }
            else if (berapaKaliUpgrade[idx] == 2)
            {
                waktu[idx] = 60;
            }
            else if (berapaKaliUpgrade[idx] == 3)
            {
                waktu[idx] = 300;
            }
            else
            {
                waktu[idx] = 600;
            }
            gm.money -= money;
            gm.crystal -= crystal;
            Debug.Log(upgrading.Count);
            upgrading[idx] = StartCoroutine(upgradeTimer(idx));
        }
    }
    public IEnumerator upgradeTimer(int idx)
    {
        if(idx == this.idx)
        {
            int menit = Mathf.FloorToInt(waktu[idx] / 60);
            int detik = Mathf.FloorToInt(waktu[idx] % 60);
            timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
        }
        yield return new WaitForSeconds(1);
        waktu[idx]--;
        if (idx == this.idx)
        {
            int menit = Mathf.FloorToInt(waktu[idx] / 60);
            int detik = Mathf.FloorToInt(waktu[idx] % 60);
            timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
        }
        if (waktu[idx] > 0) StartCoroutine(upgradeTimer(idx));
        else
        {
            non_active[0].SetActive(true);
            non_active[1].SetActive(true);
            non_active[2].SetActive(false);
            non_active[3].SetActive(false);
            isUpgrading = false;
            cargo.masakTime[idx]--;
            foodPrep_seconds[idx] -= 1;
            berapaKaliUpgrade[idx]++;
            float currFoodPrep = foodPrep_seconds[idx];
            timerText.text = "Faster Food Preperations: \n" + currFoodPrep.ToString() + " seconds -> " + (currFoodPrep -= 1).ToString() + " seconds";
        }
    }
    public IEnumerator upgradeTimerBoat(int idx, TextMeshProUGUI text)
    {
        if (idx == this.idx)
        {
            int menit = Mathf.FloorToInt(waktu[idx] / 60);
            int detik = Mathf.FloorToInt(waktu[idx] % 60);
            text.text = string.Format("{0:00}:{1:00}", menit, detik);
        }
        yield return new WaitForSeconds(1);
        waktu[idx]--;
        if (idx == this.idx)
        {
            int menit = Mathf.FloorToInt(waktu[idx] / 60);
            int detik = Mathf.FloorToInt(waktu[idx] % 60);
            text.text = string.Format("{0:00}:{1:00}", menit, detik);
        }
        if (waktu[idx] > 0) StartCoroutine(upgradeTimerBoat(idx,text));
        else
        {
            isUpgradingBoat = false;
            moneyBoat += 5;
            crystalBoat += 5;
            cargo.speed += 0.1f;
            text.text = "Boat Speed: " + cargo.speed;
        }
    }
    public IEnumerator upgradeTimerSlot(int idx, TextMeshProUGUI text)
    {
        int menit = Mathf.FloorToInt(waktu[idx] / 60);
        int detik = Mathf.FloorToInt(waktu[idx] % 60);
        text.text = string.Format("{0:00}:{1:00}", menit, detik);
        yield return new WaitForSeconds(1);
        waktu[idx]--;
        menit = Mathf.FloorToInt(waktu[idx] / 60);
        detik = Mathf.FloorToInt(waktu[idx] % 60);
        text.text = string.Format("{0:00}:{1:00}", menit, detik);
        if (waktu[idx] > 0) StartCoroutine(upgradeTimerSlot(idx, text));
        else
        {
            tray_spawner.doneUpgrade();
        }
    }
}
