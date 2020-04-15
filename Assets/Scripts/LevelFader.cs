using UnityEngine;

public class LevelFader : MonoBehaviour
{
    public Animator animator;
    public bool fadedIn = false;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void onFadeComplete()
    {
        fadedIn = true;
    }

    public void onFadeOutComplete()
    {
        fadedIn = false;
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
}
