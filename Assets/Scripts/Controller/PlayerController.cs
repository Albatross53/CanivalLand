using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// ���̽�ƽ
    /// </summary>
    [SerializeField] VirtualJoystick virtualJoystick;

    public bool isEvent = false;
   
    /// <summary>
    /// �÷��̾� �۷ι�ȭ
    /// </summary>
    static PlayerController g_PlayerController;

    /// <summary>
    /// �÷��̾��� ������
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
    /// �÷��̾� �̵� �ӵ�
    /// </summary>
    [SerializeField] float f_PlayerSpeed;

    /// <summary>
    /// �ִϸ�����
    /// </summary>
    Animator m_Animator;

    /// <summary>
    /// �̺�Ʈ ������ �ڵ�
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
    /// �÷��̾� �̵�
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
