using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class pathRenderer : MonoBehaviour
{
    LineRenderer lineRenderer;
    AIPath AIpath;
    private AIAgent cargo;
    // Start is called before the first frame update
    private void Awake()
    {
        cargo = FindObjectOfType<AIAgent>();
        AIpath = FindObjectOfType<AIPath>();
        lineRenderer = GetComponent<LineRenderer>();
        //StartCoroutine(path());
    }
    int a = 1;
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
        if (!AIpath.reachedEndOfPath)
        {
            lineRenderer.positionCount = a;
            lineRenderer.SetPosition(a - 1, cargo.transform.position);
            a++;
        }
        AstarPath.active.Scan();
    }
    IEnumerator path()
    {
        if(!AIpath.reachedEndOfPath)
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
    }
}
