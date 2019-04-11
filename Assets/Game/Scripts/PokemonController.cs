using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using UnityEngine.UI;
using System; 
using UnityEngine.Networking;

public class PokemonController : MonoBehaviour
{ 
    private Sprite sprite;

    public InputField input;

    public Image img; 

    public Text pokeName;

    public Pokemon pokemon;

    public void UpdatePokemon()
    {
        Pokemon pokemon = getPokemon(input.text);
        StartCoroutine(GetText(pokemon.sprites.front_default));
        pokeName.text = pokemon.name;
    } 

    IEnumerator GetText(string path)
    {
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;

            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(results);
            img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        } 
    }

    private Pokemon getPokemon(string id)
    { 
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
            String.Format("https://pokeapi.co/api/v2/pokemon/{0}/", id)
            );

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();

        return JsonUtility.FromJson<Pokemon>(jsonResponse);
    }

    [Serializable]
    public class Pokemon
    {
        public string name;

        public PokeSprite sprites;
    }

    [Serializable]
    public class PokeSprite
    {
        public string front_default;
    }

}
