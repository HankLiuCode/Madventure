using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public DropSection dropSection;
    public Character player;
    public BGMusic bgMusic;
    public SoundFX soundFx;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // only need only dropSection because the canvas group is referenced by both of the dropSection
        dropSection = GameObject.Find("DropSectionRight").GetComponent<DropSection>();
        player = GameObject.Find("Player").GetComponent<Character>();
        soundFx = GameObject.Find("SoundFX").GetComponent<SoundFX>();
    }


}
