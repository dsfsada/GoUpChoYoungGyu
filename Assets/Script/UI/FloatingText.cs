using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public static FloatingText Instance;
    public GameObject floatingTextPrefab; // 표시될 FloatingText 프리팹

    private void Start()
    {
        Instance = this;        //class 생성
    }

    // 체력이 변경될 때 호출되는 메서드
    public void ShowFloatingText(Transform position, float xOffset, float yOffset, string text, Color _color)
    {
        Vector3 floatingTextOffSet = position.position + new Vector3(xOffset, yOffset);

        TextMesh textMesh = floatingTextPrefab.GetComponent<TextMesh>(); // 생성된 FloatingText에 Text 컴포넌트가 있다면 텍스트 설정

        if (textMesh != null)
        {
            textMesh.text = text;
            textMesh.color = _color;
        }

        GameObject floatingTextObject = Instantiate(floatingTextPrefab, floatingTextOffSet, Quaternion.identity); // 표시될 FloatingText 프리팹의 복제본 생성

        Destroy(floatingTextObject, 0.2f);
    }
}
