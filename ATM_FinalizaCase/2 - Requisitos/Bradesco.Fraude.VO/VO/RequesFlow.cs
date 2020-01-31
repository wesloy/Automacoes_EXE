namespace Bradesco.Fraude.VO.VO
{
    public class RequesFlow
    {
        public string uri { get; set; }
        public string valueResponseFlows { get; set; }
        public string valueLimit { get; set; }

        public AuthenticationToken authenticationToken { get; set; }

        public string getUri()
        {
            return uri + "responseFlowId=" + valueResponseFlows + "&limit=" + valueLimit;
        }
    }
}