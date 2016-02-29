using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using TypingShoot.GameCore;
using TypingShoot.GameCore.GameObjects.Soldiers;
using TypingShoot.GameCore.Helper.RomanConverter;
using TypingShoot.GameCore.TypingData;
using TypingShoot.GameCore.Helper;
using System.Text;
using TypingShoot.GameCore.Helper.Music;
using System.Windows.Media;

namespace TypingShoot
{
    public partial class GameMainWindow : Form
    {
        private static int    PRINT_RESULT_COUNT            = 5;                                //苦手なキーを表示する個数
        private static Point  RESULT_POSITION               = new Point(150, 150);              //リザルトを表示する画面上の座標
        private static Point  EnemyPosition                 = new Point(300, 30);               //敵の座標
        private static Point  PlayerPosition                = new Point(350, 300);              //プレイヤーの座標
        private static double TIMER_INTERVAL                = 30; 　　　　　　　　　　　　　　　//体感上ちょうど良いと判断
        private static int    TYPING_SENTENCE_COUNT         = 8;  　　　　　　　　　　　　　　  //ゲーム中で表示させる文字列の数。
        private static string JP_ROMAN_TABLE_PATH           = makeFilePath( "JpRomanTbl.csv" ); //ローマ字変換表のパス
        private static string GAME_TYPING_SENTENCE_PATH     = makeFilePath( "sentence.csv"   ); //例文ファイルのパス
        private static string GAME_TITLE_IMAGE_PATH         = makeFilePath( "title.jpg"      ); //タイトル画面用画像のパス
        private static string GAME_PAUSE_IMAGE_PATH         = makeFilePath( "pause.png"      ); //ポーズ画面用画像のパス
        private static string GAME_BACKGROUND_IMAGE_PATH    = makeFilePath( "background.png" ); //背景用画像のパス
        private static string GAME_GAMEOVER_IMAGE_PATH      = makeFilePath( "gameover.png"   ); //ゲームオーバーの画像
        private static string GAME_PLAYER_IMAGE_PATH        = makeFilePath( "player.png"     ); //プレイヤー画像のパス
        private static string GAME_GIRL_LEFT_IMAGE_PATH     = makeFilePath( "girlLeft.png"   );
        private static string GAME_GIRL_CENTER_IMAGE_PATH   = makeFilePath( "girlCenter.png" );
        private static string GAME_GIRL_RIGHT_IMAGE_PATH    = makeFilePath( "girlRight.png"  );
        private static string GAME_BGM_PATH                 = makeFilePath( "bgm.mp3"        ); //ＢＧＭのパス
        private static string GAME_BULLET_IMAGE_PATH        = makeFilePath( "hana.png"       );
        private static string GAME_WINDOW_LOGO_PATH         = makeFilePath( "logo.jpg"       );
        private static string GAME_ICON_PATH                = makeFilePath( "icon.ico"       );
        private static string ENCORDING_TYPE                = "Shift_JIS";
        private static int FAKE_OFFSET_X_LEFT               = -50;
        private static int FAKE_OFFSET_X_CENTER             = 0;
        private static int FAKE_OFFSET_X_RIGHT              = 50;
        private static Point GAME_MUZZLE_POSITION_LEFT      = new Point(350, 300);
        private static Point GAME_MUZZLE_POSITION_CENTER    = new Point(400, 300);
        private static Point GAME_MUZZLE_POSITION_RIGHT     = new Point(450,300);



        //リソースの準備
        private Bitmap           GameBackGroundImage = new Bitmap(GAME_BACKGROUND_IMAGE_PATH);
        private JpRomanTable     JpRomanTbl          = JpRomanTable.MakeFromFile(JP_ROMAN_TABLE_PATH, ENCORDING_TYPE);
        private TypingSentence[] TypingSentences     = TypingDataReader.MakeFromCsv(GAME_TYPING_SENTENCE_PATH, ENCORDING_TYPE).ToArray();

