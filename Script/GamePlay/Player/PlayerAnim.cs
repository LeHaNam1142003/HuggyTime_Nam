using System.Collections;
using System.Collections.Generic;
using Component;
using Player;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

public class PlayerAnim : MonoBehaviour
{
    public List<StateAnimation> StateAnimations;
    public SkeletonAnimation SkeletonAnimation;
    public PlayerController PlayerController;
    private State previousstate;
    private string animationname;
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
        var currentstate = PlayerController.PlayerState;
        if (previousstate!= currentstate)
        {
            PlayAnim();
        }
        previousstate = currentstate;
    }
    public void PlayAnim()
    {
        var getstate = PlayerController.PlayerState;
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
                            SkeletonAnimation.AnimationState.SetAnimation(0, StateAnimations[i].Animation, false);
                            SkeletonAnimation.AnimationState.Event+= AttackEvent;
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
            PlayerController.KillEnemy(PlayerController.Target);
        }
    }
    }
