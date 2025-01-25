using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class lifeText : MonoBehaviour
{
    Player player;
    Text Text;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = player.life.ToString();
    }
}
