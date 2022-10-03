using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    /// <summary>
    /// 조이스틱백그라운드 이미지
    /// </summary>
    Image imageBackground;
    Image imageController;
    Vector2 touchPosition;

    private void Awake()
    {
        imageBackground = this.GetComponent<Image>();
        imageController = this.transform.GetChild(0).GetComponent<Image>();
    }

    /// <summary>
    /// 터치상태일때 매 프레임
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (imageBackground.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            touchPosition.x = (touchPosition.x / imageBackground.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / imageBackground.rectTransform.sizeDelta.y);

            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

            //touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;  
            if (touchPosition.magnitude > 1)
            {
                touchPosition = touchPosition.normalized;
            }
            

            imageController.rectTransform.anchoredPosition = new Vector2
                (touchPosition.x * imageBackground.rectTransform.sizeDelta.x / 2,
                touchPosition.y * imageBackground.rectTransform.sizeDelta.y / 2);
            
        }
    }

    /// <summary>
    /// 터치 시작 1회
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;
    }

    /// <summary>
    /// 터치 종료 1회
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        imageController.rectTransform.anchoredPosition = Vector2.zero;
        touchPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return touchPosition.x;
    }

    public float Vertical()
    {
        return touchPosition.y;
    }
}
