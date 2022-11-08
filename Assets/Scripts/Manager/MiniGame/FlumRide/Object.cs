using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] Sprite[] obsSprites;
    [SerializeField] float speed = 5.0f;

    private void Start()
    {
        FlumRideManager.Instance.NopePic = false;
        int num = Random.Range(0, 2);
        this.GetComponent<SpriteRenderer>().sprite = obsSprites[num];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (this.transform.position.x > -3.3 && this.transform.position.x < 3.3)
        {
            FlumRideManager.Instance.NopePic = true;
        }
        else
        {
            FlumRideManager.Instance.NopePic = false;
        }
        
        if (this.transform.position.x > 12)
        {
            Destroy(gameObject);
        }

    }
}
