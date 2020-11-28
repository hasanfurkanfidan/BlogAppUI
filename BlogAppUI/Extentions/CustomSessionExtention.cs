using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppUI.Extentions
{
    public static class CustomSessionExtention
    {
        public static void SetObject(this ISession session,object value,string key)
        {
            var data = JsonConvert.SerializeObject(value);
            session.SetString(key,data);
        }
        public static T GetObject<T>(this ISession session, string key) where T:class,new()
        {
           var data =  session.GetString(key);
            if (string.IsNullOrWhiteSpace(data))
            {
                
                return null;

            }
            else
            {
                return JsonConvert.DeserializeObject<T>(data);             
            }
        }
    }
}
