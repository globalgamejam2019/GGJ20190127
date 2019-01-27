using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum GoodEffect
{
    Buff = 0,
    Debuff = 1,
    None = 2
}

public class Good : MonoBehaviour
{

    public GameObject good;
    private int BuffNu = 20;
    private int DeBuffNu = -20;
    private byte DoorNu;

    public AudioClip BuffAudio1;
    public AudioClip BuffAudio2;
    public AudioClip DeBuffAudio1;
    public AudioClip DeBuffAudio2;


    //private enum GoodType
    //{
    //    Book = 0,
    //    Xbox = 1,
    //    Switch = 2,
    //    Door = 99,
    //}

    #region Public Methons
    /// <summary>
    /// 玩家交互物品
    /// ：player脚本中进行碰撞检测，然后调用此函数
    /// </summary>
    /// <param name="goodType">物品类型</param>
    /// <returns></returns>
    /// 
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            if (transform.name == "pingdiguo")
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = DeBuffAudio1;
                GetComponent<AudioSource>().Play(); ;
            }
            else if (transform.name == "53")
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = DeBuffAudio2;
                GetComponent<AudioSource>().Play(); ;
            }
            else if (transform.name == "ball")
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = BuffAudio1;
                GetComponent<AudioSource>().Play(); ;
            }
            else if (transform.name == "shoubing")
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = BuffAudio2;
                GetComponent<AudioSource>().Play(); ;
            }
        }
    }



    public int GetInteractiveGood(GoodEffect effect)
    {

       
        GameObject.Find("Spawn").GetComponent<Spawn>().count++;

            

        switch (effect)
        {
            case GoodEffect.Buff:
                
                transform.parent.position = new Vector3(1000, 1000, 1000);
                //Destroy(this.gameObject);
                return BuffNu;

            case GoodEffect.Debuff:
                
                transform.parent.position = new Vector3(1000, 1000, 1000);
                //Destroy(this.gameObject);
                return DeBuffNu;

            case GoodEffect.None:
              
                Destroy(this.gameObject);
                return DoorNu;          

        }

        return 0;
    }






    

    #endregion
}
