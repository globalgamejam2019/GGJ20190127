using UnityEngine;
using System.Collections;

public class GController : MonoBehaviour
{

    public GameObject GC;

    public float speed ;

    public float rotation;

    void update()
          {
             transform.Translate(Vector3.right * Time.deltaTime * speed);


            transform.Rotate(new Vector3(0, 0,10), Time.deltaTime * rotation, Space.Self);

    }

    private void OnCollisionEnter2D(Collision coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            GameObject.Destroy(this.gameobject);
        }

    }


}
