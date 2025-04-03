using BepInEx;
using MysticEmotes.Loading;
using System;
using System.Collections;
using UnityEngine;

namespace MysticEmotes.Main
{
    public class ButtonInfo_Beta
    {
        public ButtonInfo_Beta(string name, Action method) { this.name = name; this.method = method; }
        public ButtonInfo_Beta() { }
        public string name;
        public Action method;
    }
    public class UI : MonoBehaviour
    {
        public static volatile UI instance;

        public int currentPage = 0;

        public ButtonInfo_Beta[][] pages =
        {
            new ButtonInfo_Beta[]
            {
                new ButtonInfo_Beta("Bim Bam Boom", () => Plugin.PlayEmote("bimbamboom")),
                new ButtonInfo_Beta("Blinding Lights", () => Plugin.PlayEmote("blindinglights")),
                new ButtonInfo_Beta("Boys A Liar", () => Plugin.PlayEmote("boysaliar")),
                new ButtonInfo_Beta("Bust A Move", () => Plugin.PlayEmote("bustamove")),
                new ButtonInfo_Beta("Chicken Wing", () => Plugin.PlayEmote("chickenwing")),
                new ButtonInfo_Beta("Cupids Arrow", () => Plugin.PlayEmote("cupidsarrow")),
                new ButtonInfo_Beta("Dance Monkey", () => Plugin.PlayEmote("dancemonkey")),
                new ButtonInfo_Beta("Dirtbike Challenge", () => Plugin.PlayEmote("dirtbikechallenge")),
            },
            new ButtonInfo_Beta[]
            {
                new ButtonInfo_Beta("Don't Start Now", () => Plugin.PlayEmote("dontstartnow")),
                new ButtonInfo_Beta("Dynamic Shuffle", () => Plugin.PlayEmote("dynamicshuffle")),
                new ButtonInfo_Beta("Feel The Flow", () => Plugin.PlayEmote("feeltheflow")),
                new ButtonInfo_Beta("Gangnam Style", () => Plugin.PlayEmote("gangnamstyle")),
                new ButtonInfo_Beta("Griddy", () => Plugin.PlayEmote("griddy")),
                new ButtonInfo_Beta("Jiggle Jiggle", () => Plugin.PlayEmote("jigglejiggle")),
                new ButtonInfo_Beta("Lunar Party", () => Plugin.PlayEmote("lunarparty")),
                new ButtonInfo_Beta("Macarena", () => Plugin.PlayEmote("macarena")),
            },
            new ButtonInfo_Beta[]
            {
                new ButtonInfo_Beta("Marsh Walk", () => Plugin.PlayEmote("marshwalk")),
                new ButtonInfo_Beta("Maximum Bounce", () => Plugin.PlayEmote("maximumbounce")),
                new ButtonInfo_Beta("Monster Mash", () => Plugin.PlayEmote("monstermash")),
                new ButtonInfo_Beta("Never Gonna", () => Plugin.PlayEmote("nevergonna")),
                new ButtonInfo_Beta("Night Out", () => Plugin.PlayEmote("nightout")),
                new ButtonInfo_Beta("Ninja Style", () => Plugin.PlayEmote("ninjastyle")),
                new ButtonInfo_Beta("Out West", () => Plugin.PlayEmote("outwest")),
                new ButtonInfo_Beta("Poki", () => Plugin.PlayEmote("poki")),
            },
            new ButtonInfo_Beta[]
            {
                new ButtonInfo_Beta("Popular Vibe", () => Plugin.PlayEmote("popularvibe")),
                new ButtonInfo_Beta("Pull Up", () => Plugin.PlayEmote("pullup")),
                new ButtonInfo_Beta("Pump Up The Jam", () => Plugin.PlayEmote("pumpupthejam")),
                new ButtonInfo_Beta("Renegade", () => Plugin.PlayEmote("renegade")),
                new ButtonInfo_Beta("Roller Vibes", () => Plugin.PlayEmote("rollervibes")),
                new ButtonInfo_Beta("Rollie", () => Plugin.PlayEmote("rollie")),
                new ButtonInfo_Beta("Savage", () => Plugin.PlayEmote("savage")),
                new ButtonInfo_Beta("Side Shuffle", () => Plugin.PlayEmote("sideshuffle")),
            },
            new ButtonInfo_Beta[]
            {
                new ButtonInfo_Beta("Starlit", () => Plugin.PlayEmote("starlit")),
                new ButtonInfo_Beta("Toosie Slide", () => Plugin.PlayEmote("toosieslide")),
                new ButtonInfo_Beta("Head Banger", () => Plugin.PlayEmote("headbanger")),
                new ButtonInfo_Beta("Go Mufasa", () => Plugin.PlayEmote("gomufasa")),
                new ButtonInfo_Beta("Goated", () => Plugin.PlayEmote("goated")),
                new ButtonInfo_Beta("Wanna See Me", () => Plugin.PlayEmote("wannaseeme")),
                new ButtonInfo_Beta("Its Dynamite", () => Plugin.PlayEmote("itdynamite")),
                new ButtonInfo_Beta("Real Slim Shady", () => Plugin.PlayEmote("realslimshady")),
            },
            new ButtonInfo_Beta[]
            {
                new ButtonInfo_Beta("Last Forever", () => Plugin.PlayEmote("lastforever")),
                new ButtonInfo_Beta("Frolic", () => Plugin.PlayEmote("frolic")),
                new ButtonInfo_Beta("Hang Loose Celebration", () => Plugin.PlayEmote("hangloosecelebration")),
                new ButtonInfo_Beta("Ask Me", () => Plugin.PlayEmote("askme")),
                new ButtonInfo_Beta("Crazy Boy", () => Plugin.PlayEmote("crazyboy")),
                new ButtonInfo_Beta("Jabba Switchway", () => Plugin.PlayEmote("jabbaswitchway")),
                new ButtonInfo_Beta("Copines", () => Plugin.PlayEmote("copines")),
                new ButtonInfo_Beta("Get Gone", () => Plugin.PlayEmote("getgone")),
            },

            new ButtonInfo_Beta[]
            {
                new ButtonInfo_Beta("Build Up", () => Plugin.PlayEmote("buildup")),
                new ButtonInfo_Beta("Bring It Around", () => Plugin.PlayEmote("bringitaround")),
                new ButtonInfo_Beta("Gloss", () => Plugin.PlayEmote("gloss")),
                new ButtonInfo_Beta("In Da Party", () => Plugin.PlayEmote("indaparty")),
                new ButtonInfo_Beta("The Pollo Dance", () => Plugin.PlayEmote("thepollodance")),
                new ButtonInfo_Beta("Tootsee", () => Plugin.PlayEmote("tootsee")),
                new ButtonInfo_Beta("Triumphant", () => Plugin.PlayEmote("triumphant")),
                new ButtonInfo_Beta("Freedom Wheels", () => Plugin.PlayEmote("freedomwheels")),
            },

            new ButtonInfo_Beta[]
            {
                new ButtonInfo_Beta("Forget Me Not", () => Plugin.PlayEmote("forgetmenot")),
                new ButtonInfo_Beta("Cross Bounce", () => Plugin.PlayEmote("crossbounce")),
                new ButtonInfo_Beta("Hit It", () => Plugin.PlayEmote("hitit")),
                new ButtonInfo_Beta("Made You Look", () => Plugin.PlayEmote("madeyoulook")),
                new ButtonInfo_Beta("Leilte Lmor", () => Plugin.PlayEmote("leiltelmor")),
                new ButtonInfo_Beta("Rushin Around", () => Plugin.PlayEmote("rushinaround")),
                new ButtonInfo_Beta("Pump Me Up", () => Plugin.PlayEmote("pumpmeup")),
                new ButtonInfo_Beta("Everybody Loves Me", () => Plugin.PlayEmote("everybodylovesme")),
            },

            new ButtonInfo_Beta[]
            {
                new ButtonInfo_Beta("Miku Beam", () => Plugin.PlayEmote("mikubeam")),
                new ButtonInfo_Beta("Miku Live", () => Plugin.PlayEmote("mikulive")),
                new ButtonInfo_Beta("Boney Bounce", () => Plugin.PlayEmote("boneybounce")),
                new ButtonInfo_Beta("Rick Dance", () => Plugin.PlayEmote("rickdance")),
                new ButtonInfo_Beta("I'm A Mystery", () => Plugin.PlayEmote("imamystery")),
                new ButtonInfo_Beta("Boogie Down", () => Plugin.PlayEmote("boogiedown")),
                new ButtonInfo_Beta("Lucid Dreams", () => Plugin.PlayEmote("luciddreams")),
                new ButtonInfo_Beta("Jubi Slide", () => Plugin.PlayEmote("jubislide")),
            },

            new ButtonInfo_Beta[]
            {
                new ButtonInfo_Beta("Empty Out Your Pockets", () => Plugin.PlayEmote("emptyout")),
                new ButtonInfo_Beta("Billy Bounce", () => Plugin.PlayEmote("billybounce")),
                new ButtonInfo_Beta("Boogie Bomb", () => Plugin.PlayEmote("boogiebomb")),
                new ButtonInfo_Beta("The Robot", () => Plugin.PlayEmote("therobot")),
                new ButtonInfo_Beta("Zany", () => Plugin.PlayEmote("zany")),
                new ButtonInfo_Beta("Orange Justice", () => Plugin.PlayEmote("orangejustice")),
                new ButtonInfo_Beta("T Pose", () => Plugin.PlayEmote("tpose")),
                new ButtonInfo_Beta("Electro Swing", () => Plugin.PlayEmote("electroswing")),
            },

            new ButtonInfo_Beta[]
            {
                new ButtonInfo_Beta("Koi Dance", () => Plugin.PlayEmote("koidance")),
                new ButtonInfo_Beta("Bye Bye Bye", () => Plugin.PlayEmote("byebyebye")),
            },
        };
        private Texture2D backgroundTexture;

