using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    private GameManager manager;
    PlayerStats health;

    [Header("Animation Settings")]
	public Animator anim;
	public AnimationClip attackAnim;
	private float animTime;
	public float attackTime;
	public bool canMove;
    public bool canAttack;

    [Header("Player Attack")]
    public SwordDamage swordDamage;
    public GameObject swordPS;

    [Header("God Settings")]
    [SerializeField]
    float godSpeed = 8.0f;
    public bool godMode;
    

    [Header("Player Speed")]
    [SerializeField]
    float movementSpeed = 4.0f;

    [Header("Dash Settings")]
    public bool canDash;
    public bool dashing;
    public Vector3 moveDirection;
    public float maxDashTime = 1.0f;
    public float dashSpeed = 1.0f;
    public float dashStoppingSpeed = 0.1f;
    private float currentDashTime;
    public float CDDash;
    private float CDDashCount;

    [Header("Audio Settings")]
    private AudioSource audioSource;
    public AudioClip dashAudio;

    [Header("Skill Tree Settings")]
    public bool skillTree = false;

    [Header("Boss")]
    public BossController father;

    private void Awake()
    {
        health = GetComponent<PlayerStats>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start(){
        audioSource = GetComponent<AudioSource>();
        currentDashTime = maxDashTime;
        attackTime = attackAnim.length;
		attackTime *= 0.6f;
		canMove = true;
        canAttack = true;
        canDash = true;
        dashing = false;
        swordPS.SetActive(false);

    }

	void Update(){

        if(godMode){
            GodMode();
            GodMovement();
        }

        /*
        if (Input.GetKeyDown(KeyCode.F8))
        {
            if (skillTree)
            {
                manager.CloseSkillTree();
                skillTree = false;
            }else if (!skillTree)
            {
                manager.OpenSkillTree();
                skillTree = true;
            }
        }
        */

        if(Input.GetKeyDown(KeyCode.F10)){
            if(godMode){
                ActivePlayer();
                godMode = false;
            }else if(!godMode){
                DesactivePlayer();
                godMode = true;
            }
        }

        if (canMove)
		{
            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                anim.SetBool("dash", true);
                CDDashCount = 0.0f;
                canDash = false;
                currentDashTime = 0.0f;
                audioSource.clip = dashAudio;
                audioSource.Play(0);
                dashing = true;
            }
            
            if (Input.GetMouseButtonDown(0) && canAttack)
            {
                StartCoroutine(Attack());
            }

            ControlPlayer(movementSpeed);            
		}

        if (CDDashCount <= CDDash)
        {
            CDDashCount += Time.deltaTime;
        }
        else
        {
            canDash = true;
        }

        if (currentDashTime < maxDashTime)
        {
            moveDirection = new Vector3(0, 0, dashSpeed);
            currentDashTime += dashStoppingSpeed*Time.deltaTime;
            //currentDashTime += dashStoppingSpeed;
        }
        else
        {
            moveDirection = Vector3.zero;
            anim.SetBool("dash", false);
            dashing = false;
        }
        
        transform.Translate(moveDirection* Time.deltaTime, Space.Self);

    }

    IEnumerator Attack(){

        anim.SetBool("attack", true);
        RotatePlayer();
        canMove = false;
        canAttack = false;
        swordPS.SetActive(true);

        yield return new WaitForSeconds(attackTime);

        swordPS.SetActive(false);
        anim.SetBool("attack", false);
		canMove = true;
        canAttack = true;
    }

	void ControlPlayer(float movementSpeed){
		float moveHorizonat = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");
		
		Vector3 movement = new Vector3(moveHorizonat, 0.0f, moveVertical);
		movement.Normalize();
		
		if(movement != Vector3.zero){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.1f);
        }
        
		transform.Translate (movement*movementSpeed*Time.deltaTime, Space.World);

		if(movement != Vector3.zero){
			anim.SetBool("moving", true);
		}else{
			anim.SetBool("moving", false);
		}
	}

	void RotatePlayer()
	{
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
		float rayLength;
        Vector3 pointToLook;
        
		if(groundPlane.Raycast(ray, out rayLength))
		{
			pointToLook = ray.GetPoint(rayLength);
            pointToLook = ray.GetPoint(rayLength);
            Vector3 PoI = new Vector3(pointToLook.x - transform.position.x, 0, pointToLook.z - transform.position.z);
            transform.rotation = Quaternion.LookRotation(PoI, Vector3.up);
        }
	}

    void DesactivePlayer(){
        NavMeshAgent navMesh;
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.enabled = false;
        Rigidbody rb;
        rb = GetComponent<Rigidbody>();
        rb.mass = 0;
        canMove = false;
        anim.enabled = false;
        CapsuleCollider playerCol;
        playerCol = GetComponent<CapsuleCollider>();
        playerCol.enabled = false;
    }

    void ActivePlayer(){
        NavMeshAgent navMesh;
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.enabled = true;
        Rigidbody rb;
        rb = GetComponent<Rigidbody>();
        rb.mass = 1;
        canMove = true;
        anim.enabled = true;
        CapsuleCollider playerCol;
        playerCol = GetComponent<CapsuleCollider>();
        playerCol.enabled = true;
    }

    public void AttackOverlap()
    {
        swordDamage.MyCollision();
    }

    void GodMovement(){
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * Time.deltaTime * godSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(-Vector3.up * Time.deltaTime * godSpeed, Space.World);
        }
        ControlPlayer(godSpeed);
    }

    void GodMode()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.TakeDamage(10f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            health.HealthRecovery(10f);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            health.UseMana(10f);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            health.ManaRecovery(10f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            father.GetDamage(10);
        }

    }


    public void PlayAudio(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }
}
