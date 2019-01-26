using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{

    float speed = 3.0F;

    void Start()
    {

    }

    void Update()
    {
        if (transform.position.x < -10)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
