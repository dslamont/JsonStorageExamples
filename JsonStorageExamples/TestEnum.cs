﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JsonStorageExamples
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TestEnum
    {
        One,
        Two,
        Three
    }
}
