using UnityEngine;

public class ThemeScreen : MonoBehaviour
{
    [SerializeField] private GameObject buyTheme2;
    public Container container;

    private void OnEnable()
    {
        container.SetPoitText();
        if (container.themeUnlocked > 1)
            buyTheme2.SetActive(false);
    }

    public void BuyTheme2()
    {
        if (container.points >= 100)
        {
            container.points -= 100;
            container.themeUnlocked = 2;
            buyTheme2.SetActive(false);
        }
    }

    public void SelectTheme(int themeNo)
    {
        container.selectedTheme = themeNo;
        container.playScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}