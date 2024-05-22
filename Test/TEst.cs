using Catel;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string privateKey = @"LS0tLS1CRUdJTiBSU0EgUFJJVkFURSBLRVktLS0tLQpNSUlDV3dJQkFBS0JnR2RDbSsvcWp1SFJOelNMc0k1emZUTkxobWJJVElINzQ4QStxQ0hldUJwQ29xMHhJRVlvCkRIVlFudXhpUktjaERZakVhRlM5TWpZRzRCYkE5anBoQkFIOHR1K3lILy9pRUJYeDd2Wm1EU1ExdTJaZXRnRXcKRGZKTkcyT05XZWlENlA0Nmo0STFzMlUwQnpGcGtic0s3RkV4K2h3S1MrMFRLa0JIRXlBWnRmM0pBZ01CQUFFQwpnWUFOVnN0eUdLalFmd2hVbjE3MVovSGFlSDBxTmVHOFFOL1E2OGNvYU83N1pYUkNUMEJPRDhwY05VZnlYVmRsCkI1NEo5dVZMWFExcGNVWHRhb3ZrU3JnTHB3bFlEM1BJdEVLem9VRUF4UXdnUDNOTkJpQnovQ2NaYkFjM2MvZ0UKWTY0NFNCc2Jmc0ZiTlNFclRNTXdMelhjSzkxM3loblNMSGExTk10U0ZWU3BJUUpCQU1FN3RsZ3RXaGFWRzFpLwpweFpWMC92QTVpTmN2aWZaNW5TelFrT1dQMm1JbVJaaDBrejREMWg3SEJuQ0d2RU1HQnlGNFpaSHZrQWwvY1JLCmVxOTdkMjBDUVFDSXpUS3N2dTlWY21UNnA2aWllM1pYQlc3VTZvWlRLWWs5cjgrckpndlBtN2x2eXg4U0F3LzkKNHdsVzMwS20yTTJRUzB1enhXWTJlWmgyeXhsaitocE5Ba0FGSmVRYndVbVBKSFhRRFhzVUcwRkVpY1VYMkJhdwoxUnRRckozdFV3bHBkRnd2dm5kdDJZWC9JM2lDTHl1c2xGZm9HcUVCOGZOZG5pVituMFZaZTM2WkFrQWVqQU5tCnczUFoxcU5wdlFwUXpkVE05YStiNTRwN0EybGIxdWcrUlU2WjJ5SDdMcFlQaFpPS2s2bkFOalpCMzJOM2R2aSsKZWJPck1sZXpId0xhYWp4NUFrRUFzcjVxQmQrU2NYM2c5My9LcXpWbEllM0pwRFdRUy82S2U3dEppcmdEcnA1MQpZN0UzdGlGR2lWZHhoVzhBcUI3ZVhYUWVwai9UclFRL0dHMVBEdENiS0E9PQotLS0tLUVORCBSU0EgUFJJVkFURSBLRVktLS0tLQ==";


            //BuyReport
            /* BuyReport buyReport = new BuyReport { ServiceCode = "BCC00008" };
             string url = "http://192.168.1.201:253/api/OhsStoreService/TEST/BuyReport";
             var vaid = await APICall.post_sync<OhsServiceBuyReportResponse>(url, buyReport, privateKey);
             foreach (var prop in vaid.GetType().GetProperties())
             {
                 Console.WriteLine($"{prop.Name}: {prop.GetValue(vaid)}");
             }*/



            //GetReportDetail
            /*Dictionary<string, string> GetReportDetail = new Dictionary<string, string>
                {
                    { "ServiceCode", "001"},
                };
            string Detailurl = "https://localhost:7275/api/OhsStoreService/TEST/GetPagingReport";
            var vaid = await APICall.get_async<List<OhsStoreServiceReportPagedListResponse>>(Detailurl,GetReportDetail, privateKey);*/


            //GetServiceCategory
            /*Dictionary<string, string> GetServiceCategory = new Dictionary<string, string>
                {
                    
                };
            string Detailurl = "https://localhost:7275/api/OhsStoreService/TEST/GetServiceCategory";
            var vaid = await APICall.get_async<List<OhsStoreServiceCategoryListResponse>>(Detailurl, GetServiceCategory, privateKey);*/


            //GetPagingReport
            Dictionary<string, string> GetPagingReport = new Dictionary<string, string>
            {
                { "Keyword", ""},
                { "ServiceCategoryCode", ""},
                { "PageIndex", "1"},
                { "PageSize", "10"},
            };
            string Detailurl = "https://localhost:7275/api/OhsStoreService/TEST/GetPagingReport";
            var vaid = await APICall.get_async<List<OhsStoreServiceReportPagedListResponse>>(Detailurl, GetPagingReport, privateKey);
        }

        public class BuyReport
        {
            public string ServiceCode { get; set; }
        }
        public class OhsServiceBuyReportResponse
        {
            public string ServiceCode { get; set; }
            public string ServiceName { get; set; }
            public string Description { get; set; }
            public string DescriptionUrl { get; set; }
            public string SqlScript { get; set; }
            public string TemplateUrl { get; set; }
            public decimal Price { get; set; }
            public List<Licenses> Licenses { get; set; }

        }
        public class Licenses
        {
            public string AppCode { get; set; } = "MRS";
            public long ExpiredTime { get; set; }
            public string LicenseCode { get; set; }
            public string SubCode { get; set; }
        }
        public class OhsStoreServiceReportPagedListResponse
        {
            public long? CreateTime { get; set; }
            public string ServiceCode { get; set; }
            public string ServiceName { get; set; }
            public string ServiceTypeCode { get; set; }
            public string ServiceTypeName { get; set; }
            public string ServiceGategoryCode { get; set; }
            public string ServiceGategoryName { get; set; }
            public string Status { get; set; }
            public string AuthorLoginname { get; set; }
            public string AuthorFullName { get; set; }
            public string Description { get; set; }
            public string DescriptionUrl { get; set; }
            public decimal? Price { get; set; }
            public long BuyingCount { get; set; }
        }
        public class OhsStoreServiceCategoryListResponse
        {
            public string ServiceCategoryCode { get; set; }
            public string ServiceCategoryName { get; set; }
        }
    }
}