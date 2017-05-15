﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitingClimbingArea : MonoBehaviour
{
    public GameObject player;
    private GameObject _platformToClimbThrough;
    private GameObject _otherClimbingArea;

    private void OnTriggerExit2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            this.player.GetComponent<Player>().outOfClimbingArea();
            this.player.GetComponent<Player>().setClimbingDirection(ClimbingAreas.ClimbingDirection.NOT_CLIMBING);
            this._platformToClimbThrough.GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine(this.ClimbingDelay());
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void setGameObjects(GameObject inPlatformToClimbThrough, GameObject inOtherClimbingArea)
    {
        this._platformToClimbThrough = inPlatformToClimbThrough;
        this._otherClimbingArea = inOtherClimbingArea;
    }

    IEnumerator ClimbingDelay()
        {
        yield return new WaitForSeconds(1.0f);
        this._otherClimbingArea.GetComponent<BoxCollider2D>().enabled = true;
        }
}