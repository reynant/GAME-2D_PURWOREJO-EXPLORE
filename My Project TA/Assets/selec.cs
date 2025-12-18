using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelDisplayController : MonoBehaviour
{
    [Header("UI yang disambungkan dari Inspector")]
    [SerializeField] private GameObject panelToShow;
    [SerializeField] private Text textInPanel;
    [SerializeField] private Button triggerButton;

    [Header("Teks yang ditampilkan di panel")]
    [TextArea]
    [SerializeField] private string displayText;

    [Header("Waktu tampil panel (detik)")]
    [SerializeField] private float displayDuration = 3f;

    private void Awake()
    {
        if (panelToShow != null)
            panelToShow.SetActive(false);
    }

    private void Start()
    {
        panelToShow.SetActive(false);
        if (triggerButton != null)
            triggerButton.onClick.AddListener(ShowPanelForSeconds);
    }

    public void ShowPanelForSeconds()
    {
        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
            if (textInPanel != null)
                textInPanel.text = displayText;

            StopAllCoroutines(); // kalau user klik berulang-ulang
            StartCoroutine(HideAfterDelay());
        }
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        if (panelToShow != null)
            panelToShow.SetActive(false);
    }
}
