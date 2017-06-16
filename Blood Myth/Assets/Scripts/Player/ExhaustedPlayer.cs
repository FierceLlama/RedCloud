﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustedPlayer : FatigueStateBaseClass
    {
    private Player player;

    public ExhaustedPlayer(Player _player)
        {
        this.player = _player;
        }

    public override void Enter()
        {
        throw new NotImplementedException();
        }

    public override void Update()
        {
        this.player.CheckOnGround();
#if UNITY_EDITOR
        this.player.SetMove(Input.GetAxis("Horizontal"));
        this.player.GetRigidbody().velocity = new Vector2(this.player.GetMove() * this.player.GetSpeed(), this.player.GetRigidbody().velocity.y);
        if (Input.GetKey(KeyCode.LeftShift) && this.player.GetMove() != 0 && !this.player.GetSprinting())
            {
            this.player.SetSprinting(true);
            if (!this.player.GetJumping())
                {
                this.player.SetIHaveChangedState(true);
                }
            }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && this.player.GetMoving())
            {
            this.player.SetSprinting(false);
            if (!this.player.GetJumping())
                {
                this.player.SetIHaveChangedState(true);
                }
            }

        //if (Input.GetKeyDown(KeyCode.Space) && !this.player.GetJumping() && this.player.GetGrounded())
        //    {
        //    this.player.SetJumping(true);
        //    this.player.SetIHaveChangedState(true);
        //    this.player.GetRigidbody().velocity = new Vector2(this.player.GetRigidbody().velocity.x, 20);
        //    }
#endif
        this.player.SpriteDirection();
        //if (!this.player.GetJumping())
        //    {
            if (this.player.GetMove() != 0 && !this.player.GetMoving())
                {
                this.player.SetMoving(true);
                this.player.SetIHaveChangedState(true);
                }
            else if (this.player.GetMove() == 0 && this.player.GetMoving())
                {
                this.player.SetMoving(false);
                this.player.SetIHaveChangedState(true);
                }
            //}

        if (this.player.GetIHaveChangedState())
            {
            /*if (this.player.GetJumping())
                {
                this.player.skeletonAnimation.state.SetAnimation(0, "Jump_Exhausted", false);
                }

            else*/ if (this.player.GetMoving()/* && !this.player.GetJumping()*/)
                {
                //if (this.player.GetSprinting())
                //    {
                //    this.player.skeletonAnimation.state.SetAnimation(0, "Run_Exhausted", true);
                //    }
                //else
                //    {
                    this.player.skeletonAnimation.state.SetAnimation(0, "Walk_Exhausted", true);
                    //}
                }
            else
                {
                this.player.skeletonAnimation.state.SetAnimation(0, "Idle_Exhausted", true);
                }
            this.player.SetIHaveChangedState(false);
            }
        }
    }