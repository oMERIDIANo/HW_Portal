using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    float m_FieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        m_FieldOfView = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(m_FieldOfView);

        if (Input.GetButton("Fire3"))
        {
            m_FieldOfView = 100f;
        }
        if (Input.GetButtonUp("Fire3"))
        {
            m_FieldOfView = Camera.main.fieldOfView;
        }
    }
}