        public Animator animator;

        private Rect uiRect = new Rect(Screen.width / 2.6f, Screen.height / 2.8f, 400, 400);

        void Awake() => instance = this;

        void Start()
        {
            backgroundTexture = new Texture2D(1, 1);
            backgroundTexture.SetPixel(0, 0, new Color(.12f, .12f, .12f, .50f));
            backgroundTexture.Apply();
            animator = AssetLoading.robot.GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (Plugin.UserInput.GetKeyDown(KeyCode.O)) Plugin.StopEmoting();

            var scroll = Plugin.UserInput.mouseScrollDelta.y;
            if (scroll > 0)
                currentPage = Mathf.Clamp(currentPage + 1, 0, pages.Length - 1);
            else if (scroll < 0)
                currentPage = Mathf.Clamp(currentPage - 1, 0, pages.Length - 1);
        }

        void OnGUI()
        {
            if (!Plugin.UserInput.GetKey(KeyCode.B)) return;

            GUI.DrawTexture(uiRect, backgroundTexture);
            GUI.Label(new Rect(uiRect.x + 155, uiRect.y + 5, 100, 30), $"Current Page: {currentPage}");
            DrawButtonWheel();
        }

        void DrawButtonWheel() // gpt
        {
            try
            {
                int[,] positions = {
                    {-1, -1}, {0, -1}, {1, -1},
                    {-1,  0},          {1,  0},
                    {-1,  1}, {0,  1}, {1,  1}
                };

                var buttonSize = 80f;
                var spacing = 10f;
                var center = new Vector2(uiRect.x + uiRect.width / 2, uiRect.y + uiRect.height / 2);
                for (int i = 0; i < pages[currentPage].Length; i++)
                {
                    var position = new Vector2(center.x + positions[i, 0] * (buttonSize + spacing), center.y + positions[i, 1] * (buttonSize + spacing));
                    var buttonRect = new Rect(position.x - buttonSize / 2, position.y - buttonSize / 2, buttonSize, buttonSize);
                    var style = new GUIStyle(GUI.skin.button);
                    style.alignment = TextAnchor.MiddleCenter;
                    style.fontSize = Mathf.Clamp(14 - (pages[currentPage][i].name.Length / 3), 8, 14);
                    style.wordWrap = true;
                    if (GUI.Button(buttonRect, pages[currentPage][i].name, style))
                        pages[currentPage][i].method.Invoke();
                }
            }
            catch { }
        }

        public string RemoveSpaces(string input) => input.Replace(" ", "");
    }
}