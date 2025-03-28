﻿using System;
using System.Collections.Generic;

namespace XM.Shared.Core.Dialog
{
    public class DialogPage
    {
        public string Header { get; set; }
        public Action<DialogPage> PageInit { get; set; }
        public List<DialogResponse> Responses { get; set; }

        public DialogPage(Action<DialogPage> pageInit)
        {
            PageInit = pageInit;
            Responses = new List<DialogResponse>();
            Header = string.Empty;
        }

        public DialogPage AddResponse(string text, Action action)
        {
            Responses.Add(new DialogResponse(text, action));
            return this;
        }

        public int NumberOfResponses => Responses.Count;
    }
}
