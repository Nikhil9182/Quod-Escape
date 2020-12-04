using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    Animator anim;
    int random_number;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        random_number = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if(random_number == 2)
        {
            anim.SetBool("wave1", true);
        }
        if(random_number == 3)
        {
            anim.SetBool("wave2", true);
        }
    }
}
