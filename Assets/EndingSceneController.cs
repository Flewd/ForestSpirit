using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingSceneController : MonoBehaviour
{
    [SerializeField] private GameObject NpcContainer;
    [SerializeField] private Transform _targetParticleDestination;

    [SerializeField] private PlayerDeathHandler _playerDeathHandler;

    private NpcParticleAnimationTrigger[] NpcAnimations;

    [SerializeField] private Image _darkScreenCover;

    [SerializeField] private TextMeshProUGUI[] _creditsTexts;
    [SerializeField] private TextMeshProUGUI[] _creditsTexts2;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCheckpoint.LastRegisteredCheckpointPosition = _targetParticleDestination.position;
        NpcAnimations = NpcContainer.GetComponentsInChildren<NpcParticleAnimationTrigger>();

        StartCoroutine(PlayAllSpiritAnimations());
    }

    public IEnumerator PlayAllSpiritAnimations()
    {
        Color color = _darkScreenCover.color;
        color.a = 1;
        _darkScreenCover.color = color;

        // turn off screen cover
        while (_darkScreenCover.color.a > 0)
        {
            Color newColor = _darkScreenCover.color;
            newColor.a -= Time.deltaTime;
            _darkScreenCover.color = newColor;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2);

        for (int i = 0; i < NpcAnimations.Length; i++)
        {
            NpcAnimations[i].PlayParticleAnimation(_targetParticleDestination);
            yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
        }

        yield return new WaitForSeconds(0.35f);

        _playerDeathHandler.TransitionPlayerBackToCheckpoint();

        yield return new WaitForSeconds(5.75f);

        // turn on screen cover
        while (_darkScreenCover.color.a < 1)
        {
            Color newColor = _darkScreenCover.color;
            newColor.a += Time.deltaTime;
            _darkScreenCover.color = newColor;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);


        // turn on text
        while (_creditsTexts[0].color.a < 1)
        {
            for (int i = 0; i < _creditsTexts.Length; i++)
            {
                Color newColor = _creditsTexts[i].color;
                newColor.a += Time.deltaTime;
                _creditsTexts[i].color = newColor;

            }

            yield return new WaitForEndOfFrame();
        }

        // make sure all texts are fully opaque
        for (int i = 0; i < _creditsTexts.Length; i++)
        {
            Color newColor = _creditsTexts[i].color;
            newColor.a = 1;
            _creditsTexts[i].color = newColor;
        }

        yield return new WaitForSeconds(3);


        // turn off text
        while (_creditsTexts[0].color.a > 0)
        {
            for (int i = 0; i < _creditsTexts.Length; i++)
            {
                Color newColor = _creditsTexts[i].color;
                newColor.a -= Time.deltaTime;
                _creditsTexts[i].color = newColor;

            }

            yield return new WaitForEndOfFrame();
        }

        // make sure all texts are fully opaque
        for (int i = 0; i < _creditsTexts.Length; i++)
        {
            Color newColor = _creditsTexts[i].color;
            newColor.a = 0;
            _creditsTexts[i].color = newColor;
        }

        yield return new WaitForSeconds(1);


        // turn on text
        while (_creditsTexts2[0].color.a < 1)
        {
            for (int i = 0; i < _creditsTexts2.Length; i++)
            {
                Color newColor = _creditsTexts2[i].color;
                newColor.a += Time.deltaTime;
                _creditsTexts2[i].color = newColor;

            }

            yield return new WaitForEndOfFrame();
        }

        // make sure all texts are fully opaque
        for (int i = 0; i < _creditsTexts2.Length; i++)
        {
            Color newColor = _creditsTexts2[i].color;
            newColor.a = 1;
            _creditsTexts2[i].color = newColor;
        }

        yield return new WaitForSeconds(4);


        // turn off text
        while (_creditsTexts2[0].color.a > 0)
        {
            for (int i = 0; i < _creditsTexts2.Length; i++)
            {
                Color newColor = _creditsTexts2[i].color;
                newColor.a -= Time.deltaTime;
                _creditsTexts2[i].color = newColor;

            }

            yield return new WaitForEndOfFrame();
        }

        // make sure all texts are fully opaque
        for (int i = 0; i < _creditsTexts2.Length; i++)
        {
            Color newColor = _creditsTexts2[i].color;
            newColor.a = 0;
            _creditsTexts2[i].color = newColor;
        }

        yield return new WaitForSeconds(1);


        Application.Quit();
    }   
}
