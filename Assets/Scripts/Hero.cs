using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    public bool heroAnimation = true;
    public int frameCountPerSecond = 10;
    public float timer = 0;
    public Sprite[] sprites;
    private SpriteRenderer spriteRender;
    public float superGunTime = 5f;
    private float resetSuperGunTime;

    private bool isMouseDown = false;
    private Vector3 lastMousePosition = Vector3.zero;
    private int gunCount = 1;

    public Gun gunTop;
    public Gun gunLeft;
    public Gun gunRight;


    private float speed = 4; // zjiang
    private Vector2 v; // zjiang
    public float clockTime = 5f; // zjiang
    private float resetClockTime; // zjiang
	// Use this for initialization
	void Start () {
        spriteRender = this.GetComponent<SpriteRenderer>();
        resetSuperGunTime = superGunTime;
        superGunTime = 0;
        gunTop.openFire();
        v = this.GetComponent<Rigidbody2D>().velocity; // zjiang
        resetClockTime = clockTime;
        clockTime = 0;
	}
  
	// Update is called once per frame
	void Update () {
        if (heroAnimation) {
            timer += Time.deltaTime;
            int frameIndex = (int)(timer / (1f / frameCountPerSecond));//the same to static frame++ in Update
            int frame = frameIndex % 2;
            spriteRender.sprite = sprites[frame];
        }

        /*** keyboard ctrl by zjiang***/
        checkPosition2(); // seems that this line and the one after the if's are necessary to make it more smooth to move over the border to the other side
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            v.x = -speed;
            checkPosition2();
            GetComponent<Rigidbody2D>().velocity = v;
            //checkPosition2();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            v.x = 0;
            checkPosition2();
            GetComponent<Rigidbody2D>().velocity = v;
            //checkPosition2();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            v.x = speed;
            checkPosition2();
            GetComponent<Rigidbody2D>().velocity = v;
            //checkPosition2();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            v.x = 0;
            checkPosition2();
            GetComponent<Rigidbody2D>().velocity = v;
            //checkPosition2();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            v.y = speed;
            checkPosition2();
            GetComponent<Rigidbody2D>().velocity = v;
            //checkPosition2();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            v.y = 0;
            checkPosition2();
            GetComponent<Rigidbody2D>().velocity = v;
            //checkPosition2();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            v.y = -speed;
            checkPosition2();
            GetComponent<Rigidbody2D>().velocity = v;
            //checkPosition2();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            v.y = 0;
            checkPosition2();
            GetComponent<Rigidbody2D>().velocity = v;
            //checkPosition2();
        }
        checkPosition2(); // seems necessary
        /*** end of keyboard ctrl***/

        /*** mouse ctrl part ***/
        /*if (Input.GetMouseButtonDown(0)) {
            isMouseDown = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            isMouseDown = false;
            lastMousePosition = Vector3.zero;
        }

        if (isMouseDown&&GameManager._instance.gameState==GameState.Running) {
            if (lastMousePosition != Vector3.zero) {
                //Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
                
                transform.position = transform.position + offset;
                checkPosition();
                
            }

            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }*/
        /*** end of mouse ctrl ***/

        if (superGunTime > 0) {
            superGunTime-=Time.deltaTime;
            if (gunCount == 1) {
                transformToSuperGun();
            }
        } else if (gunCount == 2) {
            transformToNormalGun();
        }
        if (clockTime > 0) {
            clockTime -= Time.deltaTime;
            if (Time.timeScale == 1) {
                Time.timeScale = 0.6f;
            }
        } else if (Time.timeScale == 0.6f) {
            Time.timeScale = 1;
        }
	}

    private void transformToSuperGun() {
        gunCount = 2;
        gunLeft.openFire();
        gunRight.openFire();
        gunTop.stopFire();
    }

    private void transformToNormalGun() {
        gunCount = 1;
        gunLeft.stopFire();
        gunRight.stopFire();
        gunTop.openFire();
    }
    /***
        checkPosition2() by zjiang
        if the ship is over left/right border, it will appear on the other side
        if it is over top/bottom border, it will stop there
    ***/
    private void checkPosition2() { 
        //check x -2.22f~2.22f  y -3.9f~3.4f
        Vector3 pos = transform.position;
        float x = pos.x;
        float y = pos.y;
        x = x < -2.22f? 2.22f : x;
        x = x > 2.22f ? -2.22f : x;
        y = y < -3.9f ? -3.9f : y;
        y = y > 3.4f ? 3.4f : y;

        transform.position = new Vector3(x, y, 0);
    }
    /***
        checkPosition()
        original check position function
        the ship will stop at border
    ***/
    private void checkPosition() {
        //check x -2.22f~2.22f  y -3.9f~3.4f
        Vector3 pos = transform.position;
        float x = pos.x;
        float y = pos.y;
        x = x < -2.22f?-2.22f : x;
        x = x > 2.22f ? 2.22f : x;
        y = y < -3.9f ? -3.9f : y;
        y = y > 3.4f ? 3.4f : y;

        transform.position = new Vector3(x, y, 0);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Award") {
            GetComponent<AudioSource>().Play();
            Award award = other.GetComponent<Award>();
            if (award.type == 0) {
                //chang to double guns
                superGunTime = resetSuperGunTime;
                Destroy(award.gameObject);
            } else if (award.type == 1) {
                BombManager.instance.AddBomb();
                Destroy(award.gameObject);
             
            } else if (award.type == 2) {
                clockTime = resetClockTime;
                Destroy(award.gameObject);
            }
        } else if (other.tag == "Enemy") {
            Destroy(this.gameObject);
            GameOver.instance.Show(GameManager._instance.score);
            BombManager.instance.gameObject.SetActive(false);
        }
    }
    
}
