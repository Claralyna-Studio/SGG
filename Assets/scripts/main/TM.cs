using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TM : MonoBehaviour
{
    [SerializeField] private Animator tutor;
    [SerializeField] private Animator tutorOrang;
    [SerializeField] private float typingSpeed;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] [TextArea] private string[] tutorial;
    int idx = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(MM.tutor)
        {
            showTutor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        tutorOrang.SetBool("talking", canClick);
    }
    bool canClick = true;
    public void next()
    {
        if(canClick)
        { 
            canClick = false;
            StartCoroutine(deleteTyping());
            if(idx < tutorial.Length-1)
            {
                idx++;
            }
            tutor.SetTrigger("next");
        }
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
        //tutorOrang.SetBool("talking",true);
        text.text = tutorial[idx];
        text.maxVisibleCharacters++;
        yield return new WaitForSeconds(typingSpeed);
        if (text.maxVisibleCharacters < tutorial[idx].Length)
        {
            StartCoroutine(deleteTyping());
        }
        else
        {
            //tutorOrang.SetBool("talking",false);
            canClick = true;
        }
    }
    IEnumerator deleteTyping()
    {

        text.maxVisibleCharacters--;
        yield return new WaitForSeconds(typingSpeed * 0.3f);
        if (text.maxVisibleCharacters > 0)
        {
            StartCoroutine(deleteTyping());
        }
        else
        {
            StartCoroutine(typing());
        }
    }

}
