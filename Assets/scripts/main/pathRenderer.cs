using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class pathRenderer : MonoBehaviour
{
    TrailRenderer lineRenderer;
    AIPath AIpath;
    private AIAgent cargo;
    public bool canTrail = false;
    // Start is called before the first frame update
    private void Awake()
    {
        cargo = FindObjectOfType<AIAgent>();
        AIpath = FindObjectOfType<AIPath>();
        //lineRenderer = GetComponent<LineRenderer>();
        lineRenderer = GetComponent<TrailRenderer>();
        //StartCoroutine(path());
    }
    //int a = 1;
    // Update is called once per frame
    void Update()
    {
        /*        if(lineRenderer && AIpath)
                {
                    lineRenderer.positionCount = AIpath.path.vectorPath.Count;
                    for (int i = 0; i < AIpath.path.vectorPath.Count; i++)
                    {
                        lineRenderer.SetPosition(i, AIpath.path.vectorPath[i]);
                    }
                    //lineRenderer.Simplify(1);
                }*/

        //line renderer
/*        if (!AIpath.reachedEndOfPath && canTrail)
        {
            lineRenderer.positionCount = a;
            lineRenderer.SetPosition(a - 1, cargo.transform.position);
            a++;
        }*/
        //AstarPath.active.Scan();
    }
    public void startTrail()
    {
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        //a = 1;
        canTrail = true;
        //StopAllCoroutines();
    }
/*    IEnumerator path()
    {
        if(!AIpath.reachedEndOfPath && canTrail)
        {
            lineRenderer.positionCount = a;
            lineRenderer.SetPosition(a-1, cargo.transform.position);
            a++;
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(path());
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }
    }*/
    public void deleteTrail()
    {
        canTrail = false;
        StartCoroutine(deleteTrail2());
    }
    public IEnumerator deleteTrail2()
    {
        //canTrail = false;
        /*        a = 1;
                lineRenderer.positionCount = 0;*/
        if (lineRenderer.startWidth > 0)
        {
            lineRenderer.startWidth -= 0.05f;
        }
        else if (lineRenderer.endWidth > 0) 
        {
            lineRenderer.endWidth -= 0.05f;
        }
        else if(transform.parent.localScale != Vector3.zero)
        {
            transform.parent.localScale = Vector3.MoveTowards(transform.parent.localScale, Vector3.zero, Time.deltaTime * 5f);
        }
        yield return new WaitForSeconds(0.001f);
        if (lineRenderer.startWidth > 0 || lineRenderer.endWidth > 0 || transform.parent.localScale != Vector3.zero)
        {
            StartCoroutine (deleteTrail2());
        }
        else
        {
            lineRenderer.time = 0.01f;
            Destroy(transform.parent.gameObject.transform.parent.transform.parent.gameObject);
            //lineRenderer.positionCount = 0;
        }
    }

}
