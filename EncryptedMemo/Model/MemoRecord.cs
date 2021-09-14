using EncryptUtilLibrary;

namespace EncryptedMemo
{
    public class MemoRecord
    {
        private EncryptUtils encrypter;
        public string Txt { get; set; } = string.Empty;
        private string _pass;

        public MemoRecord(string pass)
        {
            _pass = pass;
            encrypter = new EncryptUtils("EncryptedMemo",_pass);
        }

        public void SaveData()
        {
            Settings.Default.Data = encrypter.AesEncrypt(Txt);
            Settings.Default.Save();
        }

        public void OpenData()
        {
            if (!encrypter.CheckKey())
                encrypter.Pass = _pass;
            Txt = encrypter.AesDecrypt(Settings.Default.Data);
        }

        private void UpdatePass(string pass)
        {
            encrypter.Pass = pass;
        }
    }
}
