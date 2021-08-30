using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public AudioSource footsteps;

    //LEVEL 1
    public float runSpeed;

    Rigidbody rb;
    Animator anim;

    bool facingRight;

    public bool grounded = false;
    public bool firing = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    float moving;
    float moving2;

    bool collidingLeft = false;
    bool collidingRight = false;

    public bool speedUp = false;

    public bool zoomOut;
    public bool tiltUp;
    public bool tiltDown;
    public bool checkpointArea;


    //Levels
    public bool level1 = true;
    public bool level2 = false;
    public bool level3 = false;

    public GameObject loadingScreen;
    public bool loading;

    //LEVEL3
    public bossContainer boss;
    public GameObject centerPoint;

    public GameObject portal2;

    //audio sources
    public GameObject music1;
    public GameObject music2;
    public GameObject music3;

    AudioSource jumpSound;
    AudioSource shootSound;

    public bool bossTouched;
    // Use this for initialization
    void Start () {
        
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        facingRight = true;

        loadingScreen.SetActive(false);
        loading = false;
        bossTouched = false;

        portal2.SetActive(false);

        jumpSound = GameObject.FindGameObjectWithTag("jumpSound").GetComponent<AudioSource>();
        shootSound = GameObject.FindGameObjectWithTag("shootSound").GetComponent<AudioSource>();

        StartCoroutine(waitForLevel1());
    }
	
	// Update is called once per frame
	void Update () {

        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            anim.SetBool("grounded", grounded);
            rb.AddForce(new Vector3(0, jumpHeight, 0));
            jumpSound.Play();
        }

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (groundCollisions.Length > 0)
            grounded = true;
        else
            grounded = false;

        anim.SetBool("grounded", grounded);

        if (level1)
        {
            if (!loading)
            {
                music1.SetActive(true);
                music2.SetActive(false);
                music3.SetActive(false);
            }


            moving = Input.GetAxis("Horizontal");
            anim.SetFloat("speed", Mathf.Abs(moving));

            if ((Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) && firing && grounded)
            {
                moving = 0;
                anim.SetBool("stopToShoot", firing);
            }
            else if ((Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) && !firing && grounded)
            {

                anim.SetBool("stopToShoot", firing);
            }
            //shooting
            if (Input.GetKeyDown(KeyCode.E) && grounded)
            {
                StartCoroutine(attackAnimation());
                anim.SetBool("shooting", firing);
                shootSound.Play();
            }

            if (!firing)
            {
                anim.SetBool("shooting", firing);
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && grounded)
            {
                footsteps.Play();

            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                footsteps.Stop();

            //if(grounded)
            rb.velocity = new Vector3(moving * runSpeed, rb.velocity.y, 0);

            if (moving > 0 && !facingRight)
                Flip();
            else if (moving < 0 && facingRight)
                Flip();
        }


        if (level2)
        {
            // audio sources
            music1.SetActive(false);
            music2.SetActive(true);
            music3.SetActive(false);
            //set speed of the character to level1. so that the running animation can be played
            moving = -1f;
            anim.SetFloat("speed", Mathf.Abs(moving));
            rb.velocity = new Vector3(moving * runSpeed, rb.velocity.y, 0);
            
            //accelerate gradually
            if(runSpeed <4 && !speedUp)
            {
                runSpeed += 0.005f;
            }
            if (speedUp)
            {
                runSpeed += 0.005f;
            }

            //collide with boundaries left and right
            if (!collidingLeft&& !collidingRight)
            {
                var move = new Vector3(0, Input.GetAxis("Jump"), Input.GetAxis("Horizontal"));
                transform.position += move * runSpeed * Time.deltaTime;
            }
            else if (collidingLeft)
            {
                var move = new Vector3(0, Input.GetAxis("Jump"), 0.1f);
                transform.position += move * runSpeed * Time.deltaTime;
            }
            else if (collidingRight)
            {
                var move = new Vector3(0, Input.GetAxis("Jump"), -0.1f);
                transform.position += move * runSpeed * Time.deltaTime;
            }

            jumpHeight =500f;
        }
        if(level3)
        {
            if (!loading && (bossTouched && GameObject.Find("boss Container")))
            {
                music1.SetActive(false);
                music2.SetActive(false);
                music3.SetActive(true);
            }
            else if (!loading && (!bossTouched || GameObject.Find("boss Container") == null))
            {
                music1.SetActive(true);
                music2.SetActive(false);
                music3.SetActive(false);
            }

            moving = Input.GetAxis("Horizontal");
            moving2 = Input.GetAxis("Vertical");
            anim.SetFloat("speed", Mathf.Abs(moving));
            anim.SetFloat("speed3", Mathf.Abs(moving2));

            runSpeed = 1.5f;

            //Rotation Up&Left, Up, Left
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
                transform.rotation = Quaternion.Euler(0, -135, 0);
            else if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow))
                transform.rotation = Quaternion.Euler(0, -90, 0);
            else if (!Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
                transform.rotation = Quaternion.Euler(0, 180, 0);

            //Rotation Up&Right, Right
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
                transform.rotation = Quaternion.Euler(0, -45, 0);
            else if (!Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
                transform.rotation = Quaternion.Euler(0, 0, 0);

            //Rotation Down&Left, Down
            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
                transform.rotation = Quaternion.Euler(0, 135, 0);
            else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
                transform.rotation = Quaternion.Euler(0, 90, 0);

            //Rotation Down&Right
            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
                transform.rotation = Quaternion.Euler(0, 45, 0);


            if (!loading)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    var move = new Vector3(0, Input.GetAxis("Jump"), 3);
                    transform.position += move * runSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    var move = new Vector3(0, Input.GetAxis("Jump"), -3);
                    transform.position += move * runSpeed * Time.deltaTime;

                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    var move = new Vector3(-2, Input.GetAxis("Jump"), 0);
                    transform.position += move * runSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    var move = new Vector3(2, Input.GetAxis("Jump"), 0);
                    transform.position += move * runSpeed * Time.deltaTime;

                }
            }
            if(GameObject.Find("boss Container"))
            {
                portal2.SetActive(false);
            }
            else
                portal2.SetActive(true);
            jumpHeight = 500f;
        }
    }

    void FixedUpdate()
    {
       
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.z *= -1;
        transform.localScale = scale;
    }

    public float GetFacing()
    {
        if (facingRight)
            return 1;
        else
            return -1;
    }

    IEnumerator attackAnimation()
    {
        firing = true;
        moving = 0;
        yield return new WaitForSeconds(0.5f);
        firing = false;
        moving = Input.GetAxis("Horizontal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "level1")
        {
            level1 = true;
            level2 = false;
            level3 = false;
        }

        if(other.tag =="level2")
        {
            level1 = false;
            level2 = true;
            level3 = false;
        }

        if (other.tag == "level3")
        {
            level1 = false;
            level2 = false;
            level3 = true;
        }

        if (other.tag=="speedTrigger")
        {
            speedUp = true;
        }

        if (other.tag == "cameraZoomOut")
        {
            zoomOut = true;
        }

        if (other.tag == "cameraTiltUp")
        {
            tiltUp = true;
        }

        if (other.tag == "cameraTiltDown")
        {
            tiltDown = true;
        }

        if(other.tag =="cameraTrigger")
        {
            checkpointArea = true;
        }
        
        if (other.tag=="gotoL3")
        {
            StartCoroutine(waitForLevel3());
        }

        if (other.tag == "startTrigger")
        {
            bossTouched = true;
        }

        if(other.name== "PortalToL3 (2)" && boss.States==bossContainer.Phases.deathPhase)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="collideLeft")
        {
            collidingLeft = true;
        }
        if (collision.gameObject.tag == "collideRight")
        {
            collidingRight = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "collideLeft")
        {
            collidingLeft = false;
        }
        if (collision.gameObject.tag == "collideRight")
        {
            collidingRight = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "cameraZoomOut")
        {
            zoomOut = false;
        }
        if (other.tag == "cameraTiltUp")
        {
            tiltUp = false;
        }
        if (other.tag == "cameraTiltDown")
        {
            tiltDown = false;
        }
        if (other.tag == "cameraTrigger")
        {
            checkpointArea = false;
        }
    }

    public IEnumerator waitForLevel3()
    {
        transform.position = centerPoint.transform.position;
        loadingScreen.SetActive(true);
        loading = true;
        
        yield return new WaitForSeconds(5f);
        loadingScreen.SetActive(false);
        loading = false;
    }

    public IEnumerator waitForLevel1()
    {
        loadingScreen.SetActive(true);
        loading = true;
        runSpeed = 0;
        yield return new WaitForSeconds(5f);
        loadingScreen.SetActive(false);
        loading = false;
        runSpeed = 2;
    }

}
