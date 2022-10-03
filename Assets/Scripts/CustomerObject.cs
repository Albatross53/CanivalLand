using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerObject : MonoBehaviour
{
    [SerializeField] private float moveTime = 0.5f;
    public Vector3 MoveDirection { set; get; } = Vector3.zero;
    public bool IsMove { set; get; } = false;

	float x = 0;
	float y = 0;
    protected CustomerGenerator Pool;

    private void Update()
    {
        if(x == -1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }else if(x == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public virtual void Creat(CustomerGenerator pool)
    {
        Pool = pool;
        gameObject.SetActive(true);
        InvokeRepeating("SetDir", 1f, 2f);
    }

    void SetDir()
    {
        if (gameObject.transform.position.y < -14)
        {
            y = 1;
        }
        else if (gameObject.transform.position.y > 12)
        {
            y = -1;
        }
        else if (gameObject.transform.position.x < -26)
        {
            x = 1;
        }
        else if (gameObject.transform.position.x > 30)
        {
            x = -1;
        }
        else
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);
        }

        if (x != 0 || y != 0)
		{
			MoveDirection = new Vector3(x, y, 0);
		}
	}

	void BackDir()
    {
		MoveDirection = new Vector3(-MoveDirection.x, -MoveDirection.y, 0);
		SetDir();
	}

	public virtual void Push()
    {
        Pool.PushObject(this);
    }

	private IEnumerator Start()
	{
		while (true)
		{
			if (MoveDirection != Vector3.zero && IsMove == false)
			{
				Vector3 end = transform.position + MoveDirection;

				yield return StartCoroutine(GridSmoothMovement(end));
			}

			yield return null;
		}
	}

	private IEnumerator GridSmoothMovement(Vector3 end)
	{
		Vector3 start = transform.position;
		float current = 0;
		float percent = 0;

		IsMove = true;

		while (percent < 1)
		{
			current += Time.deltaTime;
			percent = current / moveTime;

			transform.position = Vector3.Lerp(start, end, percent);

			yield return null;
		}

		IsMove = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "Wall")
		{
			BackDir();
		}
	}
}
