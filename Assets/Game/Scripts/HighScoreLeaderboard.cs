using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreLeaderboard : MonoBehaviour
{
    public Button postHighScoreButton;

    PokeTrainer player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PokeTrainer>();
        postHighScoreButton.enabled = false;
    }

    private void FixedUpdate()
    {
        if (PlayfabManager.Instance.state == PlayfabManager.LoginStates.Success)
        {
            postHighScoreButton.enabled = true;
        }
    }

    public void PostHighScore()
    { 
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate() { StatisticName = "Caught Pokemon", Value = player.score}
                }
            },
            OnUpdatePlayerStatisticsResponse,
            OnPlayFabError
        );
    }

    public void OnUpdatePlayerStatisticsResponse(UpdatePlayerStatisticsResult response)
    {
        Debug.Log("User statistics updated");
    }

    public void OnPlayFabError(PlayFabError error)
    {
        Debug.LogError("PlayFab Error: " + error.GenerateErrorReport());
    }

    public void GetLeaderboard()
    {
        PlayFabClientAPI.GetLeaderboard(
            new GetLeaderboardRequest()
            {
                StatisticName = "Caught Pokemon",
                StartPosition = 0,
                MaxResultsCount = 5
            },
            OnLeaderboardResultResponse,
            OnPlayFabError
        );
    }

    public void OnLeaderboardResultResponse(GetLeaderboardResult response)
    {
        string leaderBoardName = (response.Request as GetLeaderboardRequest).StatisticName;
        Debug.Log("Get Leaderboard Completed " + leaderBoardName);

        foreach (PlayerLeaderboardEntry playerDetails in response.Leaderboard)
        {
            Debug.Log(string.Format("Player {0} has Rank {1} with a score of {2}",
                playerDetails.DisplayName,
                playerDetails.Position,
                playerDetails.StatValue
                ));
        }
    }
}
