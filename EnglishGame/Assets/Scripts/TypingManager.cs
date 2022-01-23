using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingManager : MonoBehaviour
{
    [SerializeField] Text qText;
    [SerializeField] Text aText;
    [SerializeField] Text cText;
    [SerializeField] Text mText;

    private AudioSource gameAudio;
    [SerializeField] AudioClip typingSE;
    [SerializeField] AudioClip collectSE;
    [SerializeField] AudioClip missSE;

    [SerializeField] TextAsset _question;
    [SerializeField] TextAsset _answer;

    private List<string> _qList = new List<string>();
    private List<string> _aList = new List<string>();

    private string _qString;
    private string _aString;

    private int _qNum;

    private int _aNum;

    private int collectPoint;
    private int missPoint;

    [SerializeField] Text timerText;
    private float timer;
    private float timeDelta;
    private float delta = 1.0f;

    [SerializeField] Text brainText;

    void Start()
    {
        getAudio();
        SetList();
        OutPut();
    }

    void getAudio(){
        gameAudio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        Timer();

        cText.text = collectPoint.ToString();
        mText.text = missPoint.ToString();
        if(Input.GetKeyDown(_aString[_aNum].ToString())){
            gameAudio.PlayOneShot(typingSE);
            Correct();
            if(_aNum >= _aString.Length){
                collectPoint++;
                review();
                gameAudio.PlayOneShot(collectSE);
                OutPut();
            }
        }else if(Input.anyKeyDown){
            gameAudio.PlayOneShot(missSE);
            Miss();
        }
    }

    void review() {
        brainText.text = "単語: " + _aString + "       " + "<color=red>" + "意味: " + _qString + "</color>";
    }

    void Timer() {
        timer += Time.deltaTime;
        if(timer > delta){
            timeDelta++;
            timerText.text = "TIME:" + timeDelta;
            timer = 0;
        }
    }

    void SetList() {

        string[] qArray = _question.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        _qList.AddRange(qArray);

        string[] aArray = _answer.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        _aList.AddRange(aArray);
    }

    void OutPut(){
        _aNum = 0;
        _qNum = Random.Range(0, _qList.Count);

        _qString = _qList[_qNum];
        _aString = _aList[_qNum];

        qText.text = _qString;
        aText.text = _aString;
    }

    void Correct() {
        _aNum++;
        aText.text = "<color=#6A6A6A>" + _aString.Substring(0, _aNum) + "</color>" + _aString.Substring(_aNum);
    }
    void Miss() {
        missPoint++;
        aText.text = "<color=#6A6A6A>" + _aString.Substring(0, _aNum) + "</color>" + "<color=#FF0000>" +  _aString.Substring(_aNum, 1) + "</color>" + _aString.Substring(_aNum + 1);
    }
}