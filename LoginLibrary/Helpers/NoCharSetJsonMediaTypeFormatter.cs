﻿using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace LoginLibrary.Helpers
{
    public class NoCharSetJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType.CharSet = "";
        }
    }
}