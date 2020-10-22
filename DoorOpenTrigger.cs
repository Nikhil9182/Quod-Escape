using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    public static DoorOpenTrigger instance;

    public Transform Door;

    public bool openDoor = false;
    public float doorOpenDistance = 7.5f;
    public float groundDistance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        groundDistance = Door.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(openDoor)
        {
            if(Door.position.y <= doorOpenDistance)
            {
                Door.position = new Vector3(Door.position.x, Door.position.y + (doorOpenDistance * Time.deltaTime), Door.position.z);
            }
        }
        else if(!openDoor)
        {
            if(Door.position.y > groundDistance)
            {
                Door.position = new Vector3(Door.position.x, Door.position.y - (doorOpenDistance * Time.deltaTime), Door.position.z);
            }
        }
    }
}
