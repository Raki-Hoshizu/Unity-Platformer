using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    int Health;
    [Range(1, 5)][SerializeField] int MaxHealth = 2;

    Animator PlayerAnimator;
    PlayerMovement Movement;
    Rigidbody2D PlayerRigidBody;
    CapsuleCollider2D PlayerCollider;

    [SerializeField] Image[] Hearts;
    [SerializeField] Sprite FullHeart;
    [SerializeField] Sprite EmptyHeart;

    [Header("Health HUD")]
    [SerializeField] CanvasGroup HealthUI;
    [SerializeField] float FadeInDuration = 0.5f;
    [SerializeField] float DisplayDuration = 2f;
    [SerializeField] float FadeOutDuration = 3f;
    Coroutine FadeInCoroutine;
    Coroutine WaitDisplayCoroutine;
    Coroutine FadeOutCoroutine;

    [SerializeField] LayerMask EnemyLayers;

    void Start()
    {
        Health = MaxHealth;
        PlayerAnimator = GetComponent<Animator>();
        Movement = GetComponent<PlayerMovement>();
        PlayerRigidBody = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();
        FadeOutCoroutine = StartCoroutine(FadeOut());
    }

    void Update()
    {
        if (Health > MaxHealth)
            Health = MaxHealth;
        
        for (int i = 0; i < Hearts.Length; i++)
        {
            Hearts[i].enabled = i < MaxHealth ? true : false;
            Hearts[i].sprite = i < Health ? FullHeart : EmptyHeart;
        }

        if (!IsAlive())
        {
            Movement.enabled = false;
            PlayerCollider.enabled = false;
            PlayerRigidBody.bodyType = RigidbodyType2D.Kinematic;
            PlayerRigidBody.velocity = Vector2.zero;
            PlayerAnimator.SetTrigger("Die");
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    void WakeUpUI()
    {
        if (FadeInCoroutine != null)
            StopCoroutine(FadeInCoroutine);
        FadeInCoroutine = StartCoroutine(FadeIn());

        if (FadeOutCoroutine != null)
            StopCoroutine(FadeOutCoroutine);
        FadeOutCoroutine = StartCoroutine(FadeOut());
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage; WakeUpUI();
        PlayerAnimator.SetTrigger("Hit");
    }
    public void Heal(int Amount) { Health += Amount; WakeUpUI(); }
    public bool IsAlive() { return Health > 0; }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((EnemyLayers | 1 << other.gameObject.layer) == EnemyLayers)
        {
            TakeDamage(1);
        }
    }

    IEnumerator FadeIn()
    {
        float t = 0;
        float Start = HealthUI.alpha;
        while (t < FadeInDuration)
        {
            HealthUI.alpha = Mathf.Lerp(Start, 1f, t / FadeInDuration);
            t += Time.deltaTime;
            yield return null;
        }
        HealthUI.alpha = 1f;
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(DisplayDuration);
        float t = 0;
        float Start = HealthUI.alpha;
        while (t < FadeOutDuration)
        {
            HealthUI.alpha = Mathf.Lerp(Start, 0f, t / FadeOutDuration);
            t += Time.deltaTime;
            yield return null;
        }
        HealthUI.alpha = 0f;
    }
}
