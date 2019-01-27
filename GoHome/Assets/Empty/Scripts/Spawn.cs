using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject[] Good;
    public Transform[] SPos;
    public float count = 0;
    private bool isFirstEat = false;

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
        //spawn();
        if (count == 1 && !isFirstEat)
        {
            isFirstEat = true;
            Singleton<GameManager>.Instance.SetAudioBgm(AudioSoundType.eatGood);

            /*Singleton<GameManager>.Instance.SetVideoPlayer();
            Singleton<GameManager>.Instance.SetBallPlayer();*/
        }
    }


    void spawn()
    {

        timer += Time.deltaTime;
     
        if (timer >= 1)
        {
                Instantiate(Good[Random.Range(0, Good.Length)], SPos[Random.Range(0, SPos.Length)].position, Quaternion.identity);

            timer = 0;

        }



    }




}
