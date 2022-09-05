using System.Collections;
using System.Collections.Generic;
using Component;
using Enemy;
using Level;
using Player;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

public class PlayerAnim : MonoBehaviour
{
    public List<StateAnimation> StateAnimations;
    public SkeletonAnimation SkeletonAnimation;
    private State previousstate;
    public State Currentstate;
    private string animationname;
    public bool Iskill;
    public bool IsFind;
    public Transform Gettarget;
    [System.Serializable]
    public class StateAnimation
    {
        public string Name;
        public AnimationReferenceAsset Animation;
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (previousstate!= Currentstate)
        {
            PlayAnim();
        }
        previousstate = Currentstate;
    }
    public void PlayAnim()
    {
        var getstate = Currentstate;
        switch (getstate)
        {
            case State.Die:
                animationname = StateAnimPlayer.Die;
                break;
            case State.Attack2:
                animationname = StateAnimPlayer.Attack2;
                break;
            case State.Idle:
                animationname = StateAnimPlayer.Idle;
                break;
            case State.Run:
                animationname = StateAnimPlayer.Run;
                break;
            case State.Yawn:
                animationname = StateAnimPlayer.Yawn;
                break;
            case  State.Win:
                animationname = StateAnimPlayer.Win;
                break;
                ;
        }
        for (int i = 0; i <StateAnimations.Count; i++)
        {
            {
                if (StateAnimations[i].Name==animationname)
                {
                    if (animationname!=StateAnimPlayer.Attack2&& animationname!= StateAnimPlayer.Die)
                    {
                        SkeletonAnimation.AnimationState.SetAnimation(0, StateAnimations[i].Animation, true);
                    }
                    else
                    {
                        if (animationname==StateAnimPlayer.Attack2)
                        {
                            SkeletonAnimation.AnimationState.Event+= AttackEvent;
                            SkeletonAnimation.AnimationState.SetAnimation(0, StateAnimations[i].Animation, false);
                        }
                        else
                        {
                            SkeletonAnimation.AnimationState.SetAnimation(0, StateAnimations[i].Animation, false);
                        }
                    }
                }
            }
        }
    }
    private void AttackEvent(TrackEntry trackentry, Event e)
    { 
        if (e.Data==SkeletonAnimation.Skeleton.Data.FindEvent(PlayerEvent.OnBullet))
        {
            EnemyBase enemyBase = Gettarget.GetComponent<EnemyBase>();
            enemyBase.EnemyDie();
        }
        if (e.Data==SkeletonAnimation.Skeleton.Data.FindEvent(PlayerEvent.EndAttack))
        {
            IsFind = true;
        }
    }
    public void UpDateState( State Getstate)
    {
        Currentstate = Getstate;
    }
    }
