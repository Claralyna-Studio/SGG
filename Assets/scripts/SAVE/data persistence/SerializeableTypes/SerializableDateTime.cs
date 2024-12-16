using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDateTime : ISerializationCallbackReceiver
{
    [SerializeField] 
    private string serializedTime;

    //from YT
    private DateTime pausedDate;
    public DateTime PausedDate
    {
        get => pausedDate;
        set => pausedDate = value;
    }
    public SerializableDateTime()
    {
        //Debug.LogWarning(pausedDate);
        serializedTime = null;
        pausedDate = DateTime.Now;
        //pausedDate = default;
    }
    public void OnAfterDeserialize()
    {
        pausedDate = DateTime.Parse(serializedTime).ToLocalTime();
        //PausedDate = pausedDate;
    }

    public void OnBeforeSerialize()
    {
        /*        if (pausedDate == null)
                {
                    pausedDate = DateTime.Now;
                }*/
        serializedTime = pausedDate.ToUniversalTime().ToString("o");
    }

    //from chatGPT - unused
    /*
        public DateTime PausedDate { get; set; }

        public void OnAfterDeserialize()
        {
            if (!string.IsNullOrEmpty(serializedTime))
                PausedDate = DateTime.Parse(serializedTime).ToLocalTime();
        }

        public void OnBeforeSerialize()
        {
            serializedTime = PausedDate.ToUniversalTime().ToString("o");
        }*/
}
