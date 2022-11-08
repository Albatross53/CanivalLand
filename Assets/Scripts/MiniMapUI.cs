using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapUI : MonoBehaviour
{
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform bottom;
    [SerializeField] Transform top;

    [SerializeField] Image miniMapImage;
    [SerializeField] Image miniMapPlayerImage;

    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        var inst = Instantiate(miniMapImage.material);
        miniMapImage.material = inst;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Vector2 mapArea = 
                new Vector2(Vector3.Distance(left.position, right.position), Vector3.Distance(bottom.position, top.position));
            Vector2 charPos = 
                new Vector2(Vector3.Distance(left.position, new Vector3(player.transform.position.x, 0f, 0f)), 
                Vector3.Distance(bottom.position, new Vector3(0f, player.transform.position.y, 0f)));
            Vector2 normalPos = new Vector2(charPos.x / mapArea.x, charPos.y / mapArea.y);

            miniMapPlayerImage.rectTransform.anchoredPosition =
                new Vector2(miniMapImage.rectTransform.sizeDelta.x * normalPos.x, miniMapImage.rectTransform.sizeDelta.y * normalPos.y);
        }
    }
}
