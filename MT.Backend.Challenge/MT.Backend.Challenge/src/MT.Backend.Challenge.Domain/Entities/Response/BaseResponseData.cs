using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Backend.Challenge.Domain.Entities.Response
{
    public abstract class BaseResponseData<T> : BaseResponse
    {
        public T Data { get; set; } = default!;
    }
}
