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
    public int BuffNu = 20;
    public int DeBuffNu = -20;
    public byte DoorNu;

    public AudioSource BuffAudio;
    public AudioSource DeBuffAudio;


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



    public int GetInteractiveGood(GoodEffect effect)
    {

       
        GameObject.Find("Spawn").GetComponent<Spawn>().count++;



        switch (effect)
        {
            case GoodEffect.Buff:

                Destroy(this.gameObject);
                return BuffNu;

            case GoodEffect.Debuff:
                Destroy(this.gameObject);
                return DeBuffNu;

            case GoodEffect.None:
                Destroy(this.gameObject);
                return DoorNu;          

        }
        return 0;
    }






    

    #endregion
}
