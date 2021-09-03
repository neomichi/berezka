namespace Berezka.Data.Model
{
    readonly public struct JsonWebToken
    {
        public  string accessToken { get;  }

        public string token_type { get;  }
        /// <summary>
        /// sec
        /// </summary>
        public int expires_in { get;  }

        public string refreshToken { get;}
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="accesstoken">accesstoken</param>
        /// <param name="refreshtoken">refreshtoken</param>
        /// <param name="expiresin">expiresin</param>
        public JsonWebToken(string accessToken, string refreshToken,int expiresin=0)
        {
            this.token_type = "bearer";
            this.expires_in = expiresin;
            this.refreshToken = refreshToken;
            this.accessToken = accessToken;
        }
    }
}
