using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPos : MonoBehaviour
{

    public GameObject[] obj;
    public Transform[] pos;
    public float count = 0;
    float timer = 0;



    private void Start()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            Instantiate(obj[Random.Range(0, obj.Length)], pos[i].position, Quaternion.identity);
        }
    }

}
