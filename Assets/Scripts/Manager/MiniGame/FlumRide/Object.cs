using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public int type;
    float x;

    static Object g_object;

    private void Start()
    {
        x = this.transform.position.x;
        InvokeRepeating("Move", 0f, 0.5f);
        //MoveStart();
    }

    public void MoveStart()
    {
        StartCoroutine("Movement");
    }
    void Move()
    {
        x++;
        gameObject.transform.position = new Vector3(x, -2, 0);
    }

    IEnumerator Movement()
    {
        x++;
        gameObject.transform.position = new Vector3(x, -2, 0);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("Movement");
    }

    private void Update()
    {
        if(transform.position.x >= 10)
        {
            gameObject.transform.position = new Vector3(-10, -2, 0);
            Destroy(gameObject);
            //StopCoroutine("Movement");
        }
    }

    public static Object Instance
    {
        get { return g_object; }
    }
}
