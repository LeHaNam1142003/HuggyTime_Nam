
using System;
using System.Collections;
using System.Collections.Generic;
using Component;
using Spine.Unity;
using DG.Tweening;
using Enemy;
using nameTag;
using Stick;
using UnityEngine;
using UnityEngine.SocialPlatforms;
namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public State PlayerState;
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
        public bool PlayerCanmove;
        private int distanceraycast = 10;
        private Rigidbody2D playerrb;
        private bool isleft;
        private bool isright;
        private bool finishattack;
        private bool collidetrap;
        private bool collideenemy;
        private GameObject[] findenemy;
        public bool isafraid { set; get; }
        public bool isdead { set; get; }
        private Transform stickposiraycastright
        {
            get =>StickPosiRaycastRight ;
            set => StickPosiRaycastRight = value;
        }
        private Transform stickposiraycastleft
        {
            get =>StickPosiRaycastLeft ;
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
        }
        // Update is called once per frame
        void Update()
        {
            IgnorCollision();
            if (PlayerCanmove!=false)
            {
                DetectEnemy();
            }
        }
        void BoolStart()
        {
            PlayerCanmove = true;
            isleft = true;
            isright = true;
            finishattack = true;
             collideenemy = false;
             collidetrap = false;
        }
        void RaycastPLayer_Right()
        {
            stickhitright = Physics2D.Raycast(stickposiraycastright.position, transform.right, setdistanceraycast, StickLayermask);
            Debug.DrawRay(stickposiraycastright.position,transform.right*setdistanceraycast,Color.blue);
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
            stickhitleft = Physics2D.Raycast(stickposiraycastleft.position, -transform.right, setdistanceraycast, StickLayermask);
            Debug.DrawRay(stickposiraycastleft.position,-transform.right*setdistanceraycast,Color.blue);
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
            PlayerCanmove = false;
            PlayerState = State.Win;
            StartCoroutine(WaitCameraFollow());
        }
        IEnumerator WaitCameraFollow()
        {
            yield return new WaitForSeconds(GameManager.GameManager.Instance.Gettimetocamerafollow);
            GameManager.GameManager.Instance.StartFollow();
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
        public void Die( bool isdead)
        {
            if (isdead!=false)
            {
                PlayerCanmove = false;
                PlayerState = State.Die;
                StopMove();
                StartCoroutine(WaitCameraFollow());
            }
        } 
        void FindEnemyRight()
        {
            enemyhitright = Physics2D.Raycast(enemyposiraycastright.position, transform.right, setdistanceraycast,
                    EnemyLayerMask);
                var setposi = enemyhitright.point.x > 0 ? true : false;
                Debug.DrawRay(enemyposiraycastright.position, transform.right * setdistanceraycast, Color.blue);
                if (enemyhitright&& isright)
                {
                    isleft = false;
                    Flip(enemyhitright.point.x);
                    if (setposi == true)
                    {
                        transform.DOMoveX((enemyhitright.point.x - PlayerInfor.Mindistance), PlayerInfor.Speed)
                            .OnStart((() => Move()));
                    }
                    else
                    {
                        transform.DOMoveX((enemyhitright.point.x + PlayerInfor.Mindistance), PlayerInfor.Speed)
                            .OnStart((() => Move()));
                    }
                }
                else
                {
                    isleft = true;
                }
        }
        void FindEnemyLeft()
        {
            enemyhitleft = Physics2D.Raycast(enemyposieaycastLeft.position, -transform.right, setdistanceraycast,
                    EnemyLayerMask);
                var setposi = enemyhitleft.point.x > 0 ? true : false;
                Debug.DrawRay(enemyposieaycastLeft.position, -transform.right * setdistanceraycast, Color.blue);
                {
                    if (enemyhitleft && isleft)
                    {
                        isright = false;
                        Flip(enemyhitleft.point.x);
                        if (setposi == true)
                        {
                            transform.DOMoveX((enemyhitleft.point.x - PlayerInfor.Mindistance), PlayerInfor.Speed)
                                .OnStart((() => Move()));
                            ;
                        }
                        else
                        {
                            transform.DOMoveX((enemyhitleft.point.x + PlayerInfor.Mindistance), PlayerInfor.Speed)
                                .OnStart((() => Move()));
                            ;
                        }
                    }
                    else
                    {
                        isright = true;
                    }
                }
        }
        private void OnDrawGizmos()=> Gizmos.DrawWireSphere(playergimouz.position,PlayerInfor.Range);
        void DetectEnemy()
        {
            findenemy = GameObject.FindGameObjectsWithTag(NameTag.Enemy);
            foreach (GameObject getobj in findenemy)
            {
                if (Vector3.Distance(this.transform.position,getobj.transform.position)<=PlayerInfor.Range+PlayerInfor.Extra&& Vector3.Distance(this.transform.position,getobj.transform.position)>=PlayerInfor.Range)
                {
                    EnemyController enemyController = getobj.GetComponent<EnemyController>();
                    enemyController.EnemyAfraid();
                    enemyController.See();
                }
                if (Vector3.Distance(this.transform.position,getobj.transform.position)<=PlayerInfor.Range)
                {
                    Target = getobj;
                    Flip(getobj.transform.position.x);
                    Attack2();
                    StopMove();
                }
                else
                {
                    RaycastPLayer_Left();
                    RaycastPLayer_Right();
                }
            }
        }
        public void KillEnemy( GameObject getenemy)
        {
            EnemyController enemyController = getenemy.GetComponent<EnemyController>();
            enemyController.EnemyDie();
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
            if (finishattack!=false)
            {
                PlayerState = State.Attack2;
                StartCoroutine(WaitForWin());
            }
            else
            {
                Win();
            }
        }
        IEnumerator WaitForWin()
        {
            yield return new WaitForSeconds(ConstAnimationPlayer.Attack2);
            finishattack = false;
        }
        private void Flip( float enemy)
        {
            var setflip = this.transform.eulerAngles.y ==180 ?true :false;
            if (setflip!=false)
            {
                if (this.transform.position.x>enemy)
                {
                   return;
                }
                else
                {
                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x , this.transform.eulerAngles.y-180, this.transform.eulerAngles.z);
                }
            }
            else if (setflip!=true)
            {
                if (this.transform.position.x<enemy)
                {
                   return;
                }
                else
                {
                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x , this.transform.eulerAngles.y+180, this.transform.eulerAngles.z);
                }
            }
        }
    }
}

