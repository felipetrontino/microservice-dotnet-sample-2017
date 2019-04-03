using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Framework.Core.Auth
{
    public class JwtData
    {
        public JwtData()
        {
            Tags = new List<string>();
            Roles = new List<string>();
            CustomClaims = new List<Claim>();
            Audiences = new List<string>();
            
        }

        public JwtData(string jwtEncodedString)
            : this(new JwtSecurityToken(jwtEncodedString).Payload) { }

        public JwtData(JwtPayload payload)
            : this()
        {
        }

        [Keys(JwtRegisteredClaimNames.Sub, ClaimTypes.NameIdentifier)]
        public string Sub { get; set; }

        [Keys(JwtRegisteredClaimNames.Aud)]
        public List<string> Audiences { get; set; }

        [Keys(JwtRegisteredClaimNames.Jti)]
        public string Jti { get; private set; }

        [Keys(JwtRegisteredClaimNames.Iat)]
        public long Iat { get; private set; }

        [Keys(JwtRegisteredClaimNames.Nbf)]
        public long Nbf { get; private set; }

        [Keys(JwtRegisteredClaimNames.Exp)]
        public long Exp { get; private set; }

        [Keys(JwtRegisteredClaimNames.Iss)]
        public string Issuer { get; private set; }

        [Keys(ClaimTypes.Role, ClaimNames.Roles)]
        public List<string> Roles { get; }

        [Keys(ClaimNames.Tags)]
        public List<string> Tags { get; }

        [Keys(ClaimNames.UserName)]
        public string UserName { get; set; }

        public List<Claim> CustomClaims { get; }

        public static class ClaimNames
        {
            public const string Roles = "roles";
            public const string Tags = "tags";
            public const string UserName = "username";
        }

        public class KeysAttribute : Attribute
        {
            public KeysAttribute(params string[] keys)
            {
                Keys = keys;
            }

            public string[] Keys { get; }
        }
    }
}
