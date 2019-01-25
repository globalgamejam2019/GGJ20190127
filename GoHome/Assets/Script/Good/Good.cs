using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodType
{
    Book = 0,
    Xbox = 1,
    Switch = 2,



    Door = 99,
}

public class Good : MonoBehaviour
{








    #region Public Methons
    /// <summary>
    /// 玩家交互物品
    /// ：player脚本中进行碰撞检测，然后调用此函数
    /// </summary>
    /// <param name="goodType">物品类型</param>
    /// <returns></returns>
    public int GetInteractiveGood(GoodType goodType)
    {
        switch (goodType)
        {
                
        }
        return 0;
    }

    #endregion
}
