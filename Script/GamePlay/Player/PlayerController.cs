
using System;
using System.Collections;
using System.Collections.Generic;
using Component;
using Spine.Unity;
using DG.Tweening;
using Enemy;
using Level;
using nameTag;
using Stick;
using UnityEngine;
using UnityEngine.SocialPlatforms;
namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public State PlayerState;
        [SerializeField] private PlayerAnim PlayerAnim;
        [SerializeField] PlayerInfor PlayerInfor;
        public GameObject Target;
        private RaycastHit2D stickhitright;
        private RaycastHit2D stickhitleft;
        private RaycastHit2D enemyhitright;
        private RaycastHit2D enemyhitleft;
        public LayerMask StickLayermask;
        public LayerMask EnemyLayerMask;
        [SerializeField] private Transform StickPosiRaycastRight;
        [SerializeField] private Transform EnemyPosiRaycastRight;
        [SerializeField] private Transform EnemyPosiRaycastLeft;
        [SerializeField] private Transform StickPosiRaycastLeft;
        [SerializeField] private Transform PlayerGimouz;
        [SerializeField] private Transform Findenemy;
        public bool PlayerCanmove;
        private int distanceraycast = 10;
        private Rigidbody2D playerrb;
        private bool collidetrap;
        private bool collideenemy;
        private GameObject[] findenemy;
        public bool isafraid { set; get; }
        public bool isdead { set; get; }

        private Transform stickposiraycastright
        {
            get => StickPosiRaycastRight;
            set => StickPosiRaycastRight = value;
        }

        private Transform stickposiraycastleft
        {
            get => StickPosiRaycastLeft;
            set => StickPosiRaycastLeft = value;
        }

        private Transform playergimouz
        {
            get => PlayerGimouz;
            set => PlayerGimouz = value;
        }

        private Transform enemyposiraycastright
        {
            set => EnemyPosiRaycastRight = value;
            get => EnemyPosiRaycastRight;
        }

        private Transform enemyposieaycastLeft
        {
            set => EnemyPosiRaycastLeft = value;
            get => EnemyPosiRaycastLeft;
        }

        private int setdistanceraycast
        {
            set => distanceraycast = value;
            get => distanceraycast;
        }

        void Start()
        {
            Idle();
            BoolStart();
            playerrb = GetComponent<Rigidbody2D>();
            PlayerAnim = GetComponent<PlayerAnim>();
        }
        // Update is called once per frame
        void Update()
        {
            PlayerAnim.UpDateState(PlayerState);
            IgnorCollision();
            if (PlayerCanmove != false)
            {
                DetectEnemy();
            }
        }
        void BoolStart()
        {
            PlayerCanmove = true;
            collideenemy = false;
            collidetrap = false;
        }
        void RaycastPLayer_Right()
        {
            stickhitright = Physics2D.Raycast(stickposiraycastright.position, transform.right, setdistanceraycast,
                StickLayermask);
            Debug.DrawRay(stickposiraycastright.position, transform.right * setdistanceraycast, Color.blue);
            if (stickhitright)
            {
                return;
            }
            else
            {
                FindEnemyRight();
            }
        }
        void RaycastPLayer_Left()
        {
            stickhitleft = Physics2D.Raycast(stickposiraycastleft.position, -transform.right, setdistanceraycast,
                StickLayermask);
            Debug.DrawRay(stickposiraycastleft.position, -transform.right * setdistanceraycast, Color.blue);
            if (stickhitleft)
            {
                return;
            }
            else
            {
                FindEnemyLeft();
            }
        }
        void Win()
        {
            if (PlayerState!= State.Die)
            {
                PlayerCanmove = false;
                PlayerState = State.Win;
            }
        }
        void Move() => PlayerState = State.Run;
        void StopMove()
        {
            DOTween.KillAll(false);
        }

        void Idle()
        {
            PlayerState = State.Idle;
        }
        public void Die()
        {
            if (PlayerState!= State.Win&& PlayerState!= State.Attack2)
            {
                PlayerCanmove = false;
                PlayerState = State.Die;
                StartCoroutine(WaitForLost());
                StopMove();
            }
        }
        IEnumerator WaitForLost()
        {
            yield return new WaitForSeconds(ConstAnimationPlayer.Die);
            GameManager.GameManager.Instance.GameState = GameState.Losing;
        }
        void FindEnemyRight()
        {
            enemyhitright = Physics2D.Raycast(enemyposiraycastright.position, transform.right, setdistanceraycast,
                EnemyLayerMask);
            var setposi = enemyhitright.point.x > 0 ? true : false;
            Debug.DrawRay(enemyposiraycastright.position, transform.right * setdistanceraycast, Color.blue);
            if (enemyhitright)
            {
                var findenemy = enemyhitright.transform;
                Findenemy = findenemy;
                Flip(enemyhitright.point.x);
                
                    if (Mathf.Abs((this.transform.position.x-Findenemy.position.x)) <= PlayerInfor.Range)
                    {
                        Debug.Log("right");
                        Attack2();
                    }
                    else
                    {
                        if (setposi == true)
                        {
                            transform.DOMoveX((enemyhitright.point.x - PlayerInfor.Mindistance), PlayerInfor.Speed)
                                .OnStart((() => Move())).OnComplete((() => Attack2()));
                        }
                        else
                        {
                            transform.DOMoveX((enemyhitright.point.x + PlayerInfor.Mindistance), PlayerInfor.Speed)
                                .OnStart((() => Move())).OnComplete((() => Attack2()));
                        }
                    }
            }
        }
        void FindEnemyLeft()
        {
            enemyhitleft = Physics2D.Raycast(enemyposieaycastLeft.position, -transform.right, setdistanceraycast,
                    EnemyLayerMask);
                var setposi = enemyhitleft.point.x > 0 ? true : false;
                Debug.DrawRay(enemyposieaycastLeft.position, -transform.right * setdistanceraycast, Color.blue);
                {
                    if (enemyhitleft)
                    {
                        var findenemy = enemyhitleft.transform;
                        Findenemy = findenemy;
                        Flip(enemyhitleft.point.x);
                        if (Mathf.Abs((this.transform.position.x-Findenemy.position.x)) <= PlayerInfor.Range)
                        {
                            Debug.Log("left");
                            Attack2();
                        }
                        else
                        {
                            if (setposi == true)
                                {
                                    transform.DOMoveX((enemyhitleft.point.x - PlayerInfor.Mindistance), PlayerInfor.Speed)
                                        .OnStart((() => Move())).OnComplete((() => Attack2()));
                                    ;
                                }
                                else
                                {
                                    transform.DOMoveX((enemyhitleft.point.x + PlayerInfor.Mindistance), PlayerInfor.Speed)
                                        .OnStart((() => Move())).OnComplete((() => Attack2()));
                                    ;
                                }
                        }
                    }
                }
        }
        private void OnDrawGizmos()=> Gizmos.DrawWireSphere(playergimouz.position,PlayerInfor.Range);
        void DetectEnemy()
        {
            if (PlayerAnim.IsFind!=false)
                     { 
                         Findenemy = null;
            }
            if (Findenemy==null)
            {
                PlayerState = State.Idle;
                PlayerAnim.IsFind = false;
                RaycastPLayer_Left();
                RaycastPLayer_Right();
            }
            else
            {
                if (Vector3.Distance(this.transform.position,Findenemy.position)<=PlayerInfor.Range+PlayerInfor.Extra&& Vector3.Distance(this.transform.position,Findenemy.position)>=PlayerInfor.Range)
                {
                    EnemyBase enemyController = Findenemy.GetComponent<EnemyBase>();
                    enemyController.EnemyAfraid();
                    enemyController.See();
                }
            }
        }
        void IgnorCollision()
        {
            if (collideenemy!=false)
            {
                Physics2D.IgnoreLayerCollision(LayerComponent.Enemy,LayerComponent.Player,true);
            }
            if (collidetrap!=false)
            {
                Physics2D.IgnoreLayerCollision(LayerComponent.Rock,LayerComponent.Player,true);
            }
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(NameTag.Enemy))
            {
                collideenemy = true;
            }
            if (col.gameObject.CompareTag(NameTag.Rock))
             { 
                 collidetrap = true;
             }
        }
        private void Attack2()
        {
            PlayerState = State.Attack2;
            PlayerAnim.Gettarget = Findenemy;
            StartCoroutine(WaitUpDateEnemy());
        }

        IEnumerator WaitUpDateEnemy()
        {
            yield return new WaitForSeconds(ConstAnimationPlayer.Attack2);
            LevelManager.Instance.UpDateTarget();
            if (LevelManager.Instance.CurrentTarget<=0)
            {
                Win();
            }
        }
        private void Flip( float enemy)
        {
            var setflip = this.transform.localScale.x >0 ?true :false;
            if (setflip!=false)
            {
                if (this.transform.position.x>enemy)
                {
                    this.transform.localScale = new Vector3(this.transform.localScale.x * -1, 1, 1);
                }
                else
                {
                    return;
                }
            }
            else if (setflip!=true)
            {
                if (this.transform.position.x<enemy)
                {
                    this.transform.localScale = new Vector3(this.transform.localScale.x * -1, 1, 1);
                }
                else
                {
                    return;
                }
            }
        }
    }
}

