namespace EventStreamEmailer.Model.TrafikVerket
{
    public class TrafikVerketQuery
    {
        public string Query { get; } = string.Empty;

        public TrafikVerketQuery(string query)
        {
            Query = query;
        }
    }
}