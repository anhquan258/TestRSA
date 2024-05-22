using Microsoft.AspNetCore.Mvc;
using System.Net;
using Test;

namespace Ohs.Controller
{
    public class OhsTestController : ControllerBase
    {
        string privateKey = @"LS0tLS1CRUdJTiBSU0EgUFJJVkFURSBLRVktLS0tLQpNSUlDV3dJQkFBS0JnR2RDbSsvcWp1SFJOelNMc0k1emZUTkxobWJJVElINzQ4QStxQ0hldUJwQ29xMHhJRVlvCkRIVlFudXhpUktjaERZakVhRlM5TWpZRzRCYkE5anBoQkFIOHR1K3lILy9pRUJYeDd2Wm1EU1ExdTJaZXRnRXcKRGZKTkcyT05XZWlENlA0Nmo0STFzMlUwQnpGcGtic0s3RkV4K2h3S1MrMFRLa0JIRXlBWnRmM0pBZ01CQUFFQwpnWUFOVnN0eUdLalFmd2hVbjE3MVovSGFlSDBxTmVHOFFOL1E2OGNvYU83N1pYUkNUMEJPRDhwY05VZnlYVmRsCkI1NEo5dVZMWFExcGNVWHRhb3ZrU3JnTHB3bFlEM1BJdEVLem9VRUF4UXdnUDNOTkJpQnovQ2NaYkFjM2MvZ0UKWTY0NFNCc2Jmc0ZiTlNFclRNTXdMelhjSzkxM3loblNMSGExTk10U0ZWU3BJUUpCQU1FN3RsZ3RXaGFWRzFpLwpweFpWMC92QTVpTmN2aWZaNW5TelFrT1dQMm1JbVJaaDBrejREMWg3SEJuQ0d2RU1HQnlGNFpaSHZrQWwvY1JLCmVxOTdkMjBDUVFDSXpUS3N2dTlWY21UNnA2aWllM1pYQlc3VTZvWlRLWWs5cjgrckpndlBtN2x2eXg4U0F3LzkKNHdsVzMwS20yTTJRUzB1enhXWTJlWmgyeXhsaitocE5Ba0FGSmVRYndVbVBKSFhRRFhzVUcwRkVpY1VYMkJhdwoxUnRRckozdFV3bHBkRnd2dm5kdDJZWC9JM2lDTHl1c2xGZm9HcUVCOGZOZG5pVituMFZaZTM2WkFrQWVqQU5tCnczUFoxcU5wdlFwUXpkVE05YStiNTRwN0EybGIxdWcrUlU2WjJ5SDdMcFlQaFpPS2s2bkFOalpCMzJOM2R2aSsKZWJPck1sZXpId0xhYWp4NUFrRUFzcjVxQmQrU2NYM2c5My9LcXpWbEllM0pwRFdRUy82S2U3dEppcmdEcnA1MQpZN0UzdGlGR2lWZHhoVzhBcUI3ZVhYUWVwai9UclFRL0dHMVBEdENiS0E9PQotLS0tLUVORCBSU0EgUFJJVkFURSBLRVktLS0tLQ==";
        /// <summary>
        /// Lấy danh sách sản phẩm báo cáo (Có phân trang)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(OhsStoreServiceReportPagedListResponse), (int)HttpStatusCode.OK)
        public async Task<IActionResult> GetPagingReport([FromQuery] OhsStoreServiceReportPagedListRequest request)
        {
            string url = "http://192.168.1.201:253/api/OhsStoreService/TEST/GetPagingReport";
            var vaid = await APICall.post_sync<OhsServiceBuyReportResponse>(url, x, privateKey);
        }

    }
    public class OhsStoreServiceReportPagedListRequest
    {
        public string Keyword { get; set; }
        public string ServiceCategoryCode { get; set; }
    }
    public class OhsStoreServiceReportPagedListResponse
    {
        public long? CreatTime { get; set; }
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
        public long BuyingCount { get; set; }
    }

}
