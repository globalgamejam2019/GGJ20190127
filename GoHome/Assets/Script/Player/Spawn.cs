using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject[] Good;





    public float count = 0;


    float timer = 0;

    // Use this for initialization
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (count != 0)
        {
            spawn();

        }


 
    }


    void spawn()
    {


        timer += Time.deltaTime;
        //GameObject instance = Instantiate(Resources.Load("Ball")) as GameObject;
        /*if (timer >= 3 || timer =0.5)
        {
            Instantiate(Good[Random.Range(0, Good.Length)], new Vector2(-9.5f, Random.Range(10, -8)), Quaternion.identity);


            timer = 0;

        }*/

    }

}
