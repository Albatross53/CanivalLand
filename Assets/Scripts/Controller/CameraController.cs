using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// �÷��̾�ĳ����
    /// </summary>
    [SerializeField]
    GameObject m_player;

    void Update()
    {
        Vector3 m_playerPos = m_player.transform.position;
        transform.position = new Vector3(m_playerPos.x, m_playerPos.y, -10);
    }
}
