using System.Collections;
using System.Collections.Generic;
using Component;
using UnityEngine;
using Spine.Unity;
namespace Enemy
{
    public class SecretaryAnim : MonoBehaviour
    { 
        public List<StateAnimation> StateAnimations;
    public SkeletonAnimation SkeletonAnimation;
    public Secretary_Enemy SecretaryEnemy;
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
        var currentstate = SecretaryEnemy.SecretaryState;
        if (previousstate!= currentstate)
        {
            PlayAnim();
        }
        previousstate = currentstate;
    }
    public void PlayAnim()
    {
        var getstate = SecretaryEnemy.SecretaryState;
        switch (getstate)
        {
            case State.Die:
                animationname = StateAnimEnemy.Die;
                break;
            case State.Idle:
                animationname = StateAnimEnemy.Idle;
                break;
            case State.Afraid:
                animationname = StateAnimEnemy.Afraid;
                break;
        }
        for (int i = 0; i <StateAnimations.Count; i++)
        {
            {
                if (StateAnimations[i].Name==animationname)
                {
                    if (animationname!=StateAnimEnemy.Die)
                    {
                        SkeletonAnimation.AnimationState.SetAnimation(0, StateAnimations[i].Animation, true);
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
}
