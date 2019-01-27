using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{



    private int score = GameObject.Find("Player").GetComponent<PlayerManager>()._playerData.blood;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Spawn").GetComponent<Spawn>().count++;

    }

    // Update is called once per frame
    void Update()
    {

      

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" || score ==100)
        {
            print("11");
            //GameObject.Find("GameChangeManager").GetComponent<GameChangeManager>().ChangeScene(2);
            GameObject.Find("GameManager").GetComponent<GameManager>().SetUpMonFace(MonFace.Sad);

        }
    }

}
