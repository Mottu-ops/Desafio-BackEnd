using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Backend.Challenge.Domain.Entities.configs
{
    public class QueueServerConfig
    {
        public string Hostname { get; set; } = null!;
        public int Port { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
