using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM : MonoBehaviour
{
    [SerializeField] private GameObject[] tutor;
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

    }
    public void showTutor()
    {
        foreach (GameObject go in tutor)
        {
            if(GM.day <= 3)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }
}
