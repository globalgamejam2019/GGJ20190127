using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        ///GameObject.Find("Spawn").GetComponent<Spawn>().count++;

    }

    // Update is called once per frame
    void Update()
    {
        GameObject playerGameObject = null;
        if (GameObject.Find("player"))
        {
            playerGameObject = GameObject.Find("player").gameObject;
        }
        else if (GameObject.Find("BallPlayer"))
        {
            playerGameObject = GameObject.Find("BallPlayer").gameObject;
        }
        else if (GameObject.Find("videoPlayer"))
        {
            playerGameObject = GameObject.Find("videoPlayer").gameObject;
        }

        if (playerGameObject != null)
        {
            score = playerGameObject.GetComponent<PlayerManager>()._playerData.blood;
            
            if(score >= 100) GameObject.Find("GameManager").GetComponent<GameManager>().SetKeyImage();
            else GameObject.Find("GameManager").GetComponent<GameManager>().SetKeycloseImage();
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && score >=100)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().SetUpMonFace(MonFace.Sad);
            UnityAction action = SetupBlack;
            StartCoroutine(
                SetUpFuntionForSeconds(action, 4.0f));
        }
    }

    private void SetupBlack()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>()
            .SetupBackgroundText(BackgroundSceneStatus.EndScene1);

        StartCoroutine(SetUpFuntionForSeconds(ChangeScene, 4));
    }

    private void ChangeScene()
    {
        GameObject.Find("GameChangeManager").GetComponent<GameChangeManager>().ChangeScene(2);
    }
    
    IEnumerator SetUpFuntionForSeconds(UnityAction callBackAction, float timeSecond)
    {
        float currentSecond = 0.0f;
        while (currentSecond <= timeSecond)
        {
            currentSecond += Time.deltaTime;
            yield return null;
        }
        callBackAction();
        yield break;
    }
}
