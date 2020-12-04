using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private Rigidbody rb3d;
    [SerializeField]
    private float MoveSpeed = 10f;

    private FixedJoystick joystick;

    private void Awake()
    {
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb3d = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.canMove)
        {
            rb3d.velocity = new Vector3(joystick.Horizontal * MoveSpeed, 0f, joystick.Vertical * MoveSpeed);

            if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(rb3d.velocity.x, 0f, rb3d.velocity.z));
                GameController.instance.playerAnimator.SetBool("isrunning", true);
            }
            else
            {
                GameController.instance.playerAnimator.SetBool("isrunning", false);
            }
        }
        else
        {
            transform.position = transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rod"))
        {
            GameController.instance.playerAnimator.SetBool("isdead",true);
            GameController.instance.canMove = false;
            GameController.instance.isloose = true;
        }
        if(other.gameObject.CompareTag("OpenDoor"))
        {
            if(GameController.instance.openDoor == false)
            {
                GameController.instance.openDoor = true;
            }
            else if(GameController.instance.openDoor == true)
            {
                GameController.instance.openDoor = false;
            }
        }
        if(other.gameObject.CompareTag("SlidePillar"))
        {
            if (GameController.instance.slidePillar == false)
            {
                GameController.instance.slidePillar = true;
            }
            else if (GameController.instance.slidePillar == true)
            {
                GameController.instance.slidePillar = false;
            }
        }
        if(other.gameObject.CompareTag("Laser"))
        {
            if (GameController.instance.switchLasers == false)
            {
                GameController.instance.switchLasers = true;
            }
            else if (GameController.instance.switchLasers == true)
            {
                GameController.instance.switchLasers = false;
            }
        }
    }
}
