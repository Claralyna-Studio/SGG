using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TM : MonoBehaviour
{
    [SerializeField] Canvas canvasBlkg;
    [SerializeField] private GameObject bg;
    [SerializeField] private Animator tutor;
    [SerializeField] private Animator tutorOrang;
    [SerializeField] private float typingSpeed;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] [TextArea] private string[] tutorial;
    //[SerializeField] private Transform highlight;
    //[SerializeField] private List<Button> buttons;
    public List<Transform> curr_button;
    public static bool isTutoring = true;
    [SerializeField] private bool toUI = false;
    public int idx = 0;
    // Start is called before the first frame update
    void Start()
    {
        //highlight = this.gameObject.transform;
        text.maxVisibleCharacters = 0;
        if(MM.tutor)
        {
            isTutoring = true;
            //showTutor();
            anchorMaxAwal = bg.GetComponent<RectTransform>().anchorMax;
            anchorMinAwal = bg.GetComponent<RectTransform>().anchorMin;
            pivotAwal = bg.GetComponent<RectTransform>().pivot;
            StartCoroutine(typing());
/*            foreach (Button button in buttons)
            {
                button.enabled = false;
            }*/
        }
        else
        {
            isTutoring = false;
            GetComponent<Animator>().Play("stayOut");
            //this.gameObject.SetActive(false);
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
    Vector2 anchorMaxAwal;
    Vector2 anchorMinAwal;
    Vector2 pivotAwal;
    // Update is called once per frame
    void Update()
    {
        curPos = bg.GetComponent<RectTransform>().position;
        buttonPos = curr_button[idx].GetComponent<RectTransform>().position;

        //chatgt
        RectTransform targetRect = curr_button[idx].GetComponent<RectTransform>();
        RectTransform bgRect = bg.GetComponent<RectTransform>();
        //toUI = targetRect.GetComponentInParent<Canvas>() == bgRect.GetComponentInParent<Canvas>();
        if (toUI)
        {
            //bg.transform.position = Vector2.Lerp(bg.transform.position,Camera.main.WorldToScreenPoint(highlight.position), Time.deltaTime*10f);
            //bg.transform.position = Vector2.Lerp(bg.transform.position,Camera.main.WorldToScreenPoint(curr_button[idx].position), Time.deltaTime*10f);
            //bg.GetComponent<RectTransform>().position = Vector2.Lerp(bg.GetComponent<RectTransform>().position, curr_button[idx].GetComponent<RectTransform>().position,Time.deltaTime*10f);

            //chatgpt lg :')
/*            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(bgRect.GetComponentInParent<Canvas>().worldCamera, targetRect.position);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetRect.parent as RectTransform,
                screenPoint,
                null,
                out Vector2 localPoint
            );
            bgRect.localPosition = targetRect.localPosition;

            bgRect.anchorMin = targetRect.anchorMin;
            bgRect.anchorMax = targetRect.anchorMax;
            bgRect.pivot = targetRect.pivot;*/
            bg.transform.position = Vector2.Lerp(bg.transform.position,buttonPos,Time.deltaTime*20f);
        }
        else
        {

            //bg.transform.position = Vector2.Lerp(bg.transform.position,highlight.position,Time.deltaTime*10f);

            /*            bg.GetComponent<RectTransform>().pivot = curr_button[idx].GetComponent<RectTransform>().pivot;
                        bg.GetComponent<RectTransform>().anchorMax = curr_button[idx].GetComponent<RectTransform>().anchorMax;
                        bg.GetComponent<RectTransform>().anchorMin = curr_button[idx].GetComponent<RectTransform>().anchorMin;
                        bg.GetComponent<RectTransform>().position = Vector2.Lerp(bg.GetComponent<RectTransform>().position, curr_button[idx].GetComponent<RectTransform>().position,Time.deltaTime*10f);
                        */
            //bg.GetComponent<RectTransform>().rect = curr_button[idx].GetComponent<RectTransform>().rect;
            //bg.transform.position = curr_button[idx].position;

            //chatgpt help
            /*            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, 
                            RectTransformUtility.WorldToScreenPoint(canvasBlkg.worldCamera, curr_button[idx].position),
                            null,
                            out Vector2 localPoint);
                        bg.transform.localPosition = localPoint;
                        bg.GetComponent<RectTransform>().anchorMin = curr_button[idx].GetComponent<RectTransform>().anchorMin;
                        bg.GetComponent<RectTransform>().anchorMax = curr_button[idx].GetComponent<RectTransform>().anchorMax;
                        bg.GetComponent<RectTransform>().pivot = curr_button[idx].GetComponent<RectTransform>().pivot;*/

            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(canvasBlkg.worldCamera, targetRect.position);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform as RectTransform,
                screenPoint,
                null,
                out Vector2 localPoint
            );


            bgRect.anchorMin = targetRect.anchorMin;
            bgRect.anchorMax = targetRect.anchorMax;
            bgRect.pivot = targetRect.pivot;
            bgRect.localPosition = Vector3.Lerp(bgRect.localPosition,localPoint,Time.deltaTime*20f);

        }
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            if(idx < 3 || idx == 17)
            {
                next();
            }
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
                isTutoring = false;
                canClick = true;
                tutor.SetTrigger("next");
            }
        }
    }
    public void activeFalse()
    {
        //Destroy(gameObject);
        this.gameObject.SetActive(false);
    }
/*    public void showTutor()
    {
*//*        foreach (GameObject go in tutor)
        {
            if(GM.day <= 3)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }*//*
    }*/
    int count = 4;
    IEnumerator typing()
    {
        count--;
        if(count <= 0)
        {
            GameObject.Find("sfx_dialog").GetComponent<AudioSource>().Stop();
            GameObject.Find("sfx_dialog").GetComponent<AudioSource>().Play();
            count = 4;
        }
        canClick = false;
        tutorOrang.SetBool("talking",true);
        text.text = tutorial[idx];
        text.maxVisibleCharacters++;
        //Debug.Log(typingSpeed);
        yield return new WaitForSeconds(0.01f);
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
        //text.maxVisibleCharacters--;
        text.maxVisibleCharacters = 0;
        //float temp = typingSpeed * 0.5f;
        yield return new WaitForSeconds(0.001f);
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
