using UnityEngine;
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

    public float missileType;
    public float circleGrowSpeed = 0.1f;
    public float circleSize = 1f;
    public float phase;
    public bool isLineMissileSpawnZone = false;
    public bool isSpiralMissileSpawnZone = false;
	// Use this for initialization
	void Start () {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        resetHitTime = hitTimer;
        hitTimer = 0;
        missileType = Random.Range(0f, 3f);
        phase = Random.Range(0f, 6.28f);
        if (this.transform.position.y > 2.2f) {
            isLineMissileSpawnZone = true;
        } else if (this.transform.position.y < 2.2f && this.transform.position.y > -2.2f) {
            isSpiralMissileSpawnZone = true;
        }
	}

	// Update is called once per frame
	void Update () {
        if (this.enemyType != EnemyType.missile) { // a normal enemy
            this.transform.Translate(Vector3.down * speed*Time.deltaTime);
            if (transform.position.y < -5.5f) {
                Destroy(this.gameObject);
            }
        } else { // a missile

            if (missileType >= 0f && missileType < 2.5f && isLineMissileSpawnZone) {
                float rate = Random.Range(1f, 2f);
                this.transform.Translate(Vector3.down * speed * Time.deltaTime * rate);
                this.transform.Translate(Vector3.left * speed * 0.5f * Time.deltaTime * (rate < 1.5f ? 1f : -1f));
            } else if (missileType >= 2.5f && isSpiralMissileSpawnZone) {
                this.transform.Translate(Vector3.right * Mathf.Sin((Time.time) * speed + phase) * 1f * circleSize * Time.deltaTime);
                this.transform.Translate(Vector3.down * Mathf.Cos((Time.time) * speed + phase) * 1f * circleSize * Time.deltaTime);
                circleSize += 0.05f * circleGrowSpeed;
            } else {
                Destroy(this.gameObject);
            }


            if (transform.position.y < -5.5f || transform.position.x < -2.2f || transform.position.x > 2.2f) {
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
