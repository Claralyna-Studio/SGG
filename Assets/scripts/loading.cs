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

    // Update is called once per frame
    void Update()
    {
        
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
