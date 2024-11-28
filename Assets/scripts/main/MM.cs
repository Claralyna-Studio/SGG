using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM : MonoBehaviour
{
    [SerializeField] private GameObject optionUI;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        optionUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
