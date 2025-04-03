using BepInEx;
using HarmonyLib;
using MysticEmotes.Loading;
using Photon.Pun;
using System.Reflection;
using UnityEngine.XR;
using UnityEngine;
using MysticEmotes.Utils;
using MysticEmotes.Main;
using MysticClient.UISetting;

namespace MysticEmotes
{
    public class Plugin : MonoBehaviour
    {
        public static ControllerInputPoller Controller => ControllerInputPoller.instance;
        public static IInputSystem UserInput => UnityInput.Current;

        private static VRRig Ghost = null;
        float freecamSpeed = 10f;
        Vector3 oldMousePos;
        void Update()
        {
            //AssetLoading.ReloadAssets();

            if (emoting)
            {
                RigUtils.MyOfflineRig.transform.position = AssetLoading.robot.transform.Find("ROOT/Hips/Spine1/Spine2").transform.position - (AssetLoading.robot.transform.Find("ROOT/Hips/Spine1/Spine2").transform.right / 2.5f);
                RigUtils.MyOfflineRig.transform.rotation = Quaternion.Euler(new Vector3(0, AssetLoading.robot.transform.Find("ROOT/Hips/Spine1/Spine2").transform.rotation.eulerAngles.y, 0));
                RigUtils.MyOfflineRig.leftHand.rigTarget.transform.position = AssetLoading.robot.transform.Find("ROOT/Hips/Spine1/Spine2/LeftShoulder/LeftUpperArm/LeftArm/LeftHand").transform.position;
                RigUtils.MyOfflineRig.rightHand.rigTarget.transform.position = AssetLoading.robot.transform.Find("ROOT/Hips/Spine1/Spine2/RightShoulder/RightUpperArm/RightArm/RightHand").transform.position;
                RigUtils.MyOfflineRig.leftHand.rigTarget.transform.rotation = AssetLoading.robot.transform.Find("ROOT/Hips/Spine1/Spine2/LeftShoulder/LeftUpperArm/LeftArm/LeftHand").transform.rotation * Quaternion.Euler(0, 0, 75);
                RigUtils.MyOfflineRig.rightHand.rigTarget.transform.rotation = AssetLoading.robot.transform.Find("ROOT/Hips/Spine1/Spine2/RightShoulder/RightUpperArm/RightArm/RightHand").transform.rotation * Quaternion.Euler(180, 0, -75);
                RigUtils.MyOfflineRig.head.rigTarget.transform.rotation = AssetLoading.robot.transform.Find("ROOT/Hips/Spine1/Spine2/Neck/Head").transform.rotation * Quaternion.Euler(0, 0, 90);
                if (!XRSettings.isDeviceActive)
                {
                    var speed = freecamSpeed * Time.deltaTime;
                    var body = Camera.main.transform;
                    RigUtils.MyOnlineRig.rigidbody.useGravity = false;
                    RigUtils.MyOnlineRig.rigidbody.velocity = Vector3.zero;
                    RigUtils.MyOnlineRig.rigidbody.AddForce(-Physics.gravity * RigUtils.MyPlayer.scale, ForceMode.Acceleration);
                    if (UserInput.GetKey(KeyCode.W)) body.position += body.forward * speed;
                    if (UserInput.GetKey(KeyCode.A)) body.position -= body.right * speed;
                    if (UserInput.GetKey(KeyCode.S)) body.position -= body.forward * speed;
                    if (UserInput.GetKey(KeyCode.D)) body.position += body.right * speed;
                    if (UserInput.GetKey(KeyCode.Space)) body.position += body.up * speed;
                    if (UserInput.GetKey(KeyCode.LeftControl)) body.position -= body.up * speed;
                    if (UserInput.GetMouseButton(1))
                    {
                        var look = UserInput.mousePosition - oldMousePos;
                        body.localEulerAngles += new Vector3(-look.y * .3f, look.x * .3f, 0);
                    }
                    oldMousePos = UserInput.mousePosition;
                }
            }

            if (!RigUtils.MyOfflineRig.enabled)
            {
                if (Ghost == null)
                {
                    Ghost = Instantiate(RigUtils.MyOnlineRig.offlineVRRig, RigUtils.MyOnlineRig.transform.position, RigUtils.MyOnlineRig.transform.rotation);
                    Ghost.headBodyOffset = Vector3.zero;
                    Ghost.enabled = true;
                }
                Ghost.gameObject.SetActive(true);
                Ghost.mainSkin.material = TransparentMaterial(GetChangeColorA(Color.gray, .5f));
                Ghost.headConstraint.transform.position = RigUtils.MyOnlineRig.headCollider.transform.position;
                Ghost.headConstraint.transform.rotation = RigUtils.MyOnlineRig.headCollider.transform.rotation;
                Ghost.rightHandTransform.transform.position = RigUtils.MyOnlineRig.rightHandTransform.position;
                Ghost.rightHandTransform.transform.rotation = RigUtils.MyOnlineRig.rightHandTransform.rotation;
                Ghost.leftHandTransform.transform.position = RigUtils.MyOnlineRig.leftHandTransform.position;
                Ghost.leftHandTransform.transform.rotation = RigUtils.MyOnlineRig.leftHandTransform.rotation;
                Ghost.transform.position = RigUtils.MyOnlineRig.transform.position;
                Ghost.transform.rotation = RigUtils.MyOnlineRig.transform.rotation;
            }
            else Ghost?.gameObject?.SetActive(false);
        }
        static Vector3 prePosition;
        public static bool emoting = false;
        public static void PlayEmote(string emote)
        {
            emoting = true;
            AssetLoading.PlayAudio(emote);
            RigUtils.MyOfflineRig.enabled = false;
            prePosition = RigUtils.MyOnlineRig.transform.position;
            AssetLoading.robot.transform.position = RigUtils.MyOfflineRig.transform.Find("RigAnchor/rig/body").position - new Vector3(0, 1.15f, 0);
            AssetLoading.robot.transform.rotation = RigUtils.MyOfflineRig.transform.Find("RigAnchor/rig/body").rotation;
            UI.instance.animator.Play(emote, 0);
        }

        public static void StopEmoting()
        {
            emoting = false;
            RigUtils.MyOfflineRig.enabled = true;
            RigUtils.MyOnlineRig.transform.position = prePosition;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
            UI.instance.animator.Play("idle", 0);
            AssetLoading.StopAudio();
            if (PhotonNetwork.InRoom)
            {
                RigUtils.MyOnlineRig.myRecorder.SourceType = Photon.Voice.Unity.Recorder.InputSourceType.Microphone;
                RigUtils.MyOnlineRig.myRecorder.RestartRecording();
            }
        }

        public static Material TransparentMaterial(Color color)
        {
            var material = new Material(Shader.Find("Sprites/Default")) { color = color };
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.renderQueue = 3000;
            return material;
        }

        public static Color GetChangeColorA(Color original, float a) => new Color(original.r, original.g, original.b, a);
    }
}
