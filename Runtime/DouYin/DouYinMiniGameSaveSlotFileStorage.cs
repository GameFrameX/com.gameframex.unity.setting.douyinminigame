namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 抖音小游戏存档文件存储后端。基于字节 FileSystemManager 同步接口：用户目录 <c>USER_DATA_PATH</c>（scfile://user，平台侧自动映射 persistentDataPath），两阶段原子写经 WriteFileSync + CopyFileSync + UnlinkSync。
    /// </summary>
    internal sealed class DouYinMiniGameSaveSlotFileStorage : MiniGameSaveSlotFileStorageBackendBase
    {
        protected override string GetPlatformUserDataPath()
        {
            return TTSDK.TTFileSystemManager.USER_DATA_PATH;
        }

        protected override string CombinePath(string left, string right)
        {
            return left + "/" + right;
        }

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Register()
        {
            SaveSlotFileStorageBackendFactory.RegisterChannelBackend(() => new DouYinMiniGameSaveSlotFileStorage());
        }

        private TTSDK.TTFileSystemManager GetFileSystemManager()
        {
            return TTSDK.TTFileSystemManagerWebGL.Instance;
        }

        protected override void WriteFileBytes(string filePath, byte[] contents)
        {
            GetFileSystemManager().WriteFileSync(filePath, contents);
        }

        protected override byte[] ReadFileBytes(string filePath)
        {
            return GetFileSystemManager().ReadFileSync(filePath);
        }

        protected override void CopyFileBytes(string sourceFilePath, string destinationFilePath)
        {
            GetFileSystemManager().CopyFileSync(sourceFilePath, destinationFilePath);
        }

        protected override void DeleteFile(string filePath)
        {
            GetFileSystemManager().UnlinkSync(filePath);
        }

        protected override bool ExistsFile(string filePath)
        {
            return GetFileSystemManager().AccessSync(filePath);
        }

        protected override void CreateDirectoryIfExistsMissing(string directoryPath)
        {
            if (GetFileSystemManager().AccessSync(directoryPath))
            {
                return;
            }

            GetFileSystemManager().MkdirSync(directoryPath, true);
        }

        protected override string[] ListDirectoryEntryNames(string directoryPath)
        {
            return GetFileSystemManager().ReadDirSync(directoryPath);
        }
    }
}