        private GameManager Manager;
        private System.Timers.Timer GameTimer;
        private Bitmap CurrentFrame;
        private Enemy _Enemy;
        private MediaPlayer _BGMPlayer;

        private bool IsGameRunning;
        private bool IsPosing;

        private static string makeFilePath(string fileName){
            return Application.StartupPath + @"\resource\" + fileName;
        }

        public GameMainWindow ()
        {
            InitializeComponent();
        }

        private void GameMainWindow_Load (object sender, EventArgs e)
        {
            KeyPreview    = true;
            IsGameRunning = false;
            IsPosing      = false;
            GameTimer     = MakeGameTimer(TIMER_INTERVAL);
            Manager       = GameManager.GetObject();
            pBoxLogo.Image = new Bitmap(GAME_WINDOW_LOGO_PATH);
            Icon = new Icon(GAME_ICON_PATH);

            _BGMPlayer = new MediaPlayer();
            _BGMPlayer.Open(new Uri(GAME_BGM_PATH));
            _BGMPlayer.MediaEnded += OnBgmEnded;

            startButton.Enabled =true;
            quitGameButton.Enabled = false;
            pauseButton.Enabled = false;
            SetTitleImage();

        }

        private Muzzle[] LoadMuzzle()
        {
            var leftImage = new Bitmap(GAME_GIRL_LEFT_IMAGE_PATH);
            var centerImage = new Bitmap(GAME_GIRL_CENTER_IMAGE_PATH);
            var rightImage = new Bitmap(GAME_GIRL_RIGHT_IMAGE_PATH);

            var muzzleLeft   = new Muzzle(leftImage, GAME_MUZZLE_POSITION_LEFT, FAKE_OFFSET_X_LEFT, 0);
            var muzzleCenter = new Muzzle(centerImage, GAME_MUZZLE_POSITION_CENTER, FAKE_OFFSET_X_CENTER, 0);
            var muzzleRight  = new Muzzle(rightImage, GAME_MUZZLE_POSITION_RIGHT, FAKE_OFFSET_X_RIGHT, 0);

            return new Muzzle[] {muzzleLeft,muzzleCenter,muzzleRight};
        }

        private System.Timers.Timer MakeGameTimer (double interval)
        {
            var gameTimer = new System.Timers.Timer();

            gameTimer.Interval = interval;
            gameTimer.Elapsed += new ElapsedEventHandler(TickListener);

            return gameTimer;
        }

        private void TickListener (object source, ElapsedEventArgs e)
        {
            if (Manager.IsGameOver) {
                OnGameOver();
                return;
            }

            var canvas = new Bitmap(gameArea.Width, gameArea.Height);
            var graphi = Graphics.FromImage(canvas);

            //フレームが描き終わるまではポーズさせないようにする
            lock ( GameBackGroundImage ) {
                var tmp = gameArea.Image;

                    graphi.DrawImage(GameBackGroundImage, 0, 0);
                    Manager.UpdateFrame(gameArea.Size, graphi);
                    CurrentFrame = canvas;
                    gameArea.Image = canvas;
                    graphi.Dispose();
                    tmp.Dispose();
            }

        }

        private void OnInputKeyboard (object sender, KeyPressEventArgs e)
        {
            if ( IsGameRunning && IsPosing == false) {
                Manager.AddKeyboardInputChar(e.KeyChar);
            }
        }

        private void OnGameOver()
        {
            GameTimer.Stop();
            IsGameRunning = false;
            IsPosing = false;

            var resultImage = DrawResultImage(PRINT_RESULT_COUNT);
            gameArea.Image = resultImage;
            Manager.ResetGame();
            
        }


        void OnBgmEnded (object sender, EventArgs e)
        {
            _BGMPlayer.Stop();
            _BGMPlayer.Play();
        }

