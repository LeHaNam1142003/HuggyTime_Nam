using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Level;
using nameTag;
using Player;
using UnityEngine;

public class Rock: MonoBehaviour
{
    [SerializeField] private float RockRange;
    private float rock_range
    {
        set => RockRange = value;
        get => RockRange;
    }
    public bool Isfind
    {
        set;
        get;
    }
    void Start()
    {
        Isfind = false;
    }
    void Update()
    {
        FindPlayer();
        FindEnemy();
    }
    private void OnDrawGizmos()
    {
       Gizmos.DrawWireSphere(transform.position,rock_range);
    }
    public  void FindPlayer()
    {
        var findplayer = GameObject.FindGameObjectWithTag(NameTag.Player);
        if (Vector3.Distance(this.transform.position,findplayer.transform.position+new Vector3(0,3,0))<rock_range)
        {
            Isfind = true;
            PlayerController playerController = findplayer.GetComponent<PlayerController>();
            playerController.Die();
            DecreaseEnemy();
            Isfind = false;
        }
    }
    public void FindEnemy()
    {
        var findenemy = GameObject.FindGameObjectWithTag(NameTag.Enemy);
        if (Vector3.Distance(this.transform.position,findenemy.transform.position+new Vector3(0,3,0))<rock_range)
        {
            Isfind = true;
            EnemyBase enemyBase = findenemy.GetComponent<EnemyBase>();
            enemyBase.EnemyDie();
            DecreaseEnemy();
            Isfind = false;
        }
    }
    void DecreaseEnemy()
    {
        if (Isfind!=false)
        {
            LevelManager.Instance.CurrentTarget--; 
        }
    }
}
