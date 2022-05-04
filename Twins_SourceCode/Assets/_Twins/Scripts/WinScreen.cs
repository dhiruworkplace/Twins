using UnityEngine;
using TMPro;

public class WinScreen : Title
{
    public Container container;
    public TextMeshProUGUI points;

    private void OnEnable()
    {
        container.playerList.Add("Player" + Random.Range(00000, 99999));

        int remainingTimer = (int)FindObjectOfType<GameManager>().timer;
        container.localPoints += remainingTimer > 0 ? remainingTimer : 0;
        points.text = "+" + container.localPoints;
        container.points += container.localPoints;
        container.localPoints = 0;
    }
}