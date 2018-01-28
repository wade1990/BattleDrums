using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public struct ActionTitle
{
    public ActionType action;
    public string title;
    public ActionTitle(ActionType action, string title)
    {
        this.action = action;
        this.title = title;
    }
}

public struct BeatSymbol
{
    public Beat beat;
    public string symbol;

    public BeatSymbol(Beat beat, string symbol)
    {
        this.beat = beat;
        this.symbol = symbol;
    }
}

public class Karaoke : MonoBehaviour {
    public Color passed = new Color(0f,0f,0f,.5f);
    public Color toDo = new Color(0f, 1f, 0f, 1f);
    public Color failed = new Color(1f, 0.1f, 0.1f, .9f);
    public ActionTitle[] Titles = { new ActionTitle(ActionType.MoveForwards, "Move Forward:"), new ActionTitle(ActionType.MoveBackwards, "Move Backwards:"),  new ActionTitle(ActionType.Stay, "Hold:"), new ActionTitle(ActionType.Action, "Attack:") };
    public BeatSymbol[] Symbols = { new BeatSymbol(Beat.All, "▇"), new BeatSymbol(Beat.None, "▂") };
    
    [SerializeField] GameObject _player;

    private InputHandler _state;
    private Text _textField;
    
	// Use this for initialization
	void Start () {
        _state = _player.GetComponent<InputHandler>();
        _textField = GetComponentInParent<Text>();
        _textField.supportRichText = true;
	}
	
	// Update is called once per frame
	void Update () {
        _textField.text = "";
        foreach(ActionTitle action in Titles)
        {
            Rhythm rhythm = null;
            foreach(Rhythm r in _state.PatternList)
            {
                if (r.Action != action.action)
                    continue;
                rhythm = r;
                if (!rhythm.Broken())
                    break;
            }
            if(rhythm != null)
            {
                _textField.text += action.title + "\n";
                _textField.text += "<color='#" + ColorUtility.ToHtmlStringRGBA(passed) + "'>";
                for (int i = 0; i < rhythm.Pattern.Length; i++)
                {
                    if (i == ((rhythm.GetCurrentBeat()+1) % rhythm.Pattern.Length))
                    {
                        _textField.text += "</color><color='#" + ColorUtility.ToHtmlStringRGBA(rhythm.Broken() && i != 0 ? failed : toDo) + "'>";
                    }

                    for (int j = 0; j < Symbols.Length; j++)
                    {
                        if (Symbols[j].beat == Beat.None || (rhythm.Pattern[i] & Symbols[j].beat) != Beat.None)
                        {
                            _textField.text += Symbols[j].symbol+" ";
                            break;
                        }
                    }
                }
                _textField.text += "</color>\n";
            }
        }
		
	}
}