        private void startButton_Click (object sender, EventArgs e)
        {
            //WMPLib.WindowsMediaPlayer mediaPlayer = new WMPLib.WindowsMediaPlayer();
            //mediaPlayer.URL = GAME_BGM_PATH;

            if ( IsGameRunning == false ) {
                var sentences = chooseTypingSentences(TypingSentences, TYPING_SENTENCE_COUNT);
                var enemy     = new Enemy(EnemyPosition,sentences,JpRomanTbl);
                var player = new PlayerGirl(PlayerPosition, enemy, LoadMuzzle(),new Bitmap(GAME_BULLET_IMAGE_PATH));

                Manager.RequestVisible(enemy);
                Manager.RequestVisible(player);
                _BGMPlayer.Play();

                _Enemy = enemy;
                IsPosing      = false;
                IsGameRunning = true;
                quitGameButton.Enabled = true;
                startButton.Enabled = false;
                pauseButton.Enabled = true;
                GameTimer.Start();
            }
        }

        private void pauseButton_Click (object sender, EventArgs e)
        {
            if ( IsGameRunning == false ) {
                return;
            }

            if ( IsPosing ) {
                IsPosing       = false;
                poseLabel.Text = "";
                gameArea.Image = CurrentFrame;
                GameTimer.Start();
            } else {
                IsPosing       = true;
                poseLabel.Text = "POSE";
                SetPauseImage();
                GameTimer.Stop();
            }
        }

        private void quitGameButton_Click (object sender, EventArgs e)
        {
           // if ( IsGameRunning ) {
                GameTimer.Stop();
                Manager.ResetGame();
                IsGameRunning　 = false;
                IsPosing　　　　= false;
                CurrentFrame 　 = new Bitmap(GAME_TITLE_IMAGE_PATH);
                gameArea.Image　= new Bitmap(GAME_TITLE_IMAGE_PATH);
                _BGMPlayer.Stop();
                startButton.Enabled =true;
                quitGameButton.Enabled = false;
                pauseButton.Enabled = false;
            //}
        }

        private TypingSentence[] chooseTypingSentences(TypingSentence[] resource,int sentenseCount)
        {
            var sentences   = new TypingSentence[sentenseCount];

            var randomTable = new RandamNumNotRepeat(resource.Length - 1);

            for (int i = 0; i < sentences.Length; i++) {
                sentences[i] = resource[randomTable.Next()];
            }
            return sentences;
        }

        private void SetTitleImage()
        {
            gameArea.Image = new Bitmap(GAME_TITLE_IMAGE_PATH);
        }

        private void SetPauseImage()
        {
            gameArea.Image = new Bitmap(GAME_PAUSE_IMAGE_PATH);
        }

        private string BuildResultString(int rowCount)
        {
            var result = new StringBuilder();
            var mismatchTable = _Enemy.MismatchCount.ToSortedArray_OrderByDescending();

            result.Append("＜苦手なキー＞\n");

            //ミスしていない場合。
            if (mismatchTable[0].Count <= 0) {
                result.Append("ありません！");
                return result.ToString();
            }

            for (int i = 0; i < rowCount; i++) {
                var row = new StringBuilder();
                var counter = mismatchTable[i];

                //ミスしていない文字ばあい、打ち切り。
                if (counter.Count <= 0) {
                    return result.ToString();
                }


                //　・[a] ・・・ 5回ミス
                row.Append("　・[");
                row.Append(counter.Tag.ToString());
                row.Append("] ・・・ ");
                row.Append(counter.Count.ToString());
                row.Append("　回ミス\n");

                result.Append(row);
            }

            return result.ToString();
        }

        private Bitmap DrawResultImage(int printResultCount, string fontName = "ＭＳ ゴシック",float fontSize = 15.0f)
        {
            var resultImage   = new Bitmap(gameArea.Width, gameArea.Height);
            var resultString = BuildResultString(printResultCount);

            using(Graphics g = Graphics.FromImage(resultImage)){
                TextRenderer.DrawText(g,resultString,new Font(fontName,fontSize),RESULT_POSITION,System.Drawing.Color.Black);
            }

            return resultImage;
        }

        private void targetString_kana_Click(object sender, EventArgs e)
        {

        }

    }
}
