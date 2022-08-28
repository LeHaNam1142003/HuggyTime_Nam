using System;
using System.Collections;
using System.Collections.Generic;
using nameTag;
using Player;
using UnityEngine;

public class Rock: MonoBehaviour
{
    private PlayerController playercontroller;
    [SerializeField] private float RockRange;
    private bool isdead = true;
    private float rock_range
    {
        set => RockRange = value;
        get => RockRange;
    }
    void Start()
    {
        playercontroller = GameObject.Find(NameTag.Player).GetComponent<PlayerController>();
    }
    void Update()
    {
        UpdateRock();
    }
    private void OnDrawGizmos()
    {
       Gizmos.DrawWireSphere(transform.position,rock_range);
    }
    public  void UpdateRock()
    {
        var findplayer = GameObject.FindGameObjectWithTag(NameTag.Player);
        if (Vector3.Distance(this.transform.position,findplayer.transform.position+new Vector3(0,3,0))<rock_range)
        {
            if (isdead!=false)
            {
                playercontroller.Die(isdead);
                isdead = false;
            }
        }
    }
}
