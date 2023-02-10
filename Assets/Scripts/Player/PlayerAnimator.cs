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
    /// <param name="direction">Вектор о значениями от -1 до 1 по x и z</param>
    /// <param name="yRot">Определяет, куда повернут персонаж на данный момент (от-1 до 1)</param>
    public void AnimateMove(Vector3 direction, float yRot)
    {
        //yRot равен 0, если персонаж повернут прямо по глобальной оси Z, 0,5 - прямо по X, -1 и 1 - назад по Z, -0,5 - назад по Х

        //отключаем анимацию передвижения при особо низких значениях
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
