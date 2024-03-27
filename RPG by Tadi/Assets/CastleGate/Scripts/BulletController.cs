using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private enum State
    {
        Idle,
        FireBullet
    }

    private State state = State.Idle;
    private Animator animator;
    private float bulletInitSpeed = 10f;
    private float bulletCurSpeed = 10f;
    private float bulletAccel = 50f;
    private Vector3 fireTargetPosition;
    private System.Action OnFireComplete;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (state == State.FireBullet)
        {
            bulletCurSpeed += bulletAccel * Time.fixedDeltaTime;
            Vector3 direction = (fireTargetPosition - transform.position).normalized;
            float move = bulletCurSpeed * Time.fixedDeltaTime;

            transform.position += direction * move;

            float distance = Vector3.Distance(transform.position, fireTargetPosition);
            bool isArriving = distance < move;

            if (isArriving)
            {
                state = State.Idle;
                transform.position = fireTargetPosition;
                bulletCurSpeed = bulletInitSpeed;
                animator.SetBool("Explosion", true);
            }
        }
    }

    public void OnExplosionAnimationStart(string name)
    {

    }

    public void OnExplosionAnimationComplete(string name)
    {
        // Arrived at Slide Target Position
        GameManager.Ins.Res.ReturnObjectToPool(transform.gameObject);
        animator.SetBool("Explosion", false);
        OnFireComplete?.Invoke();
    }

    public void SetBullet(Datas.BulletType type, Vector3 position, Vector3 fireTargetPosition, System.Action OnFireComplete)
    {
        animator.runtimeAnimatorController = GameManager.Ins.Anim.BulletAnimator[(int)type - 1];

        transform.position = position;
        this.fireTargetPosition = fireTargetPosition;
        //Vector3 dir = (fireTargetPosition - position).normalized;
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        this.OnFireComplete = OnFireComplete;

        state = State.FireBullet;
    }
}
