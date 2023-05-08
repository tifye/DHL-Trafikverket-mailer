namespace EventStreamEmailer.Model.TrafikVerket
{
    public class TrafikVerketRequest
    {
        private readonly string _authKey;
        public TrafikVerketQuery Query { get; set; }
        
        public TrafikVerketRequest(string authenticationkey, TrafikVerketQuery query)
        {
            _authKey = authenticationkey;
            Query = query;
        }

        public string ToXml()
        {
            return @$"
            <REQUEST>
                <LOGIN authenticationkey=""{_authKey}"" />
                {Query.Query}
            </REQUEST>
            ";
        }
    }
}