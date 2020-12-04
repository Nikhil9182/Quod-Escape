using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public int keyID;
    [SerializeField]
    private float keyRotSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(keyRotSpeed * Time.deltaTime, 0, 0);
        if(GameController.instance.isSeen)
        {
            for (int i = 0; i < 3; i++) 
                GameController.instance.key[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameController.instance.keyCount += 1;
            GameController.instance.key[keyID].SetActive(false);
            GameController.instance.KeyVisibilityUI();
        }
    }
}
