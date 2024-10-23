using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MT.Backend.Challenge.Domain.Entities.Response
{
    public abstract class BaseResponse
    {
        [JsonPropertyName("mensagem")]
        public string Message { get; set; } = null!;

        public int StatusCode { get; set; } = 200;

    }
}
