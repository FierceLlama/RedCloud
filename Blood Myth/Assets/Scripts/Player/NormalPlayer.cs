﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlayer : FatigueStateBaseClass
    {
    private Player player;

    public NormalPlayer(Player _player)
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

#if UNITY_ANDROID
        if (/*this.player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Right && this.player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary*/
            this.player.GetMovingRight())
        {
            this.player.SetMove(1.0f);
        }
        else if (/*this.player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Left && this.player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary*/
                this.player.GetMovingLeft())
        {
            this.player.SetMove(-1.0f);
        }
        else
        {
            this.player.SetMove(0.0f);
        }
        this.player.GetRigidbody().velocity = new Vector2(this.player.GetMove() * this.player.GetSpeed(), this.player.GetRigidbody().velocity.y);

        if ((this.player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Right || this.player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Left)
                && this.player.getPrimaryTouch().getTouchTapCount() >= 2 && this.player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary && !this.player.GetSprinting())
            {
            this.player.SetSprinting(true);
            if (!this.player.GetJumping())
                {
                this.player.SetIHaveChangedState(true);
                }
            }
        // Need to find a way to kill sprint...but this should be a non issue when using the new UI
        else if (this.player.getPrimaryTouch().getTouchTapCount() == 0)
            {
            this.player.SetSprinting(false);
            if (!this.player.GetJumping())
                {
                this.player.SetIHaveChangedState(true);
                }
            }

        if ((this.player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Bottom || this.player.getSecondaryTouch().CurrentScreenSection == ScreenSection.Bottom)
            && !this.player.fatigueForJumping()
            && this.player.GetGrounded())
            {
            this.player.SetJumping(true);
            this.player.SetIHaveChangedState(true);
            this.player.GetRigidbody().velocity = new Vector2(this.player.GetRigidbody().velocity.x, this.player.jumpVelocity);
            }

#endif

#if UNITY_EDITOR
        this.player.SetMove(Input.GetAxis("Horizontal"));
        //if (/*this.player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Right && this.player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary*/
        //    this.player.GetMovingRight())
        //    {
        //    this.player.SetMove(1.0f);
        //    }
        //else if (/*this.player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Left && this.player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary*/
        //        this.player.GetMovingLeft())
        //    {
        //    this.player.SetMove(-1.0f);
        //    }
        //else
        //    {
        //    this.player.SetMove(0.0f);
        //    }
        this.player.GetRigidbody().velocity = new Vector2(this.player.GetMove() * this.player.GetSpeed(), this.player.GetRigidbody().velocity.y);
        if (Input.GetKey(KeyCode.LeftShift) && this.player.GetMove() != 0 && !this.player.GetSprinting())
            {
            this.player.SetSprinting(true);
            if (!this.player.GetJumping())
                {
                this.player.SetIHaveChangedState(true);
                }
            }
        else if (!Input.GetKey(KeyCode.LeftShift) && this.player.GetSprinting())
            {
            this.player.SetSprinting(false);
            if (!this.player.GetJumping())
                {
                this.player.SetIHaveChangedState(true);
                }
            }

        if (Input.GetKeyDown(KeyCode.Space) && !this.player.GetJumping() && this.player.GetGrounded())
            {
            this.player.SetJumping(true);
            this.player.SetIHaveChangedState(true);
            this.player.GetRigidbody().velocity = new Vector2(this.player.GetRigidbody().velocity.x, this.player.jumpVelocity);
            }
#endif

        this.player.SpriteDirection();
        if (!this.player.GetJumping())
            {
            if (this.player.GetMove() != 0 && !this.player.GetMoving())
                {
                this.player.SetMoving(true);
                this.player.SetIHaveChangedState(true);
                }
            else if (this.player.GetMove() == 0 && this.player.GetMoving())
                {
                this.player.SetMoving(false);
                this.player.SetSprinting(false);
                this.player.SetIHaveChangedState(true);
                }
            }

        if (this.player.GetIHaveChangedState())
            {
            if (this.player.GetJumping())
                {
                this.player.skeletonAnimation.state.SetAnimation(0, "Jump", false);
                this.player.LowerHydrationForJumping();
                }

            else if (this.player.GetMoving() && !this.player.GetJumping())
                {
                if (this.player.GetSprinting())
                    {
                    this.player.skeletonAnimation.state.SetAnimation(0, "Sprint", true);
                    this.player.SetSpeed(this.player.sprintSpeed);
                    }
                else
                    {
                    this.player.skeletonAnimation.state.SetAnimation(0, "Run", true);
                    this.player.SetSpeed(this.player.normalSpeed);
                    }
                }
            else
                {
                this.player.skeletonAnimation.state.SetAnimation(0, "Idle", true);
                }
            this.player.SetIHaveChangedState(false);
            }

        if (this.player.GetSprinting())
            {
            this.player.LowerHydrationForSprinting();
            }
        }
    }