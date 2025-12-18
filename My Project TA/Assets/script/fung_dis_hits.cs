using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject panel;
    public Image imageDisplay;
    public Text titleText;
    public Text bodyText;

    [Header("Tombol Tutup")]
    public Button closeButton;

    void Start()
    {
        HideInfo();

        // Pastikan tombol tutup bisa dipakai
        if (closeButton != null)
            closeButton.onClick.AddListener(HideInfo);
    }

    public void ShowInfo(Sprite image, string title, string body)
    {
        if (panel == null) return;

        panel.SetActive(true);

        if (imageDisplay != null) imageDisplay.sprite = image;
        if (titleText != null) titleText.text = title;
        if (bodyText != null)
        {
            bodyText.text = body;
            LayoutRebuilder.ForceRebuildLayoutImmediate(bodyText.rectTransform); // Agar ukuran konten disesuaikan
        }
    }

    public void HideInfo()
    {
        if (panel != null)
            panel.SetActive(false);
    }
}
