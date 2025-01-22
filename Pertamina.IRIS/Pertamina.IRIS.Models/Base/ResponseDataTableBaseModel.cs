using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.Base
{
    public class ResponseDataTableBaseModel
    {
        public ResponseDataTableBaseModel()
        {
            IsSuccess = true;
            Message = string.Empty;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class ResponseDataTableBaseModel<T>
    {
        public ResponseDataTableBaseModel()
        {
            IsSuccess = true;
            Message = string.Empty;
        }
        public ResponseDataTableBaseModel(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public ResponseDataTableBaseModel(T data)
        {
            IsSuccess = true;
            Message = string.Empty;
            Data = data;
        }
        public ResponseDataTableBaseModel(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public ResponseDataTableBaseModel(T data, PageInfoBaseModel pageInfo)
        {
            IsSuccess = true;
            Message = string.Empty;
            Data = data;
            PageInfo = pageInfo;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public PageInfoBaseModel PageInfo { get; set; }
    }
}
