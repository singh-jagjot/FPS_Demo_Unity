using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    // public float groundDrag;
    // public float airDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    // [Header("Keybinds")]
    // public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Gun Related")]
    public Transform muzzle;
    public GameObject bulletShell;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public float gunDamage = 10f;
    public float shootRange = 150f;
    public float fireRate = 16f;
    private float nextTimeToFire = 0f;


    [Header("Other")]
    public Transform orientation;
    public GameObject bullet;
    public float gravityScale;
    public float playerMaxHealth = 80f;
    private float playerCurrentHealth;
    public Canvas gameInfo;
    // int bulletCounter = 0;
    // int frameCounter = 0;

    // GameObject[] bullets;
    float horizontalInput;
    float verticalInput;

    private int idOflastEnemyThatGaveDamage = 0;
    Vector3 moveDirection;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        this.playerCurrentHealth = this.playerMaxHealth;
        // bullets = new GameObject[60];
        // muzzle = GameObject.Find("muzzle").transform;
        // for(int i=0;i<60;++i)
        // {
        //     bullets[i] = Instantiate(bullet);
        //     bullets[i].SetActive(false);
        // }
    }


    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.6f + 0.6f, whatIsGround);
        // transform.Rotate(new Vector3(0.0f, -90f, 0.0f), Space.World);
        playerInput();
        // playerSpeedControl();
        // Debug.Log("Grounded: " + grounded);
        // Drag Handling
        // if(grounded)
        //     rb.drag = groundDrag;
        // else
        //     rb.drag = airDrag;
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);//Adding downward gravity force(for fast fall)
        if (grounded)
            movePlayer();

        if (Input.GetAxisRaw("Fire1") == 1 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            muzzleFlash.Play();
            GameObject bs = Instantiate(bulletShell, muzzle.position, Quaternion.identity);
            bs.GetComponent<Rigidbody>().AddForce(bs.transform.forward * 2f, ForceMode.Impulse);
            RaycastHit rHit;
            bool didHit = Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rHit, shootRange);

            if (didHit)
            {
                // Debug.Log("Hitted: " + rHit.transform.name);
                // hit.rigidbody.AddForceAtPosition(transform.forward * impactForce, hit.point);
                EnemyHandler handler = rHit.transform.GetComponentInParent<EnemyHandler>();

                if (handler != null)
                {
                    handler.takeDamage(gunDamage);
                }
            }

            Destroy(bs, 2f);
            // frameCounter++;
            // if(frameCounter == 60 || bulletCounter == 60){
            //     frameCounter = 0;
            //     bulletCounter = 0;            
            // }
            // bullets[bulletCounter].transform.position = muzzle.position;// + new Vector3(0.5f,0.5f,0f);
            // bullets[bulletCounter].transform.Rotate(0f,0f,90f);
            // bullets[bulletCounter].SetActive(true);
            // bullets[bulletCounter].GetComponent<Rigidbody>().velocity = bullets[bulletCounter].transform.forward * 5;
            // bullets[bulletCounter].GetComponent<Rigidbody>().AddForce(bullets[bulletCounter].transform.forward * 10, ForceMode.VelocityChange);
            // bulletCounter++;
            // GameObject newBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);
            // newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * 100;
            // newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 100, ForceMode.Force);
        }
    }

    private void playerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        verticalInput = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        // Debug.Log("Key:"+Input.GetKey(jumpKey)+" R2J:"+readyToJump+" G:"+grounded);
        // if(Input.GetKey(jumpKey) && readyToJump && grounded)
        // {
        //     // Debug.Log("Inside");
        //     readyToJump = false;
        //     jump();
        //     Invoke(nameof(resetJump), jumpCooldown);
        // }
        if (Input.GetAxisRaw("Jump") == 1 && readyToJump && grounded)
        {
            // Debug.Log("Inside");
            readyToJump = false;
            jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }

        // if(Input.GetAxisRaw("Fire1") == 1)
        // {   
        //     frameCounter++;
        //     if(frameCounter == 60 || bulletCounter == 60){
        //         frameCounter = 0;
        //         bulletCounter = 0;            
        //     }
        //     bullets[bulletCounter].transform.position = muzzle.position;// + new Vector3(0.5f,0.5f,0f);
        //     // bullets[bulletCounter].transform.Rotate(0f,0f,90f);
        //     bullets[bulletCounter].SetActive(true);
        //     // bullets[bulletCounter].GetComponent<Rigidbody>().velocity = transform.right * 100;
        //     bullets[bulletCounter].GetComponent<Rigidbody>().AddForce(bullets[bulletCounter].transform.forward * 10, ForceMode.VelocityChange);
        //     // bulletCounter++;
        // }
    }

    private void movePlayer()
    {
        // moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection = orientation.right * verticalInput + orientation.forward * -horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    // One of the good methods to set speed
    private void playerSpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 lmtVelocity = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(lmtVelocity.x, rb.velocity.y, lmtVelocity.z);
        }
    }

    private void jump()
    {
        // Reset y velocity to jump for exactly same height
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce * airMultiplier * 10f, ForceMode.Impulse);
    }

    private void resetJump()
    {
        readyToJump = true;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.transform.CompareTag("Enemy") && this.idOflastEnemyThatGaveDamage != collisionInfo.gameObject.GetInstanceID())
        {
            this.idOflastEnemyThatGaveDamage = collisionInfo.gameObject.GetInstanceID();
            EnemyHandler eh = collisionInfo.transform.GetComponentInParent<EnemyHandler>();
            this.playerCurrentHealth -= eh.hitDamage;
            // Debug.Log("PlayerHealth: " + this.playerCurrentHealth);
            // Debug.Log("EnemyId: " + collisionInfo.gameObject.GetInstanceID());
            // Destroy(collisionInfo.gameObject);// not working ???
            eh.die();
            GameInfo gi = this.gameInfo.GetComponent<GameInfo>();
            gi.updatehealth((this.playerCurrentHealth / this.playerMaxHealth) * 100);
            if (this.playerCurrentHealth <= 0f)
            {
                gi.gameOver();
            }
        }
    }
}
