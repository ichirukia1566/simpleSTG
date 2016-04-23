﻿using UnityEngine;
using System.Collections;

public enum EnemyType{
    smallEnemy,
    middleEnemy,
    bigEnemy,
    missile
}
public class Enemy : MonoBehaviour {

    public int hp = 1;
    public float speed = 2;
    public int score = 100;
    public EnemyType enemyType = EnemyType.smallEnemy;
    public bool isDeath = false;

    //use for death animation
    public int framePerSecond = 10;
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private float timer = 0;
    //use for hit sprite
    public Sprite hitSprite;
    public float hitTimer = 0.2f;
    private float resetHitTime;

    public Sprite[] hitSprites;


	// Use this for initialization
	void Start () {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        resetHitTime = hitTimer;
        hitTimer = 0;
	}

	// Update is called once per frame
	void Update () {
        if (this.enemyType != EnemyType.missile) {
            this.transform.Translate(Vector3.down * speed*Time.deltaTime);
            if (transform.position.y < -5.5f) {
                Destroy(this.gameObject);
            }
        } else {
            float rate = Random.Range(1f, 2f);
            this.transform.Translate(Vector3.down * speed * Time.deltaTime * rate);
            this.transform.Translate(Vector3.left * speed * 0.1f * Time.deltaTime * (rate < 1.5f ? 1f : -1f));
            if (transform.position.y < -5.5f || transform.position.x < -3f || transform.position.x > 3f) {
                Destroy(this.gameObject);
            }
        }


        //death animation
        if (isDeath) {
            timer += Time.deltaTime;
            int frameIndex = (int)(timer / (1f / framePerSecond));

            if (frameIndex >= sprites.Length) {
                Destroy(this.gameObject);
            }else
                spriteRenderer.sprite = sprites[frameIndex];
        } else {
            if (this.enemyType != EnemyType.missile) {
                if (hitTimer > 0) {
                    hitTimer -= Time.deltaTime;
                    int frameIndex = (int)( (resetHitTime - hitTimer) / (1f / framePerSecond));//
                    frameIndex %= 2;
                    spriteRenderer.sprite = hitSprites[frameIndex];
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Space) && BombManager.instance.count > 0) {
            ToDie();
        }




	}

    public void BeHit() {
            hp--;
            if (hp <= 0) {
                ToDie();
            } else {
                hitTimer = resetHitTime;
            }
    }

    private void ToDie() {
        if (!isDeath) {
            if (this.enemyType != EnemyType.missile) {
                isDeath = true;
                GameManager._instance.score += score;
            }
        }
    }

}
