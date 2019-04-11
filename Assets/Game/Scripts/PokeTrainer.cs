using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeTrainer : MonoBehaviour
{
    public LocationServicesController location;
    public HighScoreLeaderboard highScore;

    public PokemonController controller; 

    public int score; 

    void Start()
    {
        score = 0;
    }

    public void CatchEmAll()
    {
        score++;
        LogEvent(controller.pokemon.name);
    }

    private void LogEvent(string pokeName)
    {
         PlayFabClientAPI.WritePlayerEvent(new WriteClientPlayerEventRequest { 
            EventName = "Pokemon_Caught",
            Body = new Dictionary<string, object>()
            {
                { "name", pokeName },
                { "longitude", location.longitudeText },
                { "latitude", location.latitudeText }
            }
        },
        result => Debug.Log("Success"), 
        error => Debug.LogError(error.GenerateErrorReport())); 
    }
}
