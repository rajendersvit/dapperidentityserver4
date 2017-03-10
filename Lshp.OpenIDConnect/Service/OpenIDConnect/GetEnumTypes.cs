using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.OpenIDConnect
{
    public static class GetEnumTypes
    {
        public static  AccessTokenType GetClientAccessTokenType(int tokenTypeId)
        {
            AccessTokenType accessTokenType = AccessTokenType.Jwt;
            switch (tokenTypeId)
            {
                case 0:
                    accessTokenType = AccessTokenType.Jwt;
                    break;
                case 1:
                    accessTokenType = AccessTokenType.Reference;
                    break;
            }
            return accessTokenType;
        }

        public static TokenExpiration GetTokenExpiration(int expirationId)
        {
            TokenExpiration accessTokenType = TokenExpiration.Sliding;
            switch (expirationId)
            {
                case 0:
                    accessTokenType = TokenExpiration.Sliding;
                    break;
                case 1:
                    accessTokenType = TokenExpiration.Absolute;
                    break;
            }
            return accessTokenType;
        }
        public static TokenUsage GetTokenUsage(int expirationId)
        {
            TokenUsage accessTokenType = TokenUsage.OneTimeOnly;
            switch (expirationId)
            {
                case 0:
                    accessTokenType = TokenUsage.ReUse;
                    break;
                case 1:
                    accessTokenType = TokenUsage.OneTimeOnly;
                    break;
            }
            return accessTokenType;
        }

        public static string ConvertBoolToString(bool flag)
        {
            return flag ? "Yes" : "No";
        }
        public static bool  ConvertStringToBool(string flag)
        {
            if (string.IsNullOrWhiteSpace(flag)) return false;           
            return flag.ToLower()=="yes" ? true : false ;
        }
    }
}
