using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class upgrades : MonoBehaviour
{
    GM gm;
    traySpawner tray_spawner;
    cargo_spawner cargo;
    [SerializeField] private List<GameObject> meja_upgrade;
    [Header("1=Bali, 2=Jawa")]
    [SerializeField] private List<float> foodPrep_seconds;
    [SerializeField] private List<int> berapaKaliUpgrade;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI crystalText;
    [SerializeField] private int moneyBoat;
    [SerializeField] private int crystalBoat;
    int money = 0;
    int crystal = 0;
    public List<Coroutine> upgrading;
    // Start is called before the first frame update
    void Start()
    {
        upgrading = new List<Coroutine>(2);
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
        if (waktu[idx] <= 0)
        {
            float currFoodPrep = foodPrep_seconds[idx];
            timerText.text = "Faster Food Preperations: \n" + currFoodPrep.ToString() + " seconds -> " + (currFoodPrep -= 1).ToString() + " seconds";
        }
        if (gm.money >= money && gm.crystal >= crystal && waktu[idx] <= 0)
        {
            buy_button.interactable = true;
        }
        else
        {
            buy_button.interactable = false;
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
    public void pindah()
    {
        //upgrading.Add(StartCoroutine(upgradeTimer(1)));
        if (curr_prov.gameObject.tag == "Bali")
        {
            idx = 0;
            money = 100;
            crystal = 100;
        }
        else if(curr_prov.gameObject.tag == "Jawa")
        {
            idx = 1;
            money = 100;
            crystal = 100;
        }
        moneyText.text = money.ToString("##,#");
        crystalText.text = crystal.ToString("##,#");
        transform.position = pos.position;
        clicked = true;
        if (waktu[idx] > 0)
        {
            timerText.text = "Calculating...";
        }
    }

    [SerializeField] private int idx = 0;
    public void upgradeBoat(TextMeshProUGUI text)
    {
        if(gm.money >= moneyBoat && gm.crystal >= crystalBoat)
        {
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
