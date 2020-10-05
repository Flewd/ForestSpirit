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

    [SerializeField] private TextMeshProUGUI _thanksText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCheckpoint.LastRegisteredCheckpointPosition = _targetParticleDestination.position;
        NpcAnimations = NpcContainer.GetComponentsInChildren<NpcParticleAnimationTrigger>();

        StartCoroutine(PlayAllSpiritAnimations());
    }

    public IEnumerator PlayAllSpiritAnimations()
    {
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
        while (_thanksText.color.a < 1)
        {
            Color newColor = _thanksText.color;
            newColor.a += Time.deltaTime;
            _thanksText.color = newColor;
            yield return new WaitForEndOfFrame();
        }
    }
}
