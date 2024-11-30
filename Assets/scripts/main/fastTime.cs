using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fastTime : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void up()
    {
        Time.timeScale = 1;
        img.color = Color.white;
    }
    public void down()
    {
        Time.timeScale = speed;
        img.color = Color.gray;
    }
}
