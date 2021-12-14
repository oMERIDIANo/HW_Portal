using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire3"))
        {
            cinemachineFreeLook.m_Lens.FieldOfView = 30;
        }
        if(Input.GetButtonUp("Fire3"))
        {
            cinemachineFreeLook.m_Lens.FieldOfView = 40;
        }
    }
}
