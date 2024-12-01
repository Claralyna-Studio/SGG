using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_follow_cargo : MonoBehaviour
{
    GameObject cargo;
    // Start is called before the first frame update
    void Start()
    {
        cargo = transform.parent.GetChild(1).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if(cargo)
        {
            transform.position = new Vector3(cargo.transform.position.x, cargo.transform.position.y + 0.5f, cargo.transform.position.z);
        }
    }
}
