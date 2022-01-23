using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 画面にあるテキストの文字を変更したい
public class TypingManager : MonoBehaviour
{
    // 画面にあるテキストを持ってくる
    [SerializeField] Text qText; // 問題用のテキスト
    [SerializeField] Text aText; // 答え用のテキスト
    [SerializeField] Text cText; // 答え用のテキスト
    [SerializeField] Text mText; // 答え用のテキスト

    private AudioSource gameAudio;
    [SerializeField] AudioClip typingSE;
    [SerializeField] AudioClip collectSE;
    [SerializeField] AudioClip missSE;

    // テキストデータを読み込む
    [SerializeField] TextAsset _question;
    [SerializeField] TextAsset _answer;

    //テキストデータを格納するためのリスト
    private List<string> _qList = new List<string>();
    private List<string> _aList = new List<string>();

    // 何番目かを指定するstring
    private string _qString;
    private string _aString;

    // 何番目の問題か
    private int _qNum;

    // 問題の何文字目か
    private int _aNum;

    private int collectPoint;
    private int missPoint;

    [SerializeField] Text timerText;
    private float timer;
    private float timeDelta;
    private float delta = 1.0f;

    [SerializeField] Text brainText;

    // ゲームを始めたときに一度だけ呼ばれるもの
    void Start()
    {
        getAudio();
        // テキストデータをリストに入れる
        SetList();
        // 問題を出す
        OutPut();
    }

    void getAudio(){
        gameAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();

        cText.text = collectPoint.ToString();
        mText.text = missPoint.ToString();
        if(Input.GetKeyDown(_aString[_aNum].ToString())){
            // 正解
            gameAudio.PlayOneShot(typingSE);
            Correct();

            // 最後の文字まで正解したら
            if(_aNum >= _aString.Length){
                // 問題を変える
                collectPoint++;
                review();
                gameAudio.PlayOneShot(collectSE);
                OutPut();
            }
        }else if(Input.anyKeyDown){
            // 間違い
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

    // 問題を出すための関数
    void OutPut(){
        // 0番目の文字に戻す
        _aNum = 0;
        
        // _qNumに0～問題の数までの数字をランダムに入れる
        _qNum = Random.Range(0, _qList.Count);

        _qString = _qList[_qNum];
        _aString = _aList[_qNum];

        // 文字を変更する
        qText.text = _qString;
        aText.text = _aString;
    }

    //正解用の関数
    void Correct() {
        // 正解したときにやりたいこと
        _aNum++;
        aText.text = "<color=#6A6A6A>" + _aString.Substring(0, _aNum) + "</color>" + _aString.Substring(_aNum);
    }
    //間違い用の関数
    void Miss() {
        // 間違えたときにやりたいこと
        missPoint++;
        aText.text = "<color=#6A6A6A>" + _aString.Substring(0, _aNum) + "</color>" + "<color=#FF0000>" +  _aString.Substring(_aNum, 1) + "</color>" + _aString.Substring(_aNum + 1);
    }
}