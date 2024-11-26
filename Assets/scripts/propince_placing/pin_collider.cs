using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class pin_collider : MonoBehaviour
{
    public bool select = false;
    public string prov;
    public Color color;
    public string province;
    SpriteRenderer sprite;
    public SpriteRenderer check;
    public TextMeshPro checkText;
    // Start is called before the first frame update
    void Start()
    {
        prov = "";
        check = transform.parent.Find("check").GetComponent<SpriteRenderer>();
        checkText = check.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        checkText.text = "";
        sprite = transform.parent.Find("pin").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.color = color;
    }
}
