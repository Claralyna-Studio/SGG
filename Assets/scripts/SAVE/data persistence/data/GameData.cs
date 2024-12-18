using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using static UnityEditor.Progress;

[System.Serializable]
public class GameData
{
    //gm
    public long day;
    public long money;
    public long crystal;

    //upgrades
    //public DateTime pausedDate;
    public SerializableDateTime pausedDate;
    public SerializableList<string> pulauUnlockedName;
    public SerializableList<bool> pulauUnlocked;
    public SerializableList<float> foodPrep_seconds;
    public SerializableList<int> berapaKaliUpgrade;
    public SerializableList<int> crystalSpeedUp;
    //public List<Coroutine> upgrading;
    public SerializableList<int> waktu;
    //public SerializableDictionary<string, int> waktu;
    //public List<TextMeshProUGUI> tempText;
    //public List<string> waktuStamp;
    //public string waktuStamp;

    //tray spawner
    public int trayMax = 2;
    public SerializableList<string> canProv;
    public SerializableList<int> canProv_maxFood;

    //cargo spawner
    public int maxBoats;
    public float boatSpeed;

/*    [Serializable]
    public struct orders
    {
        public string province;
        public List<Sprite> food;
        public List<long> prices;
        public List<float> cookTime;
        public List<GameObject> restrictions;
    }
    public orders[] order;*/


    //public SerializableDictionary<string, bool> items, pajang;

    //the values defined in this constructor will be the default value
    //the game starts when there's no data to load
    public GameData()
    {
        //Debug.LogError("DATA NEW");
        //items = new SerializableDictionary<string, bool>();
        //pajang = new SerializableDictionary<string, bool>();

        /*        waktuStamp = new List<string> ();
                waktuStamp.Add("");
                waktuStamp.Add("");
                waktuStamp.Add("");
                waktuStamp.Add("");
                waktuStamp.Add("");
                waktuStamp.Add("");*/
        //pausedDate = DateTime.Now;

        pausedDate = new SerializableDateTime();
        //pausedDate.PausedDate = DateTime.Now;

        waktu = new SerializableList<int>();

        pulauUnlockedName = new SerializableList<string>();
        pulauUnlocked = new SerializableList<bool>();
        foodPrep_seconds = new SerializableList<float>();
        berapaKaliUpgrade = new SerializableList<int>();
        crystalSpeedUp = new SerializableList<int>();
        canProv = new SerializableList<string>();
        canProv_maxFood = new SerializableList<int>();
/*        tempText = new List<TextMeshProUGUI>();
        tempText.Add(null);
        tempText.Add(null);
        tempText.Add(null);
        tempText.Add(null);
        tempText.Add(null);
        tempText.Add(null);*/
        //waktuStamp = "";
        //waktu = new SerializableDictionary<string, int>();
        /*        upgrading = new List<Coroutine>();
                upgrading.Add(null);
                upgrading.Add(null);
                upgrading.Add(null);
                upgrading.Add(null);
                upgrading.Add(null);
                upgrading.Add(null);*/

        trayMax = 2;
        day = 1;
        money = 0;
        crystal = 0;
        maxBoats = 2;
        boatSpeed = 0.3f;
    }
}
