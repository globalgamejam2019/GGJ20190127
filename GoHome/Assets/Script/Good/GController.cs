using UnityEngine;
using System.Collections;

public class GController : MonoBehaviour
{

    public GameObject GC;

    public float speed ;




    void Update()

          {


        transform.Translate(Vector3.right * Time.deltaTime * speed);


          }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            GameObject.Destroy(this.gameObject);


        }

    }


}
