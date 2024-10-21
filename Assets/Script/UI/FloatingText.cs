using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public static FloatingText Instance;
    public GameObject floatingTextPrefab; // ǥ�õ� FloatingText ������

    private void Start()
    {
        Instance = this;        //class ����
    }

    // ü���� ����� �� ȣ��Ǵ� �޼���
    public void ShowFloatingText(Transform position, float xOffset, float yOffset, string text, Color _color)
    {
        Vector3 floatingTextOffSet = position.position + new Vector3(xOffset, yOffset);

        TextMesh textMesh = floatingTextPrefab.GetComponent<TextMesh>(); // ������ FloatingText�� Text ������Ʈ�� �ִٸ� �ؽ�Ʈ ����

        if (textMesh != null)
        {
            textMesh.text = text;
            textMesh.color = _color;
        }

        GameObject floatingTextObject = Instantiate(floatingTextPrefab, floatingTextOffSet, Quaternion.identity); // ǥ�õ� FloatingText �������� ������ ����

        Destroy(floatingTextObject, 0.2f);
    }
}
