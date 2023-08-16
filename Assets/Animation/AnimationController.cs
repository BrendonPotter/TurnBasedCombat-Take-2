using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject nextGameObject;
    private bool isAnimating = false;

    private void Start()
    {
        PlayAnimation();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }
    private void PlayAnimation()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isPlaying", true);
        isAnimating = true;

        Invoke("OnAnimationFinished", 4.5f);
    }
    private void OnAnimationFinished()
    {
        nextGameObject.SetActive(true);
        gameObject.SetActive(false); // Disable the GameObject when animation finishes
    }
}
