using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class tray : MonoBehaviour
{
    public Sprite food;
    [SerializeField] private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.sprite = food;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
