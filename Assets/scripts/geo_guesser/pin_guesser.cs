using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pin_guesser : MonoBehaviour
{
    geoM gm;
    private GameObject pin;
    public bool selected = false;
    private Button button;
    private Image img;
    Color curr_colr;
    [SerializeField] private List<pin_guesser> pinLain;
    ColorBlock but_clor;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<geoM>();
        if(TryGetComponent<SpriteRenderer>(out SpriteRenderer s))
        {
            s.enabled = false;
        }
/*        button = GetComponent<Button>();
        img = GetComponent<Image>();*/
        button = GameObject.Find("MAP").transform.Find(this.gameObject.name).GetComponent<Button>();
        img = GameObject.Find("MAP").transform.Find(this.gameObject.name).GetComponent<Image>();

        img.color = curr_colr;
        //but_clor = button.colors;
        //but_clor.normalColor = curr_colr;
        //button.colors = but_clor;

        pin = GameObject.Find("PIN");
        selected = false;
        pinLain = new List<pin_guesser>(FindObjectsOfType<pin_guesser>());
        pinLain.Remove(this);
    }
    Vector2 mousePos;
    // Update is called once per frame
    void Update()
    {
        transform.position = button.gameObject.transform.position;
        transform.localScale = button.gameObject.transform.lossyScale * 100f;
/*        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
        }*/
        if(selected)
        {
            //curr_colr = Color.green;
        }
        else
        {
            curr_colr = Color.red;
        }
        img.color = curr_colr;
        //but_clor.normalColor = curr_colr;
        //button.colors = but_clor;
    }
    static bool isClicked = false;
    private void OnMouseDown()
    {
        if (!isClicked)
        {
            mousePos = Input.mousePosition;
            gm.pin = mousePos;
            isClicked = true;
            clicked();
        }
    }
    public void clicked()
    {
        gm.pinned();
        Debug.Log("clicked!");
        pin.GetComponent<Animator>().Play("select",0,0);
        pin.transform.position = mousePos;

        foreach (pin_guesser pin in pinLain)
        {
            pin.selected = false;
        }
        selected = true;
    }
}
