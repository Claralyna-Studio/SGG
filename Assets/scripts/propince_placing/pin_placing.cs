using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pin_placing : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    propinM gm;
    pin_animation anim;
    [SerializeField] private GameObject col;
    private void Awake()
    {
        anim = GetComponent<pin_animation>();
        GetComponent<Animator>().Play("selected11", 0, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<propinM>();
    }
    // Update is called once per frame
    void Update()
    {
        //col.transform.position = Camera.main.ScreenToWorldPoint(transform.position);
    }
    //bool isclicked = false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!gm.fail)
        {
            //isclicked = true;
            //GetComponent<Animator>().SetBool("dragging",true);
            anim.par.transform.GetChild(1).GetComponent<Animator>().SetBool("dragging",true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!gm.fail)
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            anim.par.transform.GetChild(1).GetComponent<Animator>().SetBool("dragging", false);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!gm.fail)
        {
            //isclicked = false;
            //GetComponent<Animator>().SetBool("dragging", false);
            anim.par.transform.GetChild(1).GetComponent<Animator>().SetBool("dragging", false);
        }
    }
}
