using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private Rigidbody rb3d;
    public float MoveSpeed = 10f;
    public bool canMove = true;

    private FixedJoystick joystick;
    public Animator anim;

    private void Awake()
    {
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb3d = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb3d.velocity = new Vector3(joystick.Horizontal * MoveSpeed, 0f, joystick.Vertical * MoveSpeed);

            if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(rb3d.velocity.x, 0f, rb3d.velocity.z));
                anim.SetBool("iswalking", true);
            }
            else
            {
                anim.SetBool("iswalking", false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rod"))
        {
            anim.SetBool("isdead",true);
            canMove = false;
        }
        if(other.gameObject.CompareTag("OpenDoor"))
        {
            if(DoorOpenTrigger.instance.openDoor == false)
            {
                DoorOpenTrigger.instance.openDoor = true;
            }
            else if(DoorOpenTrigger.instance.openDoor == true)
            {
                DoorOpenTrigger.instance.openDoor = false;
            }
        }
    }
}
