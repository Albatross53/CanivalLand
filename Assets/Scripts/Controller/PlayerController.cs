using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 조이스틱
    /// </summary>
    [SerializeField] VirtualJoystick virtualJoystick;

    public bool isEvent = false;
   
    /// <summary>
    /// 플레이어 글로벌화
    /// </summary>
    static PlayerController g_PlayerController;

    /// <summary>
    /// 플레이어의 움직임
    /// </summary>
    bool stop = false;

    public bool IsStop
    {
        get { return stop; }
        set
        {
            stop = value;
        }
    }
    /// <summary>
    /// 플레이어 이동 속도
    /// </summary>
    [SerializeField] float f_PlayerSpeed;

    /// <summary>
    /// 애니메이터
    /// </summary>
    Animator m_Animator;

    /// <summary>
    /// 이벤트 아이템 코드
    /// </summary>
    public int m_PickCode = 0;

    [SerializeField] Transform[] playerPos;


    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        g_PlayerController = this;

        isEvent = false;
    }

    void Update()
    {
        if (!IsStop)
        {
            PlayerMovement();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerPosLoad();
        }
    }

    /// <summary>
    /// 플레이어 이동
    /// </summary>
    void PlayerMovement()
    {
#if UNITY_EDITOR
        
        float f_HorizontalMovement = Input.GetAxisRaw("Horizontal");
        float f_VerticalMovement = Input.GetAxisRaw("Vertical");
        
#endif

        float horizontalMovement = virtualJoystick.Horizontal();
        float verticalMovement = virtualJoystick.Vertical();

        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            m_Animator.SetBool("Walking", true);
        }
        else if (horizontalMovement == 0 && verticalMovement == 0)
        {
            m_Animator.SetBool("Walking", false);
        }

        if (horizontalMovement > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontalMovement < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        transform.Translate(horizontalMovement * Time.deltaTime * f_PlayerSpeed, verticalMovement * Time.deltaTime * f_PlayerSpeed, 0);
    }

    public void PlayerPosLoad()
    {
        gameObject.transform.position = playerPos[GameValueManager.Instance.IsAttractionPosCode].position;
    }


    
    public static PlayerController Instance
    {
        get
        {
            return g_PlayerController;
        }
    }
}
