using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    /// <summary>
    /// 0-stop,
    /// 1-front,
    /// 2-left,
    /// 3-back,
    /// 4-right
    /// </summary>
    private float move;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction">������ � ���������� �� -1 �� 1 �� x � z</param>
    /// <param name="yRot">����������, ���� �������� �������� �� ������ ������ (��-1 �� 1)</param>
    public void AnimateMove(Vector3 direction, float yRot)
    {
        //yRot ����� 0, ���� �������� �������� ����� �� ���������� ��� Z, 0,5 - ����� �� X, -1 � 1 - ����� �� Z, -0,5 - ����� �� �

        //��������� �������� ������������ ��� ����� ������ ���������
        if (direction.x <= 0.05f && direction.x >= -0.05f)
            direction.x = 0;
        if (direction.z <= 0.05f && direction.z >= -0.05f)
            direction.z = 0;


        animator.SetFloat("verticalMove", direction.z);
        animator.SetFloat("horizontalMove", direction.x);
        animator.SetFloat("playerRotation", yRot);
    }

    public void StartFire()
    {
        animator.SetTrigger("fire");
    }

    public void Death()
    {
        animator.SetBool("death", true);
    }
}
