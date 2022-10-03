using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void GoLeft()
    {
        if (transform.position.x > -8)
        {
            transform.position = new Vector2(transform.position.x - 2, -1);
        }


    }

    public void GoRight()
    {
        if (transform.position.x < 8)
        {
            transform.position = new Vector2(transform.position.x + 2, -1);
        }
    }

    IEnumerator blink()
    {
        int countTime = 0;
        while (countTime < 10)
        {
            if (countTime % 2 == 0)
            {
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            }
            else
            {
                spriteRenderer.color = new Color32(255, 255, 255, 180);
            }

            yield return new WaitForSeconds(0.2f);
            countTime++;
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Animal")
        {
            StartCoroutine("blink");
        }
    }
}
