using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private BoxCollider lineBox;

    private bool isHitting = false;
    private Vector3 directionOfParticles;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        lineBox = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.switchLasers)
        {
            line.enabled = false;
            lineBox.enabled = false;
        }
        if(!GameController.instance.switchLasers)
        {
            line.enabled = true;
            lineBox.enabled = true;
        }
        if(isHitting)
        {
            GameController.instance.bloodEffect.transform.position = GameController.instance.player.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameController.instance.canMove = false;
            isHitting = true;
            GameController.instance.isloose = true;
            GameController.instance.bloodEffect.Play();
            directionOfParticles = line.transform.position - GameController.instance.player.transform.position;
            GameController.instance.bloodEffect.transform.position = GameController.instance.player.transform.position + directionOfParticles.normalized * 0.5f;
            GameController.instance.bloodEffect.transform.rotation = Quaternion.LookRotation(directionOfParticles);
            GameController.instance.playerAnimator.SetBool("isdead", true);
        }
    }
}
