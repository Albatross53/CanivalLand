using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerObject : MonoBehaviour
{
    [SerializeField] private float moveTime = 3.0f;
    public Vector3 MoveDirection { set; get; } = Vector3.zero;
    public bool IsMove { set; get; } = false;
    public bool IsPlayerBlock { set; get; } = false;
    public bool IsOverlapBlock { set; get; } = false;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask overlapLayer;
    Vector3 movePos;

    int x = 0;
    int y = 0;

    string[] State = { "Idle", "Walk" };

    public SpriteRenderer SR;
    public SpriteRenderer Hair_SR;
    public SpriteRenderer Face_SR;

    public List<Sprite> StateSprites;

    public List<Sprite> hairSprites;
    public List<Sprite> faceSprites;

    WaitForSeconds Delay01 = new WaitForSeconds(0.1f),
                   Delay05 = new WaitForSeconds(0.5f),
                   Delay1 = new WaitForSeconds(1);

    List<Sprite> CurStateSprites = new List<Sprite>();

    List<Sprite> CurHairSprites = new List<Sprite>();
    List<Sprite> CurFaceSprites = new List<Sprite>();


    int characterIndex;
    int hairIndex;
    int faceIndex;

    const int STATESIZE = 12;
    const int SIZE = 6;

    protected CustomerGenerator Pool;

    public virtual void Creat(CustomerGenerator pool)
    {
        Pool = pool;
        gameObject.SetActive(true);

        movePos = gameObject.transform.position;

        characterIndex = Random.Range(0, 3);
        hairIndex = Random.Range(0, 3);
        faceIndex = Random.Range(0, 3);

        SetCharacter();
        SetHair();
        SetFace();
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

    void Update()
    {
        if (x == -1)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (x == 1)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void SetDir()
    {
        x = Random.Range(-1, 2);
        y = Random.Range(-1, 2);

        if (x != 0 || y != 0)
        {
            MoveDirection = new Vector3(x, y, 0);
            movePos = transform.position + MoveDirection;

            IsPlayerBlock = Physics2D.OverlapCircle(movePos, 0.5f, playerLayer);
            IsOverlapBlock = Physics2D.OverlapCircle(movePos, 0.5f, overlapLayer);
            if (IsPlayerBlock || IsOverlapBlock)
            {
                movePos = transform.position;
            }

            StartCoroutine(GridSmoothMovement(movePos));
        }
    }

    public void SetCharacter()
    {
        CurStateSprites.Clear();
        for (int i = 0; i < STATESIZE; i++) CurStateSprites.Add(StateSprites[STATESIZE * characterIndex + i]);
    }

    public void SetHair()
    {
        CurHairSprites.Clear();
        for (int i = 0; i < SIZE; i++) CurHairSprites.Add(hairSprites[SIZE * hairIndex + i]);
    }

    public void SetFace()
    {
        CurFaceSprites.Clear();
        for (int i = 0; i < SIZE; i++) CurFaceSprites.Add(faceSprites[SIZE * faceIndex + i]);
    }

    IEnumerator Start()
    {
        StartCoroutine("Hair");
        StartCoroutine("Face");

        int move = Random.Range(0, 2);

        while (true) yield return StartCoroutine(State[1]);
    }

    IEnumerator Idle()
    {
        SR.sprite = CurStateSprites[0];
        yield return Delay05;
        SR.sprite = CurStateSprites[1];
        yield return Delay05;
        SR.sprite = CurStateSprites[2];
        yield return Delay05;
        SR.sprite = CurStateSprites[3];
        yield return Delay05;
        SR.sprite = CurStateSprites[4];
        yield return Delay05;
        SR.sprite = CurStateSprites[5];
        yield return Delay05;
    }

    IEnumerator Walk()
    {
        SetDir();
        SR.sprite = CurStateSprites[6];
        yield return Delay05;
        SR.sprite = CurStateSprites[7];
        yield return Delay05;
        SR.sprite = CurStateSprites[8];
        yield return Delay05;
        SR.sprite = CurStateSprites[9];
        yield return Delay05;
        SR.sprite = CurStateSprites[10];
        yield return Delay05;
        SR.sprite = CurStateSprites[11];
        yield return Delay05;
    }

    IEnumerator Hair()
    {
        Hair_SR.sprite = CurHairSprites[0];
        yield return Delay05;
        Hair_SR.sprite = CurHairSprites[1];
        yield return Delay05;
        Hair_SR.sprite = CurHairSprites[2];
        yield return Delay05;
        Hair_SR.sprite = CurHairSprites[3];
        yield return Delay05;
        Hair_SR.sprite = CurHairSprites[4];
        yield return Delay05;
        Hair_SR.sprite = CurHairSprites[5];
        yield return Delay05;
    }

    IEnumerator Face()
    {
        Face_SR.sprite = CurFaceSprites[0];
        yield return Delay05;
        Face_SR.sprite = CurFaceSprites[1];
        yield return Delay05;
        Face_SR.sprite = CurFaceSprites[2];
        yield return Delay05;
        Face_SR.sprite = CurFaceSprites[3];
        yield return Delay05;
        Face_SR.sprite = CurFaceSprites[4];
        yield return Delay05;
        Face_SR.sprite = CurFaceSprites[5];
        yield return Delay05;
    }

    public virtual void Push()
    {
        Pool.PushObject(this);
    }
}
