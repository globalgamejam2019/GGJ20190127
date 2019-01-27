using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBtn : MonoBehaviour
{
    public void ChangeToScene0()
    {
        GameObject.Find("GameChangeManager").GetComponent<GameChangeManager>().ChangeScene(0);
    }
}
