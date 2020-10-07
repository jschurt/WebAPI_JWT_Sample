using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebAPI_JWT_Sample.JWT.Model
{
    /// <summary>
    /// Classe com parametros de configuracao do Token. (ver appsettings.json) 
    /// </summary>
    public class TokenSettings
    {
        
        /// <summary>
        /// Chave de criptografia (deve ter no minimo 16 caracteres)
        /// </summary>
        public string Secret { get; set; }
        
        /// <summary>
        /// Horas para o token expirar
        /// </summary>
        public int ExpirationHours { get; set; }
        
        /// <summary>
        /// Emissor do token (aplicacao)
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Em qual Url este token eh valido
        /// </summary>
        public string ValidUrl { get; set; }
    
    } //class

} //namespace
