using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleUiController : MonoBehaviour
{
    public static TitleUiController Instance;
    
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI controlsTextMesh;

    [SerializeField] private Image blackCover;

    private WaitForEndOfFrame waitEndOfFrame = new WaitForEndOfFrame();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Color color = titleTextMesh.color;
        color.a = 1;
        titleTextMesh.color = color;

        color = controlsTextMesh.color;
        color.a = 1;
        controlsTextMesh.color = color;

        color = blackCover.color;
        color.a = 1;
        blackCover.color = color;
        
        StartCoroutine(FadeOutTitleCover());
    }

    private IEnumerator FadeOutTitleCover()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeCoverOff());
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

    public IEnumerator FadeCoverOn()
    {
        // turn on screen cover
        while (blackCover.color.a < 1)
        {
            Color newColor = blackCover.color;
            newColor.a += Time.deltaTime;
            blackCover.color = newColor;

            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeCoverOff()
    {
        // turn on screen cover
        while (blackCover.color.a > 0)
        {
            Color newColor = blackCover.color;
            newColor.a -= Time.deltaTime;
            blackCover.color = newColor;

            yield return new WaitForEndOfFrame();
        }
    }
}
