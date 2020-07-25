using com.spoonacular;
using Org.OpenAPITools.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Classes
{
    public class Spoontacular
    {
        public static string username = "collinthec@hotmail.co.uk";  // string | The username.
        public static string hash = "3c5816cf4abd4d369c04583da847d8d3";  // string | The private hash for the username.

        public static ApiClient apiClient = new ApiClient();
        public static DefaultApi apiInstance = new DefaultApi();

        public static void InitiateSpoontacular()
        {
            //apiClient.AddDefaultHeader("username", username);
            apiClient.AddDefaultHeader("apiKey", hash);
            apiInstance = new DefaultApi(apiClient); 
        }
    }
}
