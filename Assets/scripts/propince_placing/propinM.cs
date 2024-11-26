using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class propinM : MonoBehaviour
{
    [SerializeField] private List<string> all_provinces;
    [SerializeField] private List<string> curr_province;
    [SerializeField] private List<TextMeshProUGUI> texts_province;
    [SerializeField] private List<Color> colors;
    [SerializeField] private List<pin_collider> pins;
    province[] provs;
    // Start is called before the first frame update
    void Start()
    {
        provs = FindObjectsOfType<province>();
        //curr_province = provinces[Random.Range(0, provinces.Count)];
        for (int i = 0; i < 6; i++)
        {
            int idx = Random.Range(0, all_provinces.Count);
            curr_province.Add(all_provinces[idx]);
            all_provinces.RemoveAt(idx);
            texts_province[i].text = curr_province[i];
            texts_province[i].color = colors[idx];

            pins[i].color = colors[idx];
            pins[i].province = curr_province[i];
            colors.Remove(pins[i].color);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void locked(Button lokk)
    {
        lokk.interactable = false;
        foreach(pin_collider pin in pins)
        {
            if (pin.province.ToLower() == pin.prov.ToLower())
            {
                Debug.Log("Betul");
                pin.checkText.text = "Correct!";
                pin.check.color = Color.green;
            }
            else
            {
                Debug.Log("Salah");
                pin.checkText.text = "Wrong!";
                pin.check.color = Color.red;
            }
            pin.check.GetComponent<Animator>().SetTrigger("check");
        }
    }
}
