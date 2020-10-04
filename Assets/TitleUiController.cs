using System.Collections;
using TMPro;
using UnityEngine;

public class TitleUiController : MonoBehaviour
{
    public static TitleUiController Instance;
    
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI controlsTextMesh;

    private WaitForEndOfFrame waitEndOfFrame = new WaitForEndOfFrame();

    private void Awake()
    {
        Instance = this;
    }

    public void StartFadeOut()
    {
        StartCoroutine(PlayFadeOutSequence());
    }

    private IEnumerator PlayFadeOutSequence()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(FadeOutTextMesh(titleTextMesh));
        yield return new WaitForSeconds(5);
        StartCoroutine(FadeOutTextMesh(controlsTextMesh));
    }

    private IEnumerator FadeOutTextMesh(TextMeshProUGUI textMesh)
    {
        while(textMesh.color.a > 0)
        {
            textMesh.color = new Color(
                textMesh.color.r,
                textMesh.color.g,
                textMesh.color.b,
                textMesh.color.a - Time.deltaTime);

            yield return waitEndOfFrame;
        }
    }
}
