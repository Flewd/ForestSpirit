using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] GameObject _entirePlayerGameObject;
    [SerializeField] SkinnedMeshRenderer _playerModel;
    [SerializeField] ParticleSystem _spiritParticles;

    [SerializeField] float _particleAnimationDuration = 3;
    [SerializeField] private Ease _particleEaseType = Ease.InOutSine;

    bool animationStarted = false;

    public void TransitionPlayerBackToCheckpoint()
    {
        if (animationStarted == false)
        {
            animationStarted = true;
            StartCoroutine(PlaySpiritAnimation());
        }
    }

    private IEnumerator PlaySpiritAnimation()
    {

        Vector3 playerSpawnPos = PlayerCheckpoint.LastRegisteredCheckpointPosition;

        TweenParams tweenParams = new TweenParams()
            .SetEase(_particleEaseType);

        _spiritParticles.gameObject.SetActive(true);
        _spiritParticles.Play();

        _entirePlayerGameObject.transform.DOMove(playerSpawnPos, _particleAnimationDuration).SetAs(tweenParams).OnComplete(() => StartCoroutine(CleanupParticles()));


        _playerModel.gameObject.SetActive(false);

        yield return null;

// can fade player out if we use a transparent supported material

        //Material mat = _playerModel.material;

        /*
        while (mat.color.a > 0)
        {
            Color newColor = mat.color;
            newColor.a -= Time.deltaTime;
            mat.color = newColor;
            _playerModel.material = mat;
            yield return new WaitForEndOfFrame();
        }
        */
    }

    private IEnumerator CleanupParticles()
    {
        animationStarted = false;
        _playerModel.gameObject.SetActive(true);

        //Material mat = _playerModel.material;

        /*
        while (mat.color.a < 1)
        {
            Color newColor = mat.color;
            newColor.a += Time.deltaTime;
            mat.color = newColor;
            _playerModel.material = mat;
            yield return new WaitForEndOfFrame();
        }
        */

        _spiritParticles.Stop();
        yield return new WaitForSeconds(2);
        _spiritParticles.gameObject.SetActive(false);
    }
}
