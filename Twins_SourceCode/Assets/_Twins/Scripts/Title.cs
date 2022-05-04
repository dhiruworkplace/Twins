using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject englishTitle, RussiaTitle;

    // Start is called before the first frame update
    void Start()
    {
        SetTitle();
    }

    private void SetTitle()
    {
        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            englishTitle.SetActive(false);
            RussiaTitle.SetActive(true);
        }
        else
        {
            englishTitle.SetActive(true);
            RussiaTitle.SetActive(false);
        }
    }
}