using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace rest02.Model
{
    public class KlasaMVVM
    {
        public enum httpVerb
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        
        public string endPoint { get; set; } = "https://measurements-dev.azurewebsites.net/api/measurements";
        public string userName { get; set; } = "abbuser123";
        public string userPassword { get; set; } = "secretabbpassword123";
        public string postJSON { get; set; }
        public httpVerb httpMethod { get; set; } = httpVerb.POST;
        public string Pliki { get; set; } = "Twój Plik";

        public string Mail { get; set; } = "mail";

        public string Typ { get; set; } = "Typ[ID]";

        public string Lokacja { get; set; } = "Lokacja [ID]";

        public string Logi { get; set; } = "Uruchomienie Aplikacji \n";

        public string[] Files { get; set; }

        public void Send()
        {
            foreach (string maklowicz in Files)
            {
                httpMethod = KlasaMVVM.httpVerb.POST;
                postJSON = "{ \n" + "\"data\": " + File.ReadAllText(maklowicz) + ",\n" + "\"email\": \"" + Mail + "\",\n" + "\"location_id\": " + Lokacja + ",\n" + "\"type_id\": " + Typ + "\n }";
                Logi += "\nWykonywanie procedury: ";

                string strResponse = string.Empty;
                strResponse = makeRequest();

                Logi += strResponse;
            }

        }

        public void przygotowanie()
        {
            int licz = Files.Count();
            string filename = ""; //= System.IO.Path.GetFileName(files[0]);
            for(int i=0; i<licz;i++)
                 {
                     filename += System.IO.Path.GetFileName(Files[i]) + " \n";
                     string readText = File.ReadAllText(Files[i]);
                 }

            Pliki = filename;
        }

        public string makeRequest()
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();

            String authHeaer = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(userName + ":" + userPassword));
            request.Headers.Add("Authorization", "Basic" + " " + authHeaer);

            if (postJSON != string.Empty)
            {
                request.ContentType = "application/json";
                using (StreamWriter swJSONPayLoad = new StreamWriter(request.GetRequestStream()))
                {
                    swJSONPayLoad.Write(postJSON);
                    swJSONPayLoad.Close();
                }
            }

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();


                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception wyjatek)
            {
                strResponseValue = "\"Coś się posypało\":[\"" + wyjatek.Message.ToString() + "\"],\"Błędy.\"";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            return strResponseValue;
        }
    }
}
