using UnityEngine;

public class ResultScreen : Title
{
    public Player playerPrefab;
    public Container container;
    public Transform content;

    private void OnEnable()
    {
        GeneratePlayer();       
    }

    private void GeneratePlayer()
    {
        if (content.childCount < container.playerList.Count)
        {
            for (int i = content.childCount; i < container.playerList.Count; i++)
            {
                Player player = Instantiate(playerPrefab, content);
                player.playerNo.text = (i + 1).ToString();
                player.playerName.text = container.playerList[i];
            }
        }
    }
}