using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    //настройки пастронов
    [SerializeField] private int[] maxBullets;
    [SerializeField] private UIWeapon[] uiWeapons;
    [SerializeField] private int[] addingBullets;
    private int[] currentBullets;

    void Awake()
    {
        Application.targetFrameRate = 60;

        if (S == null)
            S = this;
        //устанавливаем максимальное количество боеприпасов
        currentBullets = new int[maxBullets.Length];
        for (int i = 0; i < maxBullets.Length; i++)
        {
            currentBullets[i] = maxBullets[i];
            uiWeapons[i].RefreshBullets(currentBullets[i]);
        }        
    }
    private void OnEnable()
    {
        EventAggregator.SetWeapon.AddListener(ActiveWeapon);
        EventAggregator.AddBullets.AddListener(AddBullets);
    }
    private void OnDisable()
    {
        EventAggregator.SetWeapon.RemoveListener(ActiveWeapon);
        EventAggregator.AddBullets.RemoveListener(AddBullets);
    }

    public void Fire(int weaponIndex, float cd)
    {
        currentBullets[weaponIndex] -= 1;
        uiWeapons[weaponIndex].Fire(currentBullets[weaponIndex], cd);
    }

    public int GetCurrentBullets(int weaponIndex)
    {
        return currentBullets[weaponIndex];
    }

    public void AddBullets()
    {
        for (int i = 0; i < currentBullets.Length; i++)
        {
            currentBullets[i] += addingBullets[i];
            if (currentBullets[i] > maxBullets[i])
                currentBullets[i] = maxBullets[i];
            uiWeapons[i].RefreshBullets(currentBullets[i]);
        }
    }

    public void ActiveWeapon(int weaponIndex)
    {
        foreach (UIWeapon weapon in uiWeapons)
            weapon.InactiveWeapon();//снимаем отображение всех иконок        
        uiWeapons[weaponIndex]?.ActiveWeapon();
    }
}
