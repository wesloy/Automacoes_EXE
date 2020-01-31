using Bradesco.Fraude.VO.ResponseSmsVO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Bradesco.Fraude.VO.VO
{
    [Serializable]
    public class ResponseFlows
    {
        public string uri { get; set; }
        public string idResponse { get; set; }
        public string idFlows { get; set; }
        public AuthenticationToken authentication { get; set; }

        public string getUri()
        {
            return uri + idResponse;
        }


        private string bodyMessage;

        public string getBodyMessage
        {
            get
            {
                string parameter = "{\"categoryId\":\"{0}\",\"id\":\"{1}\"}";

                bodyMessage = parameter.Replace("{0}", idFlows).Replace("{1}", idResponse);
                return bodyMessage;
            }
            private set { bodyMessage = value; }
        }

        public List<ResponseFlows> getValuesResponse(object response)
        {
            ResponseFlows responseFlows = new ResponseFlows();
            List<ResponseFlows> responses = new List<ResponseFlows>();

            for (int i = 0; i < ((object[])response).Count(); i++)
            {
                Dictionary<string, object> idResp = (Dictionary<string, object>)(((object[])response))[i];
                for (int j = 0; j < idResp.Count(); j++)
                {
                    if ((idResp.ToArray()[j]).Key == "responseFlow")
                    {
                        Dictionary<string, object> idFlows = (Dictionary<string, object>)(idResp.ToArray()[j]).Value;

                        for (int k = 0; k < idFlows.Count; k++)
                        {
                            if (idFlows.ToArray()[k].Key == "categories")
                            {
                                /// if (idFlows.ToArray()[k].Key == "id")
                                ///    responseFlows.idFlows = idFlows.ToArray()[k].Value.ToString();

                                object categoriesID = idFlows.ToArray()[k].Value;

                                Dictionary<string, object> a = (Dictionary<string, object>)((object[])categoriesID)[0];
                                for (int l = 0; l < a.Count; l++)
                                    if (a.ToArray()[l].Key == "id")
                                    {
                                        responseFlows.idFlows = a.ToArray()[l].Value.ToString();
                                        break;
                                    }

                                break;
                            }
                        }                       
                    }

                    if ((idResp.ToArray()[j]).Key == "id")
                        responseFlows.idResponse = (idResp.ToArray()[j]).Value.ToString();
                }
                responses.Add(responseFlows);
            }//for

            return responses;
        }

        public ResponseFlows handlerResponseFlow(string response)
        {

            ResponseFlows responseFlows = new ResponseFlows();

            object resp = JsonConvert.DeserializeObject(response);
            JEnumerable<JToken> jTk = ((JObject)resp).Children();
            
            foreach (var jtok in jTk)
            {
                if (jtok.Path == "id")
                {
                    responseFlows.idResponse = jtok.First().ToString();
                    break;
                }
            }

            return responseFlows;
        }
    }
}
