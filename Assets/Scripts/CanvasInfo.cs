using TMPro;
using UnityEngine;

public class CanvasInfo : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void LateUpdate()
    {
        text.SetText("Vidas: " + GameManager.Instance.lives + "\nMonedas: " + GameManager.Instance.coins);
    }
}
