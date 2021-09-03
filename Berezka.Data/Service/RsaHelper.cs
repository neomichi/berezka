using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Linq;
using System.Globalization;
using Berezka.Data.Service;

namespace Berezka.Data.Service
{

    public class RsaHelper : ICryptoHelper
    {
        private readonly string pb = "<RSAKeyValue><Modulus>vRrex42UIr8h2zzmimA+k5Rk0c4Evplc1WAM12IN+jVmAk+W/OTejlMe8mhkTpBLuU7Fa/K/ylglFaBPcRtPQlK1iXFk2h/VyXwhafxj75J4U90qbg84/vg4HypnSO6JpSUFeavJQjHFpdsF4S813+2SFCkvHXcIiAn51XfYigc=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private readonly string pk = "<RSAKeyValue><Modulus>vRrex42UIr8h2zzmimA+k5Rk0c4Evplc1WAM12IN+jVmAk+W/OTejlMe8mhkTpBLuU7Fa/K/ylglFaBPcRtPQlK1iXFk2h/VyXwhafxj75J4U90qbg84/vg4HypnSO6JpSUFeavJQjHFpdsF4S813+2SFCkvHXcIiAn51XfYigc=</Modulus><Exponent>AQAB</Exponent><P>4/OKi3lY1+T9/nEEf1p8ePFfkTbWBhGy2qWpggZgPAtbHc5QcYV6VtznHSPKknh0Z+ne/KFfe+4FdMwkwcFq0Q==</P><Q>1F+rdMCGsLVcx94/GGzCvbjkQ+LHozYj0mXOr4pXzpyCHIuFCqP1Rcz/gtNdLXoWmo3LY1KTjDaIZ9tqio6tVw==</Q><DP>SPyZx76666WujeGyBvT6Fd9zMhPUw2y3T7rrY26XMaKRMiH1L+QFH/rrJTFoky1uWDdR5qHk6NF1fcg/nKpJgQ==</DP><DQ>LoLZWROFLBQ8QqWg6ed/6u8gRuHW2R7VT5HuZzGEM4LBWCESPRdVczkHSI6j3H7djnG5doIyQEX9L8m1Fq1Stw==</DQ><InverseQ>DWgn8BI3zyf9wjGD2W7c4Y1Vlm+/va/RBGVxCyO7GPB/3V8OzU0VTq2ADA31ffOCWABHPgunr5P9zabWbvutsA==</InverseQ><D>R0VlSVh2yFG49OQTD3wOmZiIFvrKlvs+Hb1Bmbt/ARo3BA/zHAU2S/XP6BSoGWQQ5hpmFbU7y4tFDs2Io1xjljmZbVFtnVx6LdOhDI9G7sDq/OfNTn3dUg5jGdsXIyx/39T2M78vQTKpbn1Xo1Pn0I17Ovt4rRl2GsgSBhhKKIE=</D></RSAKeyValue>";


        public (Guid UserId, DateTime fulldatetime, string securityKey) DecryptData(string cipherText)
        {
            var str = RSATool.Decrypt(cipherText, pk);
            string[] array = str.Split(',').ToArray();
            
            return (array[0].ToGuid(), array[1].ParceFullToDateTime(), array[2]);         
            
        }

        public string EncryptData(Guid UserId, string fullstringdate, string securityKey)
        {
            var text = string.Concat(UserId.ToString(),",", fullstringdate, ",", securityKey);
            
            return RSATool.Encrypt(text, pb);

        }

        
    }
}
