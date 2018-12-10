using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
	
	public Animator anim;
	public AnimationClip attackAnim;
	private float animTime;
	private float attackTime;
	public bool canMove;
    public BoxCollider sword;
    PlayerHealth health;
    public bool canAttack;
    public bool canSpell;
    public bool canDash;
    public float manaSpell;

    private float spellCount;
    public float dashForce;
    public bool godMode;

    private AudioSource audio;
    public AudioClip attackAudio;

    public AnimationClip spellAnim;

	[SerializeField]
	float movementSpeed = 4.0f;

    public GameObject FireBallPrefab;

    public Vector3 moveDirection;
    public float maxDashTime = 1.0f;
    public float dashSpeed = 1.0f;
    public float dashStoppingSpeed = 0.1f;

    private float currentDashTime;
    public float CDDash;
    private float CDDashCount;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    void Start(){
        audio = GetComponent<AudioSource>();
        currentDashTime = maxDashTime;
        anim = GetComponentInChildren<Animator>();
        attackTime = attackAnim.length;
		attackTime *= 0.6f;
		canMove = true;
        canSpell = true;
        canAttack = true;
        canDash = true;
        sword.enabled = false;
        spellCount = 0.0f;
	}

	void Update(){

        if(godMode){
            GodMode();
            GodMovement();
        }

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
                CDDashCount = 0.0f;
                canDash = false;
                currentDashTime = 0.0f;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && canSpell)
            {
                if (health.currentMana > 0f)
                {
                    health.UseMana(manaSpell);
                    spellCount = 0.0f;
                    health.SpellCD.fillAmount = 0.0f;
                    StartCoroutine(CastSpell());
                }
            }
            
            if (Input.GetAxisRaw("Fire1")>0 && canAttack)
            {
                audio.clip = attackAudio;
                audio.Play(0);
                StartCoroutine(Attack());
            }

            ControlPlayer();            
		}

        if(spellCount < 1.0f)
        {
            spellCount += Time.deltaTime;
            health.SpellCD.fillAmount = spellCount;
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
            currentDashTime += dashStoppingSpeed;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        
        transform.Translate(moveDirection* Time.deltaTime, Space.Self);

    }

    IEnumerator Attack(){

        anim.SetBool("attack", true);
        RotatePlayer();
        canMove = false;
        canSpell = false;
        canAttack = false;
        sword.enabled = true;
        
        yield return new WaitForSeconds(attackTime);

        anim.SetBool("attack", false);
        canSpell = true;
		canMove = true;
        canAttack = true;
        sword.enabled = false;
    }

	void ControlPlayer(){
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
		Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 1.3f, 0));
		float rayLength;
        Vector3 pointToLook;
        
		if(groundPlane.Raycast(ray, out rayLength))
		{
			pointToLook = ray.GetPoint(rayLength);
            //Debug.Log(pointToLook);
            transform.LookAt(pointToLook);
            //Debug.Log(transform.rotation);
		}
	}

    IEnumerator CastSpell()
    {
        RotatePlayer();
        anim.SetBool("spell", true);
        canMove = false;
        canAttack = false;
        canSpell = false;
        Spell1();

        yield return new WaitForSeconds(spellAnim.length);

        anim.SetBool("spell", false);
        canMove = true;
        canSpell = true;
        canAttack = true;
    }

    void Spell1()
    {
        Vector3 SpawnSpellLoc = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        GameObject clone = Instantiate(FireBallPrefab, SpawnSpellLoc, Quaternion.identity);

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
    }

    void GodMovement(){
        ControlPlayer();
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
            health.GetExp(10f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.GetExp(50f);
        }

    }
}
