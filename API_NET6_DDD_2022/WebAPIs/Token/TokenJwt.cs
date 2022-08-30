using System.IdentityModel.Tokens.Jwt;

namespace WebAPIs.Token
{
    
    public class TokenJwt
    {
        private JwtSecurityToken token;
        internal TokenJwt(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidToTo => token.ValidTo;
        
        public string value => new JwtSecurityTokenHandler().WriteToken(token);
    }
}
