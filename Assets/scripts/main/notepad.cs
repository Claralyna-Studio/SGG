using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notepad : MonoBehaviour
{
    public tray order;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void exitNotePad()
    {
        GetComponent<Animator>().SetBool("in", false);
        order.exitNotePad();
    }
}
