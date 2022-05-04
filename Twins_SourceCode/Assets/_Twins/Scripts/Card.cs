using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image faceDownImage;
    public Image faceCard;
    public bool faceDown = true;

    public void OnClick()
    {
        if (faceDown)
        {
            if (GameManager.clickCard == null)
                return;
            GameManager.clickCard?.Invoke(this);
            faceCard.gameObject.SetActive(true);
            faceDown = false;
        }
    }

    public void FaceDownCard()
    {
        faceDown = true;
        faceCard.gameObject.SetActive(false);
    }
}