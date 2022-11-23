using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    float speed = 3.0f;

    void Update()
    {
        this.gameObject.transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(this.gameObject.transform.position.y < -11)
        {
            this.gameObject.transform.position = new Vector2(0, 21);
        }
    }
}
