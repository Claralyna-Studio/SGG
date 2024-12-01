using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class MM : MonoBehaviour
{
    [SerializeField] private GameObject optionUI;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider sfx;
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
        Time.timeScale = 1;
        Screen.SetResolution(1920, 1080, true);
        optionUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("bgm") && PlayerPrefs.HasKey("sfx"))
        {
            PlayerPrefs.SetFloat("bgm", Mathf.Log10(bgm.value) * 20);
            PlayerPrefs.SetFloat("sfx", Mathf.Log10(sfx.value) * 20);
            mixer.SetFloat("bgm", PlayerPrefs.GetFloat("bgm"));
            mixer.SetFloat("sfx", PlayerPrefs.GetFloat("sfx"));
        }
        else
        {
            mixer.SetFloat("bgm", Mathf.Log10(bgm.value) * 20);
            mixer.SetFloat("sfx", Mathf.Log10(sfx.value) * 20);
        }
    }
    public void mulai()
    {

    }
    bool option = false;
    public void options()
    {
        option = !option;
        optionUI.SetActive(option);

    }
}
