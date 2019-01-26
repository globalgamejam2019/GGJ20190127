using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour {

    public float movespeed = 3f;
    Animator anim;
    
    void Start () {
        anim = GetComponent<Animator>();	
	}
    void Update () {
        if(Input.GetKey(KeyCode.D))
        {
            anim.SetBool("run",true);
            transform.Translate(Vector3.right * Time.deltaTime * movespeed);
        }
        else
        {
            anim.SetBool("run",false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("runleft", true);
            transform.Translate(Vector3.left * Time.deltaTime * movespeed);
        }
        else
        {
            anim.SetBool("runleft", false);
        }

    }
}
