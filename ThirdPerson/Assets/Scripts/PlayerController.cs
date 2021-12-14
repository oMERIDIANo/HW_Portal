using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField]
    float moveSpeed = 3.0f;

    [Header("References")]
    [SerializeField]
    Transform mainCamera;
    [SerializeField]
    BoxCollider swordCollider;

    Rigidbody rb;
    Animator anim;

    public GameObject portal1;
    public GameObject portal2;

    bool startedCombo = false;
    float timeSinceButtonPressed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        var camForward = mainCamera.forward;
        var camRight = mainCamera.right;

        camForward.y = 0;
        camForward.Normalize();
        camRight.y = 0;
        camRight.Normalize();

        var moveDirection = (camForward * v * moveSpeed) + (camRight * h * moveSpeed);

        transform.LookAt(transform.position + moveDirection);
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

        anim.SetFloat("moveSpeed", Mathf.Abs(moveDirection.magnitude));

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 50f, Color.red);

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50f, layerMask))
        {
            if(hit.transform.name == "Player")
            {
                Debug.Log("Hitting Player");
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            portal1.transform.position = hit.point;
            Quaternion rotation = Quaternion.LookRotation(-hit.normal, transform.up);
            portal1.transform.rotation = rotation;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            portal2.transform.position = hit.point;
            Quaternion rotation = Quaternion.LookRotation(-hit.normal, transform.up);
            portal2.transform.rotation = rotation;
        }

        if (Input.GetButtonDown("Jump") && !startedCombo)
        {
            anim.SetTrigger("swordCombo");
            startedCombo = true;
        }

        if(Input.GetButtonDown("Jump") && startedCombo)
        {
            timeSinceButtonPressed = 0;
        }

        timeSinceButtonPressed += Time.deltaTime;
    }

    public void PotentialComboEnd()
    {
        TurnOffSwordCollider();

        if (timeSinceButtonPressed < 0.5f)
            return;

        anim.SetTrigger("stopCombo");
        startedCombo = false;
        timeSinceButtonPressed = 0;
        
    }

    public void EndOfCombo()
    {
        startedCombo = false;
        timeSinceButtonPressed = 0;
        TurnOffSwordCollider();
    }

    public void TurnOnSwordCollider()
    {
        swordCollider.enabled = true;
    }

    public void TurnOffSwordCollider()
    {
        swordCollider.enabled = false;
    }
}
