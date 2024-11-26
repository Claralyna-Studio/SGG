using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin_animation : MonoBehaviour
{
    public GameObject par;
    [SerializeField] private bool canMove = true;
    //[SerializeField] private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(this.transform.position);
            par.transform.position = new Vector3(pos.x, pos.y, 100);
        }

    }
    public void clicked()
    {
        //GameObject clone = Instantiate(par, parent.transform);
        //clone.transform.SetParent(parent.transform, false);


        par.GetComponent<ParticleSystem>().Emit(50);
    }
}
