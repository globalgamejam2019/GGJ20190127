using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float Movespeed = 6.0f;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("runright", true);
            transform.Translate(Vector3.right * Time.deltaTime * Movespeed);
        }
       else
        {
            anim.SetBool("runright", false);
        }//向右跑动
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("runleft", true);
            transform.Translate(Vector3.left * Time.deltaTime * Movespeed);
        }       
        else
        {
            anim.SetBool("runleft", false);
            
        }//向左跑
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("runback", true);
            transform.Translate(Vector3.up * Time.deltaTime * Movespeed);
        }
        else
        {
            anim.SetBool("runback", false);
        }//向后跑
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("runforward", true);
            transform.Translate(Vector3.down * Time.deltaTime * Movespeed);
        }
        else
        {
            anim.SetBool("runforward", false);
        }//向前跑
        //同时按SD时触发斜向下跑if (Input.GetKey(KeyCode.S)&& Input.GetKey(KeyCode.D))
        //{
           anim.SetBool("forwardright", true);
       // }
        //else
       // {
          //  anim.SetBool("forwardright", false);
        //}
    }
}
