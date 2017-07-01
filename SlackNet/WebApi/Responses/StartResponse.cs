﻿using System.Collections.Generic;
using SlackNet.Objects;

namespace SlackNet.WebApi.Responses
{
    public class StartResponse : ConnectResponse
    {
        public IList<User> Users { get; set; }
        public IList<Channel> Channels { get; set; }
        public IList<Group> Groups { get; set; }
        public IList<Group> Mpims { get; set; }
        public IList<Im> Ims { get; set; }
        public IList<Bot> Bots { get; set; }
    }
}