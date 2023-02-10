using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    private bool isUsingSkill = false;
    [SerializeField] private float skillCD = 3;
    [SerializeField] private float maxFireDistance = 30;
    private float currentSkillCD;
    private float timeToContinueMove;

    //для детектинга персонажа
    private Ray ray;
    [SerializeField] private LayerMask playerLayer;

    //переменные для стрельбы огненными шарами
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform mouthTrans;

    private void Awake()
    {
        currentSkillCD = Random.Range(skillCD - 2, skillCD + 2);
    }

    public override void Move()
    {
        if (currentSkillCD > 0)
            currentSkillCD -= 0.02f;
        else
        {
            ray = new Ray(transform.position, transform.forward);
            if (Physics.SphereCast(ray, 5, maxFireDistance, playerLayer))
                Fire();
        }

        if (timeToContinueMove > 0)
            timeToContinueMove -= 0.02f;
        else
            isUsingSkill = false;

        if (!isUsingSkill)
            base.Move();
    }


    private void Fire()
    {
        animator.SetTrigger("fire");
        currentSkillCD = Random.Range(skillCD - 2, skillCD + 2);
        timeToContinueMove = 0.5f;
        isUsingSkill = true;
        agent.isStopped = true;
        audioManager.PlaySpit();

        //выпуск снаряда
        Invoke("CreateFireball", 0.5f);
    }

    private void CreateFireball()
    {
        GameObject fireballGO = Instantiate<GameObject>(fireballPrefab);
        fireballGO.transform.position = mouthTrans.position;
        fireballGO.GetComponent<Rigidbody>().velocity = (target.transform.position - mouthTrans.position).normalized * 20;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(new Vector3(transform.position.x + transform.forward.x * maxFireDistance,
    //        transform.position.y + transform.forward.y * maxFireDistance, transform.position.z + transform.forward.z*maxFireDistance), 5);
    //}
}
