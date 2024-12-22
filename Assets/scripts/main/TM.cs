using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TM : MonoBehaviour
{
    [SerializeField] private GameObject bg;
    [SerializeField] private Animator tutor;
    [SerializeField] private Animator tutorOrang;
    [SerializeField] private float typingSpeed;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] [TextArea] private string[] tutorial;
    //[SerializeField] private Transform highlight;
    //[SerializeField] private List<Button> buttons;
    [SerializeField] private List<Transform> curr_button;
    public static bool isTutoring = true;
    [SerializeField] private bool toUI = false;
    [SerializeField] private int idx = 0;
    // Start is called before the first frame update
    void Start()
    {
        //highlight = this.gameObject.transform;
        text.maxVisibleCharacters = 0;
        if(MM.tutor)
        {
            //showTutor();
            StartCoroutine(typing());
/*            foreach (Button button in buttons)
            {
                button.enabled = false;
            }*/
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        //StartCoroutine(typing());
    }
/*    public void highlightUI(string h)
    {
        //curr_button = GameObject.Find(h).GetComponent<Button>();
        highlight = GameObject.Find(h).transform;
        toWorldPoint = false;
    }
    public void hightlightWorld(string h)
    {
        //curr_button = GameObject.Find(h).GetComponent<Button>();
        highlight = GameObject.Find(h).transform;
        toWorldPoint = true;
    }*/

    public void worldToUI()
    {
        toUI = true;
    }
    public void worldNormal()
    {
        toUI = false;
    }
    [SerializeField] private Vector3 curPos;
    [SerializeField] private Vector3 buttonPos;
    // Update is called once per frame
    void Update()
    {
        curPos = bg.transform.position;
        buttonPos = curr_button[idx].position;
        if (toUI)
        {
            //bg.transform.position = Vector2.Lerp(bg.transform.position,Camera.main.WorldToScreenPoint(highlight.position), Time.deltaTime*10f);
            bg.transform.position = Vector2.Lerp(bg.transform.position,Camera.main.WorldToScreenPoint(curr_button[idx].position), Time.deltaTime*10f);
        }
        else
        {
            //bg.transform.position = Vector2.Lerp(bg.transform.position,highlight.position,Time.deltaTime*10f);
            bg.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(bg.GetComponent<RectTransform>().anchoredPosition, curr_button[idx].gameObject.GetComponent<RectTransform>().anchoredPosition,Time.deltaTime*10f);
            //bg.transform.position = curr_button[idx].position;
        }
        if(Input.GetMouseButtonDown(0) && idx < 3 && canClick)
        {
            next();
        }
        /*tutorOrang.SetBool("talking", canClick);*/
    }
    public static bool canClick = true;
    public void next()
    {
        if(canClick)
        {
            if (idx < tutorial.Length - 1)
            {
                canClick = false;
    /*            if(idx < tutorial.Length-1)
                {
                    idx++;
                }*/
                StartCoroutine(deleteTyping());
                tutor.SetTrigger("next");
/*                foreach (Button button in buttons)
                {
                    if (button != curr_button[idx] || curr_button[idx] == null)
                    {
                        button.enabled = false;
                    }
                    else
                    {
                        button.enabled = true;
                    }
                }  */              
            }
            else
            {

            }
        }
    }
    public void activeFalse()
    {
        //Destroy(gameObject);
        this.gameObject.SetActive(false);
    }
    public void showTutor()
    {
/*        foreach (GameObject go in tutor)
        {
            if(GM.day <= 3)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }*/
    }
    IEnumerator typing()
    {
        canClick = false;
        tutorOrang.SetBool("talking",true);
        text.text = tutorial[idx];
        text.maxVisibleCharacters++;
        yield return new WaitForSeconds(typingSpeed);
        if (text.maxVisibleCharacters < tutorial[idx].Length)
        {
            StartCoroutine(typing());
        }
        else
        {
            tutorOrang.SetBool("talking",false);
            canClick = true;
        }
    }
    IEnumerator deleteTyping()
    {
        tutorOrang.SetBool("talking", false);
        canClick = false;
        text.maxVisibleCharacters--;
        float temp = typingSpeed * 0.5f;
        yield return new WaitForSeconds(temp);
        if (text.maxVisibleCharacters > 0)
        {
            StartCoroutine(deleteTyping());
        }
        else
        {
            if (idx < tutorial.Length - 1)
            {
                idx++;
            }
            StartCoroutine(typing());
        }
    }

}
