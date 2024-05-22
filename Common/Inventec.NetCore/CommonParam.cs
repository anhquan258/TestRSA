namespace Inventec.NetCore
{
    public class CommonParam
    {
        public int? Start { get; set; }
        public int? Limit { get; set; }
        public string LanguageCode { get; set; }

        public CommonParam()
        {
        }

        public CommonParam(int? start, int? limit)
        {
            this.Start = start;
            this.Limit = limit;
        }
    }
}