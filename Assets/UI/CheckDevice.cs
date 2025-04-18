using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckDevice : MonoBehaviour
{

    public GameObject textBox;

    TMP_Text m_DeviceType;


    // Start is called before the first frame update
    void Start()
    {
        string type = Convert.ToString(SystemInfo.deviceType);

        m_DeviceType = textBox.GetComponent<TMP_Text>();

        m_DeviceType.text = type;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
