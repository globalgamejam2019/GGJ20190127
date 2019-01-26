using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum GoodEffect
{
    Buff = 0,
    Debuff = 1,
    None = 2
}

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject good;
    public byte BuffNu;
    public byte DeBuffNu;
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
    public int GetInteractiveGood(GoodEffect effect)
    {

        GameObject spawn = GameObject.Find("Spawn");

        //spawn.GetComponents<spawn>().count = spawn.GetComponents<spawn>().count++;



        switch (effect)
        {
            case GoodEffect.Buff:

                return BuffNu;

            case GoodEffect.Debuff:

                return DeBuffNu;

            case GoodEffect.None:

                return DoorNu;          

        }
        return 0;
    }






    

    #endregion
}
