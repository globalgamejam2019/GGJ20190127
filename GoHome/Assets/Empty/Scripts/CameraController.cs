using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject Player;
    private Vector3 offset;

    void Start()
    {
        this.offset = this.transform.position - Player.transform.position;//先求出摄像机和主角的两点距离，也就是我们在当前场景布置的两者距离向量
    }

    void Update()
    {
        this.transform.position = new Vector3(Player.transform.position.x + this.offset.x, this.transform.position.y, this.transform.position.z);
        //在每一帧保证摄像机始终在X轴方向和玩家的距离一直，当然，这要不是横轴游戏，自然其它轴也应该保持一段距离
    }
}
