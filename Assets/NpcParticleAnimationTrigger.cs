using DG.Tweening;
using System.Collections;
using UnityEngine;

public class NpcParticleAnimationTrigger : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer _meshRenderer;

    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] Transform _targetParticlePosition;
    [SerializeField] float _particleAnimationDuration = 3;
    [SerializeField] private Ease _particleEaseType = Ease.InOutSine;

    bool animationStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (animationStarted == false && other.tag == "Player")
        {
            PlayParticleAnimation();
        }
    }

    public void PlayParticleAnimation(Transform overridePosition = null)
    {
        if(overridePosition != null)
        {
            _targetParticlePosition = overridePosition;
        }

        animationStarted = true;
        StartCoroutine(PlaySpiritAnimation());
    }

    private IEnumerator PlaySpiritAnimation()
    {
        if (_targetParticlePosition != null)
        {
            TweenParams tweenParams = new TweenParams()
                .SetEase(_particleEaseType);

            _particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();

            _particleSystem.transform.DOMove(_targetParticlePosition.position, _particleAnimationDuration).SetAs(tweenParams).OnComplete(() => StartCoroutine(CleanupParticles()));
        }

        Material mat = _meshRenderer.material;

        while (mat.color.a > 0)
        {
            Color newColor = mat.color;
            newColor.a -= Time.deltaTime;
            mat.color = newColor;
            _meshRenderer.material = mat;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CleanupParticles()
    {
        _particleSystem.Stop();
        yield return new WaitForSeconds(15);
        _particleSystem.gameObject.SetActive(false);
    }
}
