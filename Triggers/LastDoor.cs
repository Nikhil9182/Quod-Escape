using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(GameController.instance.keyCount > 0)
            {
                GameController.instance.timer.SetBool("onTimer", false);
                GameController.instance.canMove = false;
                GameController.instance.isWon = true;
                GameController.instance.playerAnimator.SetBool("iswon", true);
            }
        }
    }
}
