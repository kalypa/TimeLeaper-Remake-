using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BossAI : SingleMonobehaviour<BossAI>
{
    public enum BossState
    {
        Ready,
        Flying,
        Atk,
        Cast,
        TP,
        TPAtk
    }
    [SerializeField] private GameObject posParent;
    [SerializeField] private BoxCollider2D atkCollider;
    public float attackRange = 2f;
    public float timeToNextState;
    public List<Transform> randomBossMovePos = new List<Transform>();
    private Animator animator;
    public BossState state = BossState.Ready;
    private bool isTeleport = false;
    private bool isReady = false;
    private int patternIdx = 0;
    private bool isEnabled = true;
    private Vector2 tpPos;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        switch (state)
        {
            case BossState.Ready:
                ReadyState();
                break;
            case BossState.Flying:
                FlyingState();
                break;
            case BossState.Atk:
                AtkState();
                break;
            case BossState.Cast:
                CastState();
                break;
            case BossState.TP:
                TPState();
                break;
            case BossState.TPAtk:
                TPAtkState();
                break;
            default:
                break;
        }
    }

    void ReadyState()
    {
        if(!isReady)
        {
            timeToNextState = 1f;
            isReady = true;
        }

        if (timeToNextState <= 0f)
        {
            state = BossState.Flying;
        }
        else
        {
            timeToNextState -= Time.deltaTime;
        }
    }

    void FlyingState()
    {
        if(patternIdx >= 7)
        {
            patternIdx = 0;
        }
        randomBossMovePos.Add(posParent.transform.GetChild(patternIdx));
        transform.position = Vector2.Lerp(transform.position, randomBossMovePos[patternIdx].position, 5f * Time.deltaTime);
        if(Mathf.Abs(transform.position.x - randomBossMovePos[patternIdx].position.x) <= 0.2f)
        {
            patternIdx++;
            state = BossState.Cast;
        }
    }

    void AtkState()
    {
        atkCollider.enabled = true;
        animator.SetTrigger("isAtk");
    }

    void CastState()
    {
        animator.SetTrigger("isCast");
    }

    void TPState()
    {
        if(isTeleport == false)
        {
            tpPos = new Vector2(Random.Range(-6.5f, 6.5f), 2.5f);
            animator.SetBool("isTP", true);
            timeToNextState = 0.0001f;
            isTeleport = true;
        }

        if (timeToNextState <= 0f)
        {
            state = BossState.TPAtk;
            animator.SetBool("isTP", false);
            isTeleport = false;
        }
        else
        {
            timeToNextState -= Time.deltaTime;
        }
    }

    void TPAtkState()
    {
        atkCollider.enabled = true;
        animator.SetTrigger("isTPAtk");
    }

    public void Invisible()
    {
        isEnabled = !isEnabled;
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void Visible()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
    }
    public void TpBoss()
    {
        transform.position = tpPos;
    }
}
