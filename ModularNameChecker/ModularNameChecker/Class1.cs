using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
namespace ModularNameChecker
{
    public class accountChecker
    {
        #region Default Methods
        public string usernameInput;
        public Func<string>[] checkModules;
        public accountChecker(string _inputName)
        {
            usernameInput = _inputName;
            List<Func<string>> functionList = new List<Func<string>>();
            string[] ignoreNames = { "ToString", "Equals", "GetHashCode", "GetType" };
            foreach (MethodInfo singleMethod in typeof(accountChecker).GetMethods())
            {
                if (!(ignoreNames.Contains(singleMethod.Name)))
                {
                    Func<string> toAdd = (Func<string>)Delegate.CreateDelegate(typeof(Func<string>), this, singleMethod);
                    functionList.Add(toAdd);
                }
            }
            checkModules = functionList.ToArray();
        }
        #endregion
        #region Modules
        public string Twitter()
        {
            try
            {
                string toTest = new WebClient().DownloadString("https://twitter.com/users/username_available?scribeContext%5Bcomponent%5D=form&scribeContext%5Belement%5D=screen_name&username=<NAME>&value=<NAME>".Replace("<NAME>", usernameInput));
                if (toTest.Contains(":true,"))
                {
                    return "Available";
                }
                else
                {
                    return "Taken";
                }
            }
            catch
            {
                return "Unable to reach server";
            }
        }
        public string Minecraft()
        {
            try
            {
                string toTest = new WebClient().DownloadString("https://minecraft.net/haspaid.jsp?user=<NAME>".Replace("<NAME>", usernameInput));
                if (toTest.Contains("false"))
                {
                    return "Available";
                }
                else
                {
                    return "Taken";
                }
            }
            catch
            {
                return "Unable to reach server";
            }
        }
        public string Skype()
        {
            try
            {
                string toTest = new WebClient().DownloadString("https://login.skype.com/json/validator?new_username=<NAME>".Replace("<NAME>", usernameInput));
                if (toTest.Contains(":200"))
                {
                    return "Available";
                }
                else
                {
                    return "Taken";
                }
            }
            catch
            {
                return "Unable to reach server";
            }
        }
        public string GitHub()
        {
            try
            {
                string tempDownload = new WebClient().DownloadString("https://github.com/");
                string authToken = new Regex("<input name=\"authenticity_token\" type=\"hidden\" value=\"(.*?)\" />").Match(tempDownload).Groups[1].ToString();
                try
                {
                    new WebClient().UploadString("https://github.com/signup_check/username", "value=" + usernameInput + "&authenticity_token=" + HttpUtility.UrlEncode(authToken));
                    return "Available";
                }
                catch
                {
                    return "Taken";
                }
            }
            catch
            {
                return "Unable to reach server";
            }
        }
        public string Pastebin()
        {
            try
            {
                if ((usernameInput.Length > 2) && (usernameInput.Length < 21))
                {
                    if (new WebClient().DownloadString("http://pastebin.com/u/" + usernameInput).Contains("<title>Pastebin.com - #1 paste tool since 2002!</title>"))
                    {
                        return "Available";
                    }
                    else
                    {
                        return "Taken";
                    }
                }
                else
                {
                    return "Taken";
                }
            }
            catch
            {
                return "Unable to reach server";
            }
        }
        public string Twitch()
        {
            try
            {
                WebRequest webRequest = WebRequest.Create("https://passport.twitch.tv/usernames/<NAME>".Replace("<NAME>", usernameInput));
                webRequest.Method = "HEAD";
                HttpWebResponse reqResponse = (HttpWebResponse)webRequest.GetResponse();
                if ((int)reqResponse.StatusCode != 200)
                {
                    return "Available";
                }
                else
                {
                    return "Taken";
                }
            }
            catch
            {
                return "Unable to reach server";
            }
        }
        public string Reddit()
        {
            try
            {
                if (new WebClient().UploadString("https://www.reddit.com/api/check_username.json", "user=" + usernameInput) == "{}")
                {
                    return "Available";
                }
                else
                {
                    return "Taken";
                }
            }
            catch
            {
                return "Unable to reach server";
            }
        }
        public string Spotify()
        {
            try
            {
                if (new WebClient().DownloadString("https://www.spotify.com/uk/xhr/json/isUsernameAvailable.php?username=<NAME>") == "true")
                {
                    return "Available";
                }
                else
                {
                    return "Taken";
                }
            }
            catch
            {
                return "Unable to reach server";
            }
        }
        #endregion
    }
}
