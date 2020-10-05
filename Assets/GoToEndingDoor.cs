using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToEndingDoor : MonoBehaviour
{
    private bool transitionStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && transitionStarted == false)
        {
            StartCoroutine(TranitionToNextScene());
            transitionStarted = true;
        }
    }

    private IEnumerator TranitionToNextScene()
    {
        yield return StartCoroutine(TitleUiController.Instance.FadeCoverOn());
        SceneManager.LoadScene("EndingScene");
    }
}
