using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 抖音小游戏存储后端裁剪辅助器，防止 IL2CPP 代码裁剪移除存储后端类型。
    /// </summary>
    [Preserve]
    public class GameFrameXSettingDouYinMiniGameCroppingHelper : MonoBehaviour
    {
        [Preserve]
        private void Start()
        {
#if UNITY_WEBGL && ENABLE_DOUYIN_MINI_GAME
            _ = typeof(DouYinMiniGameSettingStorage);
#endif
        }
    }
}
