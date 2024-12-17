using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
    public string gameScene;
    public static loading instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    bool volumeDown = false;
    // Update is called once per frame
    void Update()
    {
        if (!volumeDown && GameObject.Find("bgm").GetComponent<AudioSource>().volume < 1)
        {
            GameObject.Find("bgm").GetComponent<AudioSource>().volume+=0.002f;
        }
        else if(volumeDown && GameObject.Find("bgm").GetComponent<AudioSource>().volume > 0)
        {
            GameObject.Find("bgm").GetComponent<AudioSource>().volume-=0.002f;
        }
    }
    public void volume_down()
    {
        volumeDown = true;
    }
    public void volume_up()
    {
        volumeDown = false;
    }
    public void changeScene()
    {
        /*        if(SceneManager.GetActiveScene().name == "main_menu" || gameScene == "main_menu")
                {
                    SceneManager.LoadScene(gameScene);
                }
                else if(gameScene != "gameplay")
                {
                    SceneManager.LoadScene(gameScene,LoadSceneMode.Additive);
                }
                else if(gameScene == "gameplay")
                {
                    SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
                }*/
        SceneManager.LoadScene(gameScene);
    }
}
