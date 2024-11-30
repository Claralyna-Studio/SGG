using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer food;
    public Sprite foods;
    [SerializeField] private SpriteRenderer bubble;
    public float masakTime = 10f;
    GM gm;
    private GameObject bali;
    private AIPath path;
    public float speed;
    public Transform target;
    public bool canMove = false;
    SpriteRenderer sp;
    pathRenderer trail;
    public tray Tray;
    // Start is called before the first frame update
    void Start()
    {
        Tray.isCooking = true;
        bubble.color = Color.red;
        //target = GameObject.Find("TARGET_CARGO").transform;
        gm = FindObjectOfType<GM>();
        sp = GetComponent<SpriteRenderer>();
        //sp.enabled = false;
        bali = GameObject.Find("sprites").transform.Find("Bali").gameObject;
        trail = transform.GetChild(0).GetComponent<pathRenderer>();
        path = GetComponent<AIPath>();
    }

    bool coroutineCalled = false;
    bool trailFuncCalled = false;
    bool resting = false;
    Vector3 scale = Vector3.one * 0.7f;
    // Update is called once per frame
    void Update()
    {
        transform.parent.localScale = Vector3.Lerp(transform.parent.localScale, scale, Time.deltaTime * 5f);
        path.canMove = canMove;
        if(canMove)
        {
            sp.enabled = true;
            if (!trailFuncCalled)
            {
                trailFuncCalled = true;
                trail.startTrail();
            }
            path.maxSpeed = speed;
            //path.speed = speed;
            if(path.reachedDestination && !coroutineCalled)
            {
                coroutineCalled = true;
                StartCoroutine(balik());
            }
            else if(!path.reachedDestination && !coroutineCalled)
            {
                path.destination = target.position;
            }
            else if(Vector3.Distance(path.destination,bali.transform.position) < 0.01f && path.reachedDestination)
            {
/*                resting = true;
                Invoke("doneRest",5f);*/
                canMove = false;
                //path.destination = target.position;
            }
        }
        else
        {
            coroutineCalled = false;
            //sp.enabled = false;
            if(trailFuncCalled)
            {
                scale = Vector3.zero;
                trailFuncCalled = false;
                int idx = gm.provs.IndexOf(target);
                gm.isCooking[idx] = false;
                trail.deleteTrail();
                Tray.doneCooking();
                //target.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                foreach (PolygonCollider2D col in target.GetComponent<clickable_prov>().colliders_to_be_unactived)
                {
                    col.enabled = true;
                }
            }
            //path.maxSpeed = speed*100;
            //path.destination = target.position;
            transform.position = bali.transform.position;
        }
    }
    public void startShipping(Transform targ)
    {
        if(!resting)
        {
            target.position = targ.position;
            canMove = true;
        }
    }
    void doneRest()
    {
        resting = false;
    }
    IEnumerator balik()
    {
        target.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(masakTime);
        bubble.color = Color.green;
        path.pickNextWaypointDist = 0.2f;
        target.GetComponent<SpriteRenderer>().enabled = false;
        path.destination = bali.transform.position;
    }
}
