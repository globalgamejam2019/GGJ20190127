using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{

    public GameObject MainCamera = GameObject.Find("Main Camera");//摄像机
    public float SCENE_WIDTH;//板的宽度，即Plane的宽度
    public int SCENE_NUM;//板的数量
    private float total_width;//求现有Plane并排在一起的总宽，用于计算板的补位点
    private Vector3 scene_position;//此板所在的位置

    void Start()
    {
        total_width = SCENE_WIDTH * SCENE_NUM;
        scene_position = this.transform.position;

       MainCamera = GameObject.Find("Main Camera");//摄像机
}

void Update()
    {
        if (scene_position.x + total_width / 2.0f < MainCamera.transform.position.x)
        {
            scene_position.x += total_width;
            this.transform.position = scene_position;
        }

        if (MainCamera.transform.position.x < scene_position.x - total_width / 2.0f)
        {
            scene_position.x -= total_width;
            this.transform.position = scene_position;
        }
    }
}
