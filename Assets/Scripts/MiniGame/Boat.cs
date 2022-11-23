using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] GameObject[] guestPos;
    [SerializeField] Sprite[] guestSprites;
    [SerializeField] float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        int num = Random.Range(0, 3);
        for(int i = 0; i < num; i++)
        {
            int guestNum = Random.Range(0, 3);
            guestPos[i].GetComponent<SpriteRenderer>().sprite = guestSprites[guestNum];
            guestPos[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (this.transform.position.x > -1.1 && this.transform.position.x < 1.1)
        {
            FlumRideManager.Instance.canPic = true;
        }
        else
        {
            FlumRideManager.Instance.canPic = false;
        }

        if (this.transform.position.x > 12)
        {
            Destroy(gameObject);
        }

    }
}
