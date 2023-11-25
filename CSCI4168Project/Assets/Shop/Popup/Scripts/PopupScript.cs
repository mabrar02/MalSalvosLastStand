using UnityEngine;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Text titleUIText;
    [SerializeField] Text contentUIText;
    [SerializeField] Button closeUIButton;

    public static PopupScript Instance;

    private void Awake()
    {
        Instance = this;
        closeUIButton.onClick.RemoveAllListeners();
        closeUIButton.onClick.AddListener(Hide);
    }
    public void Show ()
    {
        canvas.SetActive(true);
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }
}

