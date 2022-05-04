using UnityEngine;
using TMPro;

public class Language : MonoBehaviour
{
    public string englishText;
    public string russiaText;
    private TextMeshProUGUI myText;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        if (Application.systemLanguage == SystemLanguage.Russian)
            myText.text = russiaText;
        else
            myText.text = englishText;
    }
}