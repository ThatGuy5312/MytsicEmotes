using MysticEmotes.Utils;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MysticEmotes.Loading
{
    public class AssetLoading : MonoBehaviour
    {
        public static AssetBundle main;

        public static GameObject assetParent;
        public static GameObject robot;
        public static GameObject rifle;
        public static GameObject pistol;

        static AudioSource audioSource;

        public static Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();

        void Start()
        {
            Debug.Log("[MysticEmotes] Starting asset loading");

            main = LoadMainAsset();
            if (main != null)
            {
                assetParent = Instantiate(main.LoadAsset<GameObject>("GGGBundle")); // this is from a different bundle i made that has emotes
                if (assetParent != null)
                {
                    assetParent.transform.position = new Vector3(0, 0, 0);
                    robot = assetParent.transform.Find("EmoteParent/KyleRobot").gameObject;
                    robot.transform.GetChild(0).gameObject.GetComponent<Renderer>().renderingLayerMask = 0;
                    rifle = assetParent.transform.Find("Guns/Rifle").gameObject;
                    pistol = assetParent.transform.Find("Guns/Pistol").gameObject;
                    audioSource = robot.AddComponent<AudioSource>();
                    LoadAudioDict();
                }
                else Debug.LogError("[MysticEmotes] asset parent is null!");
            }
            else Debug.LogError("[MysticEmotes] main bundle failed to load!");
        }

        public static void ReloadAssets()
        {
            if (main == null || assetParent == null)
            {
                Debug.Log("[MysticEmotes] Starting asset loading");

                main = LoadMainAsset();
                if (main != null)
                {
                    assetParent = Instantiate(main.LoadAsset<GameObject>("GGGBundle")); // this is from a different bundle i made that has emotes
                    if (assetParent != null)
                    {
                        assetParent.transform.position = new Vector3(0, 0, 0);
                        robot = assetParent.transform.Find("EmoteParent/KyleRobot").gameObject;
                        robot.transform.GetChild(0).gameObject.GetComponent<Renderer>().renderingLayerMask = 0;
                        rifle = assetParent.transform.Find("Guns/Rifle").gameObject;
                        pistol = assetParent.transform.Find("Guns/Pistol").gameObject;
                        audioSource = robot.AddComponent<AudioSource>();
                        LoadAudioDict();
                    }
                    else Debug.LogError("[MysticEmotes] asset parent is null!");
                }
                else
                {
                    Debug.LogError("[MysticEmotes] main bundle failed to load! Attempting Reload...");
                }
            }
        }

        static AssetBundle LoadMainAsset() => AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly()?.GetManifestResourceStream("MysticEmotes.Resources.emotes"))?? null;

        static void LoadAudioDict()
        {
            var audioClips = main.LoadAllAssets<AudioClip>();
            foreach (var clip in audioClips)
                if (!audioDict.ContainsKey(clip.name))
                {
                    audioDict.Add(clip.name, clip);
                    Debug.Log("[MysticEmotes] Loaded AudioClip: " + clip.name);
                }
        }

        public static void StopAudio()
        {
            audioSource.Stop();
            audioSource.clip = null;
        }

        public static void PlayAudio(string audioName)
        {
            if (audioDict.ContainsKey(audioName))
            {
                if (CheckSource(ref audioSource))
                {
                    var clip = audioDict[audioName];
                    audioSource.clip = clip;
                    audioSource.loop = true;
                    audioSource.Play();
                    PlayThroughPhoton(clip);
                }
            }
            else
            { // idk why 'i' made this part
                Debug.LogError($"[MysticEmotes] Could not find audio {audioName} in dictionary. playing closest match.");
                var closestMatch = audioDict.Keys
                    .OrderBy(key => LevenshteinDistance(audioName.ToLower(), key.ToLower()))
                    .FirstOrDefault();
                if (!string.IsNullOrEmpty(closestMatch) && CheckSource(ref audioSource))
                {
                    Debug.Log($"[MysticEmotes] Found close match: {closestMatch}. Playing matched audio.");
                    var clip = audioDict[closestMatch];
                    audioSource.clip = clip;
                    audioSource.loop = true;
                    audioSource.Play();
                    PlayThroughPhoton(clip);
                } else Debug.LogError("[MysticEmotes] No close matches found.");
            }
        }

        static int LevenshteinDistance(string s1, string s2) // i cant even change this vro ts is gpt
        {
            int[,] dp = new int[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i <= s1.Length; i++)
                for (int j = 0; j <= s2.Length; j++)
                    if (i == 0) dp[i, j] = j;
                    else if (j == 0) dp[i, j] = i;
                    else dp[i, j] = Mathf.Min(
                        Mathf.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + (s1[i - 1] == s2[j - 1] ? 0 : 1)
                    );
            return dp[s1.Length, s2.Length];
        }

        static void PlayThroughPhoton(AudioClip clip)
        {
            if (PhotonNetwork.InRoom)
            {
                RigUtils.MyRecorder.SourceType = Photon.Voice.Unity.Recorder.InputSourceType.AudioClip;
                RigUtils.MyRecorder.AudioClip = clip;
                RigUtils.MyRecorder.RestartRecording();
            }
        }

        static bool CheckSource(ref AudioSource source)
        {
            if (source.isPlaying)
            {
                StopAudio();
                return true;
            } else return true;
        }
    }
}