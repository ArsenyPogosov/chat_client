using ClientV3.Output;
using System.IO;

namespace ClientV3.Authorization
{
    class AccessTokenStorage
    {
        private Logger logger;
        private const string tokenPath = @".\..\..\Data\AccessToken.txt";
        private string AccessToken;

        public AccessTokenStorage(Logger logger)
        {
            this.logger = logger;
            try
            {
                ReadAccessToken();
            }
            catch
            {
                logger.PostException("Can't read AccessToken!");
            }
        }

        private void ReadAccessToken() => AccessToken = File.ReadAllText(tokenPath);

        private void WriteAccessToken() => File.WriteAllText(tokenPath, AccessToken);

        public string ReturnAccessToken()
        {
            return AccessToken;
        }

        public void SetAccesToken(string newToken)
        {
            AccessToken = newToken;
            try
            {
                WriteAccessToken();
            }
            catch
            {
                logger.PostException("Can't write AccessToken!");
            }
        }
    }
}
