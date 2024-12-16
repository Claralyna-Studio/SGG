using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sprite_to_UI_pos : MonoBehaviour
{
    GameObject pos;
    RectTransform rectTransform;
    [SerializeField] private float kurangX;
    [SerializeField] private float kurangY;
    [SerializeField] private bool provInv = true;
    // Start is called before the first frame update
    void Awake()
    {
        pos = GameObject.Find("MAP").transform.Find(this.gameObject.name).gameObject;  
        if(pos.gameObject.GetComponent<Image>().enabled && provInv)
        {
            pos.gameObject.GetComponent<Image>().enabled = false;
        }
        rectTransform = pos.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pos.transform.position;
        //transform.localScale = rectTransform.lossyScale * 50f;
        //Debug.Log(rectTransform.rect.size);
        //transform.localScale = new Vector2(rectTransform.rect.size.x - kurangX, rectTransform.rect.size.y - kurangY);
        transform.localScale = new Vector2(rectTransform.rect.size.x / kurangX, rectTransform.rect.size.y / kurangY);

/*        float pixelCountForOneUnit = Screen.height * 0.5f / Camera.main.orthographicSize;
        float scaleX = rectTransform.width / pixelCountForOneUnit;
        float scaleY = rectTransform.height / pixelCountForOneUnit;

        Vector3 scale = new Vector3(scaleX, scaleY, 1.0f);
        transform.localScale = scale;*/
    }
}
