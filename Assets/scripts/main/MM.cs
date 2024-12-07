using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MM : MonoBehaviour
{
    [SerializeField] private Animator mainCanvas;
    [SerializeField] private GameObject exitSureUI;
    [SerializeField] private GameObject optionUI;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider sfx;
    static bool intro = true;
    // Start is called before the first frame update
    void Start()
    {
        if(!intro)
        {
            mainCanvas.Play("in");
        }
        GameObject.Find("LOADING").GetComponent<Animator>().Play("out");
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
        Screen.SetResolution(1920, 1080, true);
        optionUI.SetActive(false);
        exitSureUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(intro && Input.anyKeyDown)
        {
            intro = false;
            mainCanvas.SetBool("intro", false);
        }
        mainCanvas.SetBool("intro", intro);
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
    }
    public void mulai()
    {
        GameObject.Find("LOADING").GetComponent<loading>().gameScene = "gameplay";
        GameObject.Find("LOADING").GetComponent<Animator>().Play("in");
        //SceneManager.LoadScene(1);
    }
    bool option = false;
    public void options()
    {
        option = !option;
        optionUI.SetActive(option);
    }
    public void exit()
    {
        //Application.Quit();
        if(!exitSureUI.activeSelf)
        {
            exitSureUI.SetActive(true);
        }
    }
    public void exitSure(bool yesno)
    {
        if(yesno)
        {
            Application.Quit();
        }
        else
        {
            exitSureUI.SetActive(false);
        }
    }
}
