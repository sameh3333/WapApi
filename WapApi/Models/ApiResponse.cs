﻿namespace WapApi.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public T? Data { get; set; }
        public List<string >? Errors { get; set; }


        public static ApiResponse<T> SuccessResponse(T data, string message = "")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> FailResponse(string message, List <string>? erreos =null )
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors= erreos
            };
        }



    }
}
 