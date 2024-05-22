using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public HttpStatusCode Status { get; set; }
        public int StatusCode => (int)Status;
        public ResultParam Param { get; set; }

        public static implicit operator ApiResult(bool value)
        {
            return new()
            {
                Success = value,
                Status = value ? HttpStatusCode.OK : HttpStatusCode.BadRequest
            };
        }

        public static implicit operator ApiResult(string[] errors)
        {
            return new()
            {
                Success = errors == null,
                Status = errors == null ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            };
        }
    }
    public class ApiResult<T> : ApiResult
    {
        public ApiResult()
        {
        }

        public ApiResult(ResultParam param, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Success = false;
            Status = statusCode;
            Param = param;
        }

        public T Data { get; set; }

        public static implicit operator ApiResult<T>(T value)
        {
            return new()
            {
                Success = true,
                Status = HttpStatusCode.OK,
                Data = value,
            };
        }
    }

    public class PagedApiResult<T> : ApiResult
    {
        public PagedApiResult()
        {
        }

        public PagedApiResult(ResultParam param, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Success = false;
            Status = statusCode;
            Param = param;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public long TotalRecords { get; set; }

        public long TotalPage => PageSize == 0 ? 1 : (TotalRecords - 1) / PageSize + 1;

        public bool HasNextPage => PageIndex < TotalPage;

        public bool HasPreviousPage => PageIndex > 1;

        public IReadOnlyCollection<T> Data { get; set; }
    }
}
