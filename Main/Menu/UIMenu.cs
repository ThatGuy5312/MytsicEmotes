using Cinemachine;
using GorillaExtensions;
using static MysticEmotes.Plugin;
using MysticEmotes.Main.Menu;
using MysticEmotes.Utils;
using System.Linq;
using TMPro;
using UnityEngine;
using MysticEmotes.Main;
using MysticEmotes;
using UnityEngine.XR;

namespace MysticClient.UISetting
{
    public class UIMenu : MonoBehaviour
    {

        /*
               
        This script was not originaly made by me it was made by finnpluh on github at https://github.com/finnpluh/TextUITemplate.

        This is a PUBLIC template for people to use so dont go calling me a skid for using a public template.
            
        All credits go towards the person who made it although I did change up the scripts to be better for how I like to code.

         */



        private static GameObject uiParent;

        private static TextMeshPro text;

        public static bool UIEnabled = true;

        private static string UIName = $"Mystic Emotes V1";

        public static int index = 0;

        private static float cooldown;

        public static UIInputs Inputs => UIInputs.instance;

        private void Update()
        {
            if (GorillaTagger.hasInstance)
            {
                if (uiParent.IsNull())
                    text = Interface.Create("UI Menu", ref uiParent, TextAlignmentOptions.TopRight);
                else
                {
                    if (text.renderer.material.shader != Shader.Find("GUI/Text Shader"))
                        text.renderer.material.shader = Shader.Find("GUI/Text Shader");

                    var buttons = UI.instance.pages[UI.instance.currentPage];
                    if (UIEnabled && XRSettings.isDeviceActive)
                    {
                        if (!uiParent.activeSelf) uiParent.SetActive(true);
                        else
                        {
                            #region input tasks

                            if (Inputs.LeftStickDown && Inputs.RightTriggerDown || UserInput.GetKey(KeyCode.UpArrow))
                            {
                                if (Time.time >= cooldown)
                                {
                                    if (index > 0) index--;
                                    else index = buttons.Length - 1;
                                    cooldown = Time.time + .25f;
                                }
                            }

                            if ((Inputs.LeftStickDown && Inputs.RightGripDown) || UserInput.GetKey(KeyCode.DownArrow))
                            {
                                if (Time.time >= cooldown)
                                {
                                    if (index < buttons.Length - 1) index++;
                                    else index = 0;
                                    cooldown = Time.time + .25f;
                                }
                            }

                            if ((Inputs.LeftStickDown && Inputs.RightPrimaryDown) || UserInput.GetKey(KeyCode.Return))
                            {
                                if (Time.time >= cooldown)
                                {
                                    buttons[index].method.Invoke();
                                    cooldown = Time.time + .25f;
                                }
                            }

                            if (Inputs.RightStickAxis.x < -.5f || (UserInput.GetKey(KeyCode.LeftArrow)))
                            {
                                if (Time.time >= cooldown)
                                {
                                    UI.instance.currentPage = Mathf.Clamp(UI.instance.currentPage - 1, 0, UI.instance.pages.Length - 1);
                                    cooldown = Time.time + .25f;
                                }
                            }

                            if (Inputs.RightStickAxis.x > .5f || (UserInput.GetKey(KeyCode.RightArrow)))
                            {
                                if (Time.time >= cooldown)
                                {
                                    UI.instance.currentPage = Mathf.Clamp(UI.instance.currentPage + 1, 0, UI.instance.pages.Length - 1);
                                    cooldown = Time.time + .25f;
                                }
                            }

                            if (Inputs.LeftPrimaryDown) StopEmoting();

                            #endregion
                        }
                    }
                    else if (uiParent.activeSelf) uiParent.SetActive(false);

                    if (Inputs.LeftStickDown && Inputs.RightStickDown || UserInput.GetKeyDown(KeyCode.Tab))
                    {
                        if (Time.time >= cooldown)
                        {
                            UIEnabled = !UIEnabled;
                            cooldown = Time.time + .25f;
                        }
                    }

                    if (UIEnabled)
                    {
                        var display = $"<size=.75>{UIName}</size>\n";
                        for (int i = 0; i < buttons.Length; i++)
                        {
                            display += $"{((i == index) ? "->" : "")}{buttons[i].name} ";
                            display += "\n";
                        }
                        text.text = display;
                    }
                    else text.text = "";

                    uiParent.transform.position = RigUtils.MyOnlineRig.headCollider.transform.position + RigUtils.MyOnlineRig.headCollider.transform.forward * 2.75f;
                    uiParent.transform.rotation = RigUtils.MyOnlineRig.headCollider.transform.rotation;
                }
            }
        }

    }
}