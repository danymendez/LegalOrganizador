using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Utilidades
{
    public class CacheItems
    {
        private readonly IMemoryCache _memoryCache;

        public CacheItems(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        public string GetImageBase64FromCache(bool isValid) {
            if (isValid)
            {
                if (_memoryCache.Get("foto") != null)
                    return Encoding.ASCII.GetString(_memoryCache.Get("foto") as byte[]);
            }

            return "/images/man64.png";
        }

        public string GetImageBase64FromCache(ClaimsPrincipal user)
        {
            if (user != null)
            {
                if (user.Identity.IsAuthenticated)
                {
                    if (_memoryCache.Get("foto") != null)
                        return Encoding.ASCII.GetString(_memoryCache.Get("foto") as byte[]);
                }
            }

            return "/images/man64.png";
        }
    }
}
