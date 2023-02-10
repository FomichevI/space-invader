
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MonsterAudioManager))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float startRunDistance;
    [SerializeField] private float stopRunDistance;
    [SerializeField] private int pointsForKill;

    protected Animator animator;
    protected GameObject target;
    protected NavMeshAgent agent;
    protected MonsterAudioManager audioManager;

    private bool isRunning = false;
    private bool isAlive = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerController.S.gameObject;
        animator = GetComponent<Animator>();
        audioManager = GetComponent<MonsterAudioManager>();

        Walk();
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Move();
        }
    }

    //для перезаписи под контроллер босса
    public virtual void Move()
    {
        agent.destination = target.transform.position;
        agent.isStopped = false;

        if (!isRunning && Mathf.Abs((transform.position - target.transform.position).magnitude) < startRunDistance)
            Run();
        else if (isRunning && Mathf.Abs((transform.position - target.transform.position).magnitude) > stopRunDistance)
            Walk();
    }

    private void Run()
    {
        agent.speed = runSpeed;
        animator.SetInteger("speed", 2);
        isRunning = true;
        audioManager.PlayStartRun();
    } 

    private void Walk()
    {
        agent.speed = moveSpeed;
        animator.SetInteger("speed", 1);
        isRunning = false;
    }

    public void Death()
    {
        if (isAlive)
        {
            agent.isStopped = true;
            animator.SetTrigger("death");
            Invoke("DestroyGO", 2);
            isAlive = false;
            GetComponent<BoxCollider>().enabled = false;
            audioManager.PlayDeath();

            //начисляем очки
            Counter.S.CountUp(pointsForKill);
        }
    }

    private void DestroyGO()
    {
        Destroy(gameObject);
    }

}
