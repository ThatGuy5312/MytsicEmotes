using Fusion;
using GorillaLocomotion.Gameplay;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using System.Linq;
using UnityEngine;

namespace MysticEmotes.Utils
{
    public class RigUtils : MonoBehaviour
    {
        public static Player[] OtherRealtimePlayers => PhotonNetwork.PlayerListOthers;
        public static Player[] AllRealtimePlayers => PhotonNetwork.PlayerList;
        public static VRRig[] VRRigs => GorillaParent.instance.vrrigs.ToArray();
        public static VRRig[] OtherVRRigs => VRRigs.Where(rig => rig != MyOfflineRig).ToArray();
        public static NetworkView MyNetworkView => GorillaTagger.Instance.myVRRig;
        public static VRRig MyOfflineRig => GorillaTagger.Instance.offlineVRRig;
        public static GorillaLocomotion.GTPlayer MyPlayer => GorillaLocomotion.GTPlayer.Instance;
        public static GorillaTagger MyOnlineRig => GorillaTagger.Instance;
        public static PhotonView MyPhotonView => GorillaTagger.Instance.myVRRig.GetView;
        public static Recorder MyRecorder => GorillaTagger.Instance.myRecorder;

        public static Vector3 MyVelocity
        {
            get => MyPlayer.bodyCollider.attachedRigidbody.velocity;
            set => MyPlayer.bodyCollider.attachedRigidbody.velocity = value;
        }
    }
}