using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void QuestionFinish()
    {
        animator.SetBool("Question", false);
    }

    public void Question()
    {
        animator.SetBool("Question", true);
    }

    public void Talk()
    {
        animator.SetBool("Talking", true);
    }

    public void TalkFinish()
    {
        animator.SetBool("Talking", false);
    }
}
