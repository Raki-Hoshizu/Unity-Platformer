using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    Animator AnimatorController;
    ParticleSystem Particles;

    void Start()
    {
        AnimatorController = GetComponent<Animator>();
        Particles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AnimatorController.SetTrigger("Trigger");
            Particles.Play();
        }
    }

    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(2f);
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = CurrentSceneIndex + 1;

        if (NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex = 0;
        }

        SceneManager.LoadScene(NextSceneIndex);
    }
}
